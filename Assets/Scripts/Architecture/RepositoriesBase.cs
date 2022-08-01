using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture
{
    class RepositoriesBase
    {
        //интеракторы хранятся в списке
        private Dictionary<Type, Repository> repositoriesMap;

        public RepositoriesBase()
        {
            this.repositoriesMap = new Dictionary<Type, Repository>();
        }

        public void CreateAllRepositories()
        {
            CreateRepository<BankRepository>();

            /* CreateRepository<BankRepository>();
             * CreateRepository<BankRepository>();
             * CreateRepository<BankRepository>();
             * CreateRepository<BankRepository>();
             * */
        }

        private void CreateRepository<T>() where T : Repository, new()
        {
            var repository = new T();
            var type = typeof(T);
            this.repositoriesMap[type] = repository;
        }

        public void SendOnCreateToAllRepositories()
        {
            var allRepositories = this.repositoriesMap.Values;
            foreach (var repository in allRepositories)
            {
                repository.OnCreate();
            }
        }
        public void InitializeAllRepositories()
        {
            var allRepositories = this.repositoriesMap.Values;
            foreach (var repository in allRepositories)
            {
                repository.Initialize();
            }
        }
        public void SendOnStartToAllRepositories()
        {
            var allRepositories = this.repositoriesMap.Values;
            foreach (var repository in allRepositories)
            {
                repository.OnStart();
            }
        }

        public T GetRepository<T>() where T : Repository
        {
            var type = typeof(T);
            return (T)this.repositoriesMap[type];
        }

    }
}
