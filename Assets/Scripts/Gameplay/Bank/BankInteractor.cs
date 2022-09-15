using Architecture;

namespace Gameplay
{ 
    public class BankInteractor : Interactor
    {
        private BankRepository repository;

        public int coins => this.repository.coins;

        public override void OnCreate()
        {
            base.OnCreate();
            this.repository = Game.sceneManager.GetRepository<BankRepository>();
        }

        public override void Initialize()
        {
            Bank.Initialize(this);
        }

        public bool IsEnoughtCoins(int value)
        {
            return coins >= value;
        }

        //нам важно знать откуда игрок получил монетки
        //если монетка подбирается с земли то должна проиграться анимация
        //если понетка передается персонажу от NPC то анимация не проигрывается
        public void AddCoins(object sender, int value)
        {
            this.repository.coins += value;
            this.repository.Save();
        }

        public void Spend(object sender, int value)
        {
            this.repository.coins -= value;
            this.repository.Save();
        }
    }
}
