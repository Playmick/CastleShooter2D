using Architecture;
using System;

namespace Gameplay
{
    public class SkillsInteractor : Interactor
    {
        private SkillsRepository repository;

        public SkillsInteractor()
        {

        }

        public int heart => this.repository.heart;
        public int playerSpeed => this.repository.playerSpeed;
        public int overheat => this.repository.overheat;
        public int attack => this.repository.attack;
        public int bulletSpeed => this.repository.bulletSpeed;
        public int luck => this.repository.luck;

        public int spentCoins { get; set; }

        public override void OnCreate()
        {
            base.OnCreate();
            this.spentCoins = 0;
            this.repository = Game.sceneManager.GetRepository<SkillsRepository>();
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void OnStart()
        {
            base.OnStart();
        }

        //добавить скилл
        public bool AddSkill(Skill skill)
        {
            //нужна проверка что скилл меньше 10
            switch (skill)
            {
                case Skill.heart:
                    if (!SkillIsMax(repository.heart))
                        repository.heart += 1;
                    else
                        return false;
                    break;
                case Skill.playerSpeed:
                    if(!SkillIsMax(repository.playerSpeed))
                        repository.playerSpeed += 1;
                    else
                        return false;
                    break;
                case Skill.overheat:
                    if (!SkillIsMax(repository.overheat))
                        repository.overheat += 1;
                    else
                        return false;
                    break;
                case Skill.attack:
                    if (!SkillIsMax(repository.attack))
                        repository.attack += 1;
                    else
                        return false;
                    break;
                case Skill.bulletSpeed:
                    if (!SkillIsMax(repository.bulletSpeed))
                        repository.bulletSpeed += 1;
                    else
                        return false;
                    break;
                case Skill.luck:
                    if (!SkillIsMax(repository.luck))
                        repository.luck += 1;
                    else
                        return false;
                    break;
            }
            spentCoins += 1;
            return true;
        }
        //сбросить изменения скиллов за период в магазе
        public int ResetChanges()
        {
            repository.Initialize();
            int value = spentCoins;
            spentCoins = 0;
            return value;
        }
        //обнулить скиллы
        public void ResetSkills()
        {
            repository.heart = 0;
            repository.playerSpeed = 0;
            repository.overheat = 0;
            repository.attack = 0;
            repository.bulletSpeed = 0;
            repository.luck = 0;
            spentCoins = 0;
            repository.Save();
        }
        public void Save()
        {
            spentCoins = 0;
            repository.Save();
        }
        public bool IsMax()
        {
            if (heart == 10
            && playerSpeed == 10
            && overheat == 10
            && attack == 10
            && bulletSpeed == 10
            && luck == 10)
                return true;
            return false;
        }
        public bool SkillIsMax(int value)
        {
            if (value >= 10)
                return true;
            return false;
        }

        
    }
}
