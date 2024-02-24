using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class CreateNewRoom : MonoBehaviourPunCallbacks //MonoBehaviourPun, 
{
    [SerializeField]
    private Text _roomName;
    private Text RoomName
    {
        get { return _roomName; }
    }

    public void OnClick_CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };

        // PhotonNetwork.JoinOrCreateRoom ("RoomName", roomOptions, null);

        if (PhotonNetwork.JoinOrCreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
        {
            print("Create room successfully");
        }
        else
        {
            print("Create room failed");
        }


//        if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
//        {
//            print("Create room successfully");
//        }
//        else
//        {
//            print("Create room failed");
//        }
    }

    private void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        print("Create Room Failed: " + codeAndMessage[1]);

    }

    private void OnCreatedRoom(short returnCode, string message)
    {
        print("Room Created Successfully");
    }

    public override void OnJoinedRoom()
    {
        print("Room Joined Successfully");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(message + returnCode);
        Debug.Log(" failed to join room game");
    }
}