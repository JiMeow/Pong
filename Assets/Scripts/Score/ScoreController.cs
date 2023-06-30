using ExitGames.Client.Photon;
using Photon.Pun;
using RunningFishes.Pong.State;
using System;
using UnityEngine;

namespace RunningFishes.Pong.Score
{
    public class ScoreController : MonoBehaviourPunCallbacks
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

                if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Player1Score"] != player1Score)
                {
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Player1Score", player1Score } });
                }

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

                if ((int)PhotonNetwork.CurrentRoom.CustomProperties["Player2Score"] != player2Score)
                {
                    PhotonNetwork.CurrentRoom.SetCustomProperties(new Hashtable() { { "Player2Score", player2Score } });
                }

                OnPlayer2ScoreChanged?.Invoke(player2Score);
            }
        }

        public void Init(StateController stateController)
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

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            base.OnRoomPropertiesUpdate(propertiesThatChanged);
            if (propertiesThatChanged != null)
            {
                Player1Score = propertiesThatChanged.ContainsKey("Player1Score") ? (int)propertiesThatChanged["Player1Score"] : Player1Score;
                Player2Score = propertiesThatChanged.ContainsKey("Player2Score") ? (int)propertiesThatChanged["Player2Score"] : Player2Score;
            }
            Debug.Log("player1Score: " + Player1Score + " player2Score: " + Player2Score);
        }
    }
}