﻿using UnityEngine;
using System;
using System.Collections;
using Architecture.Scenes;

namespace Architecture
{
    class ArchTester : MonoBehaviour
    {
        public static Scene scene;
        private void Start()
        {
            var sceneConfig = new SceneConfigExample();
            scene = new Scene(sceneConfig);

            this.StartCoroutine(scene.InitializeRoutine());
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
