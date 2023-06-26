using ExitGames.Client.Photon;
using Photon.Pun;
using RunningFishes.Pong.Ball;
using RunningFishes.Pong.Gameplay;
using UnityEngine;

namespace RunningFishes.Pong.Multiplayer
{
    public class MultiplayerController
    {
        private GameController gameController;
        private BallController ballController;

        private bool isSubscribed = false;

        public MultiplayerController()
        {
            SetController();
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

            if (PhotonNetwork.NetworkingClient != null)
            {
                PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
            }
        }

        private void Unsubscribe()
        {
            if (!isSubscribed) return;

            isSubscribed = false;

            if (PhotonNetwork.NetworkingClient != null)
            {
                PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
            }
        }

        public void OnEvent(EventData photonEvent)
        {
            SetController();

            if (photonEvent.Code == (byte)MultiplayerEventCode.BallData)
            {
                object[] data = (object[])photonEvent.CustomData;
                Vector3 position = (Vector3)data[0];
                Vector2 speed = (Vector2)data[1];
                ballController.RaiseBallDataReceive(position, speed);
            }
        }

        private void SetController()
        {
            gameController = GameController.instance;
            ballController = gameController?.BallController;
        }
    }
}