using RunningFishes.Pong.State;
using System;

namespace RunningFishes.Pong.Score
{
    public class ScoreController
    {
        /// <summary>
        /// Event raised when player 1 score changes
        /// int is the new score
        /// </summary>
        public event Action<int> OnPlayer1ScoreChanged;

        /// <summary>
        /// Event raised when player 2 score changes
        /// int is the new score
        /// </summary>
        public event Action<int> OnPlayer2ScoreChanged;

        private StateController stateController;
        private bool isSubscribed = false;
        private int player1Score = 0;
        private int player2Score = 0;

        public int Player1Score
        {
            get => player1Score;
            set
            {
                if (player1Score == value) return;

                player1Score = value;
                OnPlayer1ScoreChanged?.Invoke(player1Score);
            }
        }

        public int Player2Score
        {
            get => player2Score;
            set
            {
                if (player2Score == value) return;

                player2Score = value;
                OnPlayer2ScoreChanged?.Invoke(player2Score);
            }
        }

        public ScoreController(StateController stateController)
        {
            stateController = this.stateController;
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

            if (stateController != null)
            {
                stateController.OnStateChanged += OnStateChanged;
            }
        }

        private void Unsubscribe()
        {
            if (!isSubscribed) return;

            isSubscribed = false;

            if (stateController != null)
            {
                stateController.OnStateChanged -= OnStateChanged;
            }
        }

        private void OnStateChanged(States state)
        {
            switch (state)
            {
                case States.Prestart:
                    Player1Score = 0;
                    Player2Score = 0;
                    break;

                default:
                    break;
            }
        }
    }
}