using UnityEngine;
using Architecture;

namespace Gameplay
{
    public class SkillsRepository : Repository
    {
        private const string HEART = "HEART_KEY";
        private const string PLAYERSPEED = "PLAYERSPEED_KEY";
        private const string OVERHEAD = "OVERHEAD_KEY";
        private const string ATTACK = "ATTACK_KEY";
        private const string BULLETSPEED = "BULLETSPEED_KEY";
        private const string LUCK = "LUCK_KEY";
        public int heart { get; set; }
        public int playerSpeed { get; set; }
        public int overheat { get; set; }
        public int attack { get; set; }
        public int bulletSpeed { get; set; }
        public int luck { get; set; }

        public override void Initialize()
        {
            this.heart = PlayerPrefs.GetInt(HEART, 0);
            this.playerSpeed = PlayerPrefs.GetInt(PLAYERSPEED, 0);
            this.overheat = PlayerPrefs.GetInt(OVERHEAD, 0);
            this.attack = PlayerPrefs.GetInt(ATTACK, 0);
            this.bulletSpeed = PlayerPrefs.GetInt(BULLETSPEED, 0);
            this.luck = PlayerPrefs.GetInt(LUCK, 0);
        }

        public override void Save()
        {
            PlayerPrefs.SetInt(HEART, this.heart);
            PlayerPrefs.SetInt(PLAYERSPEED, this.playerSpeed);
            PlayerPrefs.SetInt(OVERHEAD, this.overheat);
            PlayerPrefs.SetInt(ATTACK, this.attack);
            PlayerPrefs.SetInt(BULLETSPEED, this.bulletSpeed);
            PlayerPrefs.SetInt(LUCK, this.luck);
        }
    }
}
