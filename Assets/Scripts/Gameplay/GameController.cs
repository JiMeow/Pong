using Photon.Pun;
using RunningFishes.Pong.Ball;
using RunningFishes.Pong.Constant;
using RunningFishes.Pong.Multiplayer;
using RunningFishes.Pong.Players;
using RunningFishes.Pong.Score;
using RunningFishes.Pong.State;
using RunningFishes.Pong.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunningFishes.Pong.Gameplay
{
    public class GameController : MonoBehaviourPunCallbacks
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
            stateController = new StateController();
            scoreController = new ScoreController(stateController);
            playerController.Init();
            ballController.Init(stateController);
            multiplayerController = new MultiplayerController();
            uiController.Init(scoreController, stateController);
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PhotonNetwork.LeaveRoom();
            }
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(SceneName.Lobby);
            base.OnLeftRoom();
        }
    }
}