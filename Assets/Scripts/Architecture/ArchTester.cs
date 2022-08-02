using UnityEngine;
using System;
using System.Collections;
using Architecture.Scenes;

namespace Architecture
{
    class ArchTester : MonoBehaviour
    {
        public static SceneManagerBase sceneManager;

        private void Start()
        {
            sceneManager = new SceneManagerExample();
            sceneManager.InitScenesMap();
            sceneManager.LoadCurrentSceneAsync();
        }
        

        private void Update()
        {
            if (!Bank.isInitialized)
                return;
            if(Input.GetKeyDown(KeyCode.A))
            {
                Bank.AddCoins(this, 5);
                Debug.Log($"Coins added (5), {Bank.coins}");
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Bank.Spend(this, 10);
                Debug.Log($"Coins spended (10), {Bank.coins}");
            }
        }
    }
}
