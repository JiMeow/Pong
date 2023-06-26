using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private GameObject playerPaddle;

    public void Init()
    {
        CreateCurrentPlayer();
    }

    public void Dispose()
    {
        DestroyCurrentPlayer();
    }

    private void CreateCurrentPlayer()
    {
        GameObject player = (GameObject)Resources.Load("Player");
        int playerCount = PhotonNetwork.CountOfPlayersInRooms;
        switch (playerCount)
        {
            case 0:
                playerPaddle = PhotonNetwork.Instantiate(player.name, new Vector3(-7.5f, 0, -1), Quaternion.identity);
                break;

            case 1:
                playerPaddle = PhotonNetwork.Instantiate(player.name, new Vector3(7.5f, 0, -1), Quaternion.identity);
                break;

            default:
                break;
        }
    }

    private void DestroyCurrentPlayer()
    {
        PhotonNetwork.Destroy(playerPaddle);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int playerCount = PhotonNetwork.CountOfPlayersInRooms;
        switch (playerCount)
        {
            case 1:
                playerPaddle.transform.position = new Vector3(-7.5f, 0, -1);
                break;

            case 2:
                playerPaddle.transform.position = new Vector3(7.5f, 0, -1);
                break;

            default:
                break;
        }
    }
}