using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture
{
    public static class Bank
    {
        public static event Action OnBankInitializedEvent;

        public static int coins
        {
            get
            {
                CheckClass();
                return bankInteractor.coins;
            }
        }
        public static bool isInitialized { get; private set; }

        private static BankInteractor bankInteractor;

        public static void Initialize(BankInteractor interactor)
        {
            bankInteractor = interactor;
            isInitialized = true;

            OnBankInitializedEvent?.Invoke();
        }

        public static bool IsEnoughtCoins(int value)
        {
            CheckClass();
            return bankInteractor.IsEnoughtCoins(value);
        }

        //дублируем поведение интерактора, т.к. это фасад
        public static void AddCoins(object sender, int value)
        {
            CheckClass();
            bankInteractor.AddCoins(sender, value);
        }

        public static void Spend(object sender, int value)
        {
            CheckClass();
            bankInteractor.Spend(sender, value);
        }

        private static void CheckClass()
        {
            if(!isInitialized)
            {
                throw new Exception("Bank is not initialize yet");//если банк не проинициализирован, то мы подписываемся на событие
            }
        }
    }
}
