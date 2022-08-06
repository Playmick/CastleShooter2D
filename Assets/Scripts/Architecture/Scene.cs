using System.Collections;
using UnityEngine;
using Architecture.Routine;

namespace Architecture
{
    //этот класс хранит базу репозиториев и базу интеракторов
    public class Scene
    {
        //создаем сцену
        //для этой сцены у нас есть конфиг
        //конфиг формирует список репозиториев и интеракторов
        //чтобы этот список формировался в базе, мы создаем экземпляры базы интеракторов с конфигом указанной сцены
        //и экземпляр базы репозиториев с конфигом указанной сцены

        private InteractorsBase interactorsBase;
        private RepositoriesBase repositoriesBase;
        private SceneConfig sceneConfig;

        public Scene(SceneConfig config)
        {
            this.sceneConfig = config;
            this.interactorsBase = new InteractorsBase(config);
            this.repositoriesBase = new RepositoriesBase(config);
        }

        public Coroutine InitializeAsync()
        {
            return Coroutines.StartRoutine(this.InitializeRoutine());
        }

        private IEnumerator InitializeRoutine()
        {
            repositoriesBase.CreateAllRepositories();
            interactorsBase.CreateAllInteractors();

            yield return null;

            repositoriesBase.SendOnCreateToAllRepositories();
            interactorsBase.SendOnCreateToAllInteractors();
            yield return null;

            //этот метод может обрабатывать огромные объемы данных поэтому его рекомендуется выполнять через корутину

            repositoriesBase.InitializeAllRepositories();
            interactorsBase.InitializeAllInteractors();
            yield return null;

            repositoriesBase.SendOnStartToAllRepositories();
            interactorsBase.SendOnStartToAllInteractors();
        }

        public T GetRepository<T>() where T: Repository
        {
            return this.repositoriesBase.GetRepository<T>();
        }

        public T GetInteractor<T>() where T : Interactor
        {
            return this.interactorsBase.GetInteractor<T>();
        }
    }
}
