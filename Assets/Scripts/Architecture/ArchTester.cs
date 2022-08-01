using UnityEngine;
using System;

namespace Architecture
{
    class ArchTester : MonoBehaviour
    {
        private BankRepository bankRepository;
        private BankInteractor bankInteractor;

        private void Start()
        {
            
            this.bankRepository = new BankRepository();
            this.bankRepository.Initialize();

            this.bankInteractor = new BankInteractor(this.bankRepository);
            this.bankInteractor.Initialize();
        }

        private void Update()
        {
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
