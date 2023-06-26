using Photon.Pun;
using RunningFishes.Pong.Constant;
using UnityEngine.SceneManagement;

namespace RunningFishes.Pong.Initialize
{
    public class Initialize : MonoBehaviourPunCallbacks
    {
        private string lobbySceneName = SceneName.Lobby;

        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            SceneManager.LoadScene(lobbySceneName);
        }
    }
}