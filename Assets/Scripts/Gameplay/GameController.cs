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

        [SerializeField]
        private BallController ballController;

        public BallController BallController => ballController;

        private MultiplayerController multiplayerController;

        public void Init()
        {
            playerController.Init();
            ballController.Init();
            multiplayerController = new MultiplayerController();
        }

        public void OnDestroy()
        {
            playerController.Dispose();
            ballController.Dispose();
            multiplayerController.Dispose();
        }
    }
}