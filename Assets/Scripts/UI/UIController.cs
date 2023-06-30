using RunningFishes.Pong.Score;
using RunningFishes.Pong.State;
using UnityEngine;

namespace RunningFishes.Pong.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private UIScoreController uiScoreController;

        [SerializeField]
        private UIStartGame uiStartGame;

        public void Init(ScoreController scoreController, StateController stateController)
        {
            uiScoreController.Init(scoreController);
            uiStartGame.Init(stateController);
        }

        public void Dispose()
        {
            uiScoreController.Dispose();
            uiStartGame.Dispose();
        }
    }
}