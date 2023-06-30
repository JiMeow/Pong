using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using RunningFishes.Pong.Constant;
using UnityEngine;
using UnityEngine.UI;

namespace RunningFishes.Pong.Lobby
{
    public class LobbyController : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private Text joinText;

        [SerializeField]
        private Text createText;

        private string gameSceneName = SceneName.Game;

        public void JoinRoom()
        {
            joinText.text = "Joining...";
            PhotonNetwork.JoinRoom(joinText.text);
        }

        public void CreateRoom()
        {
            createText.text = "Creating...";
            RoomOptions roomOptions = new RoomOptions { MaxPlayers = 2 };
            Hashtable roomProps = new Hashtable
            {
                { "Player1Score", 0 },
                { "Player2Score", 0 }
            };
            roomOptions.CustomRoomProperties = roomProps;
            PhotonNetwork.CreateRoom(createText.text, roomOptions);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            PhotonNetwork.LoadLevel(gameSceneName);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            joinText.text = "Join Fail";
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            createText.text = "Create Fail";
        }
    }
}