using UnityEngine;

namespace Architecture
{
    public class PlayerInteractor : Interactor
    {
        public Player player { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            var goPlayer = new GameObject("Player");
            this.player = goPlayer.AddComponent<Player>();
        }
    }
}
