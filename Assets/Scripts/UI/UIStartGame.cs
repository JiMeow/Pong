using Photon.Pun;
using RunningFishes.Pong.State;
using UnityEngine;
using UnityEngine.UI;

namespace RunningFishes.Pong.UI
{
    public class UIStartGame : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        private StateController stateController;

        public void Init(StateController stateController)
        {
            if (!PhotonNetwork.IsMasterClient) button.gameObject.SetActive(false);

            this.stateController = stateController;
            button.onClick.AddListener(OnStartGame);
        }

        public void Dispose()
        {
            stateController = null;
        }

        private void OnStartGame()
        {
            stateController.RaiseStateChanged(States.Playing);
            gameObject.SetActive(false);
        }
    }
}