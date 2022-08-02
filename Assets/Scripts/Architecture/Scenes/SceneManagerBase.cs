using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Architecture.Routine;
using UnityEngine;

namespace Architecture.Scenes
{
    //обёртка вокруг классического Unity SceneManager
    //она умеет загружать и инициализировать репозитории и интеракторы в асинхронном режиме
    //через корутину новой сцены, а так же текущей сцены
    public abstract class SceneManagerBase
    {
        public event Action<Scene> OnSceneLoadingEvent;
        public Scene scene { get; private set; }
        public bool isLoading { get; private set; }

        protected Dictionary<string, SceneConfig> sceneConfigMap;

        public SceneManagerBase()
        {
            this.sceneConfigMap = new Dictionary<string, SceneConfig>();
        }

        //внутри этого класса будет инициализироваться список сцен
        public abstract void InitScenesMap();

        public Coroutine LoadCurrentSceneAsync()
        {
            if (this.isLoading)
                throw new Exception("Scene is loading now");

            var sceneName = SceneManager.GetActiveScene().name;

            var config = this.sceneConfigMap[sceneName];

            return Coroutines.StartRoutine(this.LoadCurrentSceneRoutine(config));
        }

        //когда мы стартуем игру, первая наша сцена уже загружена
        //нам нужно её только проинициализировать
        private IEnumerator LoadCurrentSceneRoutine(SceneConfig sceneConfig)
        {
            this.isLoading = true;
            
            yield return Coroutines.StartRoutine(this.InitializeSceneAsync(sceneConfig));

            this.isLoading = false;
            this.OnSceneLoadingEvent?.Invoke(this.scene);
        }

        private IEnumerator LoadNewSceneRoutine(SceneConfig sceneConfig)
        {
            this.isLoading = true;

            yield return Coroutines.StartRoutine(this.LoadSceneRoutine(sceneConfig));
            yield return Coroutines.StartRoutine(this.InitializeSceneAsync(sceneConfig));

            this.isLoading = false;
            this.OnSceneLoadingEvent?.Invoke(this.scene);
        }

        public Coroutine LoadNewSceneAsync(string sceneName)
        {
            if (this.isLoading)
                throw new Exception("Scene is loading now");

            var config = this.sceneConfigMap[sceneName];
            //если в этом конфиге не будет sceneName то он выдаст Exception
            //это значит что конфиг в текущей сцене не проинициализирован
            //нужно будет создать для нее кофиг и загрузить его

            return Coroutines.StartRoutine(this.LoadNewSceneRoutine(config));
        }

        //внутри этого класса мы загружаем сцены и инициализируем их
        //помимо загрузки новой сцены нам нужно инициализировать все репозитории и интеракторы

        private IEnumerator LoadSceneRoutine(SceneConfig sceneConfig)
        {
            var async = SceneManager.LoadSceneAsync(sceneConfig.sceneName);
            async.allowSceneActivation = false;

            while (async.progress < 0.9f)
                yield return null;

                async.allowSceneActivation = true;
        }
        //Async = возвращается корутина
        private IEnumerator InitializeSceneAsync(SceneConfig sceneConfig)
        {
            this.scene = new Scene(sceneConfig);
            yield return this.scene.InitializeAsync();
            //эта корутина будет выполняться столько, сколько будет происходить инициализация
            //инициализация может происходить разное кол-во времени
        }

        public T GetRepository<T>() where T : Repository
        {
            return this.scene.GetRepository<T>();
        }

        public T GetInteractor<T>() where T : Interactor
        {
            return this.scene.GetInteractor<T>();
        }
    }
}
