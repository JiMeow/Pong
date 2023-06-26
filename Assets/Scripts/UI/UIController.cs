using UnityEngine;
using RunningFishes.Pong.Score;

namespace RunningFishes.Pong.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private UIScoreController uiScoreController;

        public void Init(ScoreController scoreController)
        {
            uiScoreController.Init(scoreController);
        }

        public void Dispose()
        {
            uiScoreController.Dispose();
        }
    }
}