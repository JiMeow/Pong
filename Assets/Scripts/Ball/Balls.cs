using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using RunningFishes.Pong.Gameplay;
using RunningFishes.Pong.Multiplayer;
using RunningFishes.Pong.State;
using System.Collections;
using UnityEngine;

namespace RunningFishes.Pong.Ball
{
    public class Balls : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        private BallController ballController;
        private StateController stateController;

        private bool isSubscribed = false;

        private float speedIncreasedConstant = 1f;

        public void Init(BallController ballController, StateController stateController)
        {
            this.ballController = ballController;
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
            GameObject myPaddle = GameController.instance.PlayerController.playerPaddle;
            if (collision.gameObject.CompareTag("Player1Base"))
            {
                if (myPaddle.gameObject.transform.position.x > 0) return;

                GameController.instance.ScoreController.Player2Score++;

                ResetBallProperties();
                StartCoroutine(StartNewGameCoroutine());
            }

            if (collision.gameObject.CompareTag("Player2Base"))
            {
                if (myPaddle.gameObject.transform.position.x < 0) return;

                GameController.instance.ScoreController.Player1Score++;

                ResetBallProperties();
                StartCoroutine(StartNewGameCoroutine());
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var photonView = collision.gameObject.GetPhotonView();
                if (!photonView.IsMine) return;

                bool isMine = collision.gameObject.GetPhotonView().IsMine;
                if (!isMine) return;

                Vector2 speed = rb.velocity;
                Vector3 position = transform.position;

                // increase speedIncreasedConstant for 1.1f per bounce with paddle
                rb.velocity = rb.velocity / speedIncreasedConstant;
                speedIncreasedConstant *= 1.1f;
                if (speedIncreasedConstant > 2.5f)
                {
                    speedIncreasedConstant = 2.5f;
                }
                rb.velocity = rb.velocity * speedIncreasedConstant;

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
            speedIncreasedConstant = 1f;
            stateController.RaiseStateChanged(States.Playing);
        }
    }
}