using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using RunningFishes.Pong.Constant;
using RunningFishes.Pong.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RunningFishes.Pong.Ball
{
    public class BallMovementController : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        public void Init()
        {
        }

        public void Dispose()
        {
        }

        public void StartGame()
        {
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

        private void Update()
        {
            // TODO: remove this this is debug
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(SceneName.Lobby);
            }
        }
    }
}