using UnityEngine;
using RunningFishes.Pong.Players;

namespace RunningFishes.Pong.Gameplay
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance { get; private set; }

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Init();
            }
        }

        [SerializeField]
        private PlayerController playerController;

        public PlayerController PlayerController => playerController;

        public void Init()
        {
            playerController.Init();
        }

        public void OnDestroy()
        {
            playerController.Dispose();
        }
    }
}