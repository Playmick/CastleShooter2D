using UnityEngine;
using System;
using System.Collections;

namespace Architecture
{
    class ArchTester : MonoBehaviour
    {
        public static RepositoriesBase repositoriesBase;
        public static InteractorsBase interactorsBase;

        private void Start()
        {
            this.StartCoroutine(this.StartGameRoutine());
        }

        private IEnumerator StartGameRoutine()
        {
            repositoriesBase = new RepositoriesBase();
            interactorsBase = new InteractorsBase();

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
            yield return null;
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
