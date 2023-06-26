using UnityEngine;
using UnityEngine.UI;
using RunningFishes.Pong.Score;

namespace RunningFishes.Pong.UI
{
    public class UIScoreController : MonoBehaviour
    {
        [SerializeField]
        private Text player1ScoreText;

        [SerializeField]
        private Text player2ScoreText;

        private ScoreController scoreController;
        private bool isSubscribed = false;

        public void Init(ScoreController scoreController)
        {
            this.scoreController = scoreController;
            player1ScoreText.text = "Player1: 0";
            player2ScoreText.text = "Player2: 0";
            Subscribe();
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            if (isSubscribed) return;

            isSubscribed = true;

            if (scoreController != null)
            {
                scoreController.OnPlayer1ScoreChanged += OnScore1Changed;
                scoreController.OnPlayer2ScoreChanged += OnScore2Changed;
            }
        }

        private void Unsubscribe()
        {
            if (!isSubscribed) return;

            isSubscribed = false;

            if (scoreController != null)
            {
                scoreController.OnPlayer1ScoreChanged -= OnScore1Changed;
                scoreController.OnPlayer2ScoreChanged -= OnScore2Changed;
            }
        }

        private void OnScore1Changed(int newScore)
        {
            player1ScoreText.text = "Player1: " + newScore.ToString();
        }

        private void OnScore2Changed(int newScore)
        {
            player2ScoreText.text = "Player2: " + newScore.ToString();
        }
    }
}