using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using RunningFishes.Pong.Multiplayer;
using RunningFishes.Pong.State;
using UnityEngine;

namespace RunningFishes.Pong.Ball
{
    public class BallMovementController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        private StateController stateController;
        private bool isSubscribed = false;

        public void Init(StateController stateController)
        {
            this.stateController = stateController;
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
                stateController.OnStateChanged += StartGame;
            }
        }

        private void Unsubscribe()
        {
            if (!isSubscribed) return;

            isSubscribed = false;

            if (stateController != null)
            {
                stateController.OnStateChanged -= StartGame;
            }
        }

        public void StartGame(States state)
        {
            if (!PhotonNetwork.IsMasterClient) return;
            if (state != States.Playing) return;

            rb.velocity = Vector2.zero;
            float randomDegree = Random.Range(-60, 60);
            float randomDirection = Random.Range(0, 2) == 0 ? -1 : 1;
            float randomForce = Random.Range(7, 10);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(randomDirection * randomForce * Mathf.Cos(randomDegree * Mathf.Deg2Rad), randomForce * Mathf.Sin(randomDegree * Mathf.Deg2Rad)), ForceMode2D.Impulse);
            BroadcastBallData(transform.position, rb.velocity);
        }

        private void BroadcastBallData(Vector3 position, Vector2 speed)
        {
            object[] data = new object[] { position, speed };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent((byte)MultiplayerEventCode.BallData, data, raiseEventOptions, SendOptions.SendReliable);
        }
    }
}