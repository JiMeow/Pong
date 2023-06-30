using Photon.Pun;
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
            PhotonNetwork.CreateRoom(createText.text, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
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