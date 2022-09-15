﻿using System;
using System.Collections.Generic;
using Architecture;
using Gameplay;

namespace Scenes
{
    public class MainMenuSceneConfig : SceneConfig
    {
        public const string SCENE_NAME = "MainMenu";
        public override string sceneName => SCENE_NAME;

        public override Dictionary<Type, Repository> CreateAllRepositories()
        {
            var repositoriesMap = new Dictionary<Type, Repository>();

            //заккоментил потому что в главном меню не нужен ни игрок ни банк
            //this.CreateRepository<BankRepository>(repositoriesMap);
            //тут мы можем создавать множество таких же репозиториев, просто сейчас он существует только один
            //(Банковский репозиторий хранящий денюжки)
            //поэтому я множу код
            //в будущем нужно сделать репозиторий по каждый навык
            //this.CreateRepository<HealthRepository>(repositoriesMap);
            //this.CreateRepository<BankRepository>(repositoriesMap);
            //this.CreateRepository<BankRepository>(repositoriesMap);

            return repositoriesMap;
        }

        public override Dictionary<Type, Interactor> CreateAllInteractors()
        {
            var interactorsMap = new Dictionary<Type, Interactor>();

            /*
             * заккоментил потому что в главном меню не нужен ни игрок ни банк
            this.CreateInteractor<BankInteractor>(interactorsMap);
            this.CreateInteractor<PlayerInteractor>(interactorsMap);*/
            //тут мы можем создавать множество таких же интеракторов, просто сейчас он существует только один
            //(Банковский интерактор работающий с банком)
            //поэтому я множу код
            //в будущем нужно сделать интерактор по каждый навык
            //this.CreateInteractor<BankInteractor>(interactorsMap);
            //this.CreateInteractor<BankInteractor>(interactorsMap);
            //this.CreateInteractor<BankInteractor>(interactorsMap);

            //а ещё сцену с главным меню в котором нужен репозиторий текстов и текущего языка
            //репозиторий текстов будет грузить тексты для кнопок из ScriptableObjects

            return interactorsMap;
        }
    }
}
