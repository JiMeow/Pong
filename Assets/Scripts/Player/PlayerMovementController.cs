using UnityEngine;
using Photon.Pun;

namespace RunningFishes.Pong.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField]
        private float floorBorder;

        [SerializeField]
        private float ceilingBorder;

        private void Update()
        {
            if (PhotonNetwork.IsMasterClient) return;

            SetPlayerPosition();
        }

        private float GetMouseYPosition()
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return mousePosition.y;
        }

        private void SetPlayerPosition()
        {
            var yPosition = GetMouseYPosition();
            yPosition = Mathf.Clamp(yPosition, floorBorder, ceilingBorder);
            transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
        }
    }
}