using Architecture.Scenes;
using Architecture.Routine;
using System.Collections;
using System;
using UnityEngine;

namespace Architecture
{
    public static class Game
    {
        public static bool isRun { get; private set; }

        public static event Action OnGameInitializedEvent;
        public static SceneManagerBase sceneManager { get; private set; }
        public static void Run()
        {
            sceneManager = new SceneManager();
            Coroutines.StartRoutine(InitializeGameRoutine());
        }

        //в этой корутине инициализируем игру
        private static IEnumerator InitializeGameRoutine()
        {
            sceneManager.InitScenesMap();
            yield return sceneManager.LoadCurrentSceneAsync();
            isRun = true;
            OnGameInitializedEvent?.Invoke();
        }

        public static T GetRepository<T>() where T : Repository
        {
            return sceneManager.GetRepository<T>();
        }

        public static T GetInteractor<T>() where T : Interactor
        {
            return sceneManager.GetInteractor<T>();
        }
    }
}
