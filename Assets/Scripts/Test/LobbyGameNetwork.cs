using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyGameNetwork : MonoBehaviourPunCallbacks
{
    public static string gameVersion = "1";

    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.OfflineMode = false;
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("We are connected already.");
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = false;
        //PhotonNetwork.NickName = PlayerGameNetwork.Instance.Name;

        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        print("Joined Lobby");
        if (!PhotonNetwork.InRoom)
        {
            CanvasManager.Instance.LobbyFunction.transform.SetAsLastSibling();
        }
    }
}