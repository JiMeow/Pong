using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using RunningFishes.Pong.Multiplayer;
using UnityEngine;

namespace RunningFishes.Pong.Ball
{
    public class Balls : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        private BallController ballController;

        private bool isSubscribed = false;

        public void Init(BallController ballController)
        {
            this.ballController = ballController;
            Subscribe();
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            if (isSubscribed) return;

            isSubscribed = false;

            if (ballController != null)
            {
                ballController.OnBallDataReceived += SetBallProperties;
            }
        }

        private void Unsubscribe()
        {
            if (!isSubscribed) return;

            isSubscribed = false;

            if (ballController != null)
            {
                ballController.OnBallDataReceived -= SetBallProperties;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                bool isMine = collision.gameObject.GetPhotonView().IsMine;
                if (!isMine) return;

                Vector2 speed = rb.velocity;
                Vector3 position = transform.position;
                BroadcastBallData(position, speed);
            }
        }

        private void BroadcastBallData(Vector3 position, Vector2 speed)
        {
            object[] data = new object[] { position, speed };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.Others };
            PhotonNetwork.RaiseEvent((byte)MultiplayerEventCode.BallData, data, raiseEventOptions, SendOptions.SendUnreliable);
        }

        private void SetBallProperties(Vector3 position, Vector2 velocity)
        {
            transform.position = position;
            rb.velocity = velocity;
        }
    }
}