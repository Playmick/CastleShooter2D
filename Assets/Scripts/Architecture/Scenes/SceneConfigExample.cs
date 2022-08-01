using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Scenes
{
    public class SceneConfigExample : SceneConfig
    {
        public override Dictionary<Type, Repository> CreateAllRepositories()
        {
            var repositoriesMap = new Dictionary<Type, Repository>();

            this.CreateRepository<BankRepository>(repositoriesMap);
            //this.CreateRepository<BankRepository>(repositoriesMap);
            //this.CreateRepository<BankRepository>(repositoriesMap);
            //this.CreateRepository<BankRepository>(repositoriesMap);

            return repositoriesMap;
        }

        public override Dictionary<Type, Interactor> CreateAllInteractors()
        {
            var interactorsMap = new Dictionary<Type, Interactor>();

            this.CreateInteractor<BankInteractor>(interactorsMap);
            //this.CreateInteractor<BankInteractor>(interactorsMap);
            //this.CreateInteractor<BankInteractor>(interactorsMap);
            //this.CreateInteractor<BankInteractor>(interactorsMap);

            return interactorsMap;
        }
    }
}
