using UnityEngine;
using RunningFishes.Pong.Players;
using RunningFishes.Pong.Ball;
using RunningFishes.Pong.Multiplayer;
using RunningFishes.Pong.UI;
using RunningFishes.Pong.Score;
using RunningFishes.Pong.State;

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
            else
            {
                Destroy(gameObject);
            }
        }

        [SerializeField]
        private PlayerController playerController;

        public PlayerController PlayerController => playerController;

        [SerializeField]
        private BallController ballController;

        public BallController BallController => ballController;

        [SerializeField]
        private UIController uiController;

        public UIController UIController => uiController;

        private MultiplayerController multiplayerController;
        private StateController stateController;
        private ScoreController scoreController;
        public ScoreController ScoreController => scoreController;

        public void Init()
        {
            playerController.Init();
            ballController.Init();
            multiplayerController = new MultiplayerController();
            stateController = new StateController();
            scoreController = new ScoreController();
            uiController.Init(scoreController);
        }

        public void OnDestroy()
        {
            playerController.Dispose();
            ballController.Dispose();
            multiplayerController.Dispose();
            stateController.Dispose();
            scoreController.Dispose();
            uiController.Dispose();
        }
    }
}