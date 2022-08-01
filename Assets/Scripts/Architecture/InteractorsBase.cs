using System;
using System.Collections.Generic;

namespace Architecture
{
    public class InteractorsBase
    {
        //интеракторы хранятся в списке
        private Dictionary<Type, Interactor> interactorsMap;
        public InteractorsBase()
        {
            this.interactorsMap = new Dictionary<Type, Interactor>();
        }

        public void CreateAllInteractors()
        {
            CreateInteractor<BankInteractor>();

            /* CreateInteractor<BankInteractor>();
             * CreateInteractor<BankInteractor>();
             * CreateInteractor<BankInteractor>();
             * CreateInteractor<BankInteractor>();
             * */
        }

        private void CreateInteractor<T>() where T: Interactor, new()
        {
            var interactor = new T();
            var type = typeof(T);
            this.interactorsMap[type] = interactor;
        }

        public void SendOnCreateToAllInteractors()
        {
            var allInteractors = this.interactorsMap.Values;
            foreach (var interactor in allInteractors)
            {
                interactor.OnCreate();
            }
        }
        public void InitializeAllInteractors()
        {
            var allInteractors = this.interactorsMap.Values;
            foreach (var interactor in allInteractors)
            {
                interactor.Initialize();
            }
        }
        public void SendOnStartToAllInteractors()
        {
            var allInteractors = this.interactorsMap.Values;
            foreach (var interactor in allInteractors)
            {
                interactor.OnStart();
            }
        }

        public T GetInteractor<T>() where T : Interactor
        {
            var type = typeof(T);
            return (T)this.interactorsMap[type];
        }
    }
}
