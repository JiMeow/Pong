using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using RunningFishes.Pong.Gameplay;
using RunningFishes.Pong.Multiplayer;
using System.Collections;
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player1Base"))
            {
                GameController.instance.ScoreController.Player2Score++;
                // TODO: make state in game for reset ballpropeties
                ResetBallProperties();
                // TODO: make state in game for start new game (new score game not end yet)
                StartCoroutine(StartNewGameCoroutine());
            }
            if (collision.gameObject.CompareTag("Player2Base"))
            {
                GameController.instance.ScoreController.Player1Score++;
                // TODO: make state in game for reset ballpropeties
                ResetBallProperties();
                // TODO: make state in game for start new game (new score game not end yet)
                StartCoroutine(StartNewGameCoroutine());
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

                // increase velocity for 1.1f per bounce with paddle
                rb.velocity *= 1.1f;
                speed *= 1.1f;

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

        private void ResetBallProperties()
        {
            transform.position = new Vector3(0, 0, -1);
            rb.velocity = Vector2.zero;
        }

        private IEnumerator StartNewGameCoroutine()
        {
            yield return new WaitForSeconds(1f);
            gameObject.GetComponent<BallMovementController>().StartGame();
        }
    }
}