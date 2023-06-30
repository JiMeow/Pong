using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace RunningFishes.Pong.Players
{
    public class PlayerController : MonoBehaviourPunCallbacks
    {
        public GameObject playerPaddle;
        private bool isInit = false;

        public void Init()
        {
            if (isInit) return;

            isInit = true;

            CreateCurrentPlayer();
        }

        public void Dispose()
        {
            if (!isInit) return;

            isInit = false;

            DestroyCurrentPlayer();
        }

        private void CreateCurrentPlayer()
        {
            GameObject player = (GameObject)Resources.Load("Player");
            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            switch (playerCount)
            {
                case 1:
                    playerPaddle = PhotonNetwork.Instantiate(player.name, new Vector3(-7.5f, 0, -1), Quaternion.identity);
                    break;

                case 2:
                    playerPaddle = PhotonNetwork.Instantiate(player.name, new Vector3(7.5f, 0, -1), Quaternion.identity);
                    break;

                default:
                    break;
            }
        }

        private void DestroyCurrentPlayer()
        {
            if (playerPaddle == null) return;

            PhotonNetwork.Destroy(playerPaddle);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            if (playerPaddle == null) return;

            playerPaddle.transform.position = new Vector3(-7.5f, 0, -1);
        }
    }
}