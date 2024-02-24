using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace VisualizationTool.Networking.Photon
{
    public class Lobby : MonoBehaviourPunCallbacks
    {
        public LobbyUI lobbyUI;
        private bool isRoomClicked;

        public static LobbyConfig Config
        {
            get
            {
                return new LobbyConfig();
            }
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.JoinLobby(TypedLobby.Default);
        }

        public override void OnJoinedLobby()
        {
            Debug.Log("Joined Lobby");
            //TODO call create rooms in test purpose
            lobbyUI.CreateRooms();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            lobbyUI.CacheRooms(roomList);
            foreach (RoomInfo room in roomList)
            {
                RoomReceived(room);
            }
        }

        private void RoomReceived(RoomInfo room)
        {
            lobbyUI.UpdateRoomByName(room.Name, room);
        }

        private void Start()
        {
            Config.Init();
            lobbyUI.OnRoomClicked += LobbyOnRoomClickedHandler;
        }

        private void OnDestroy()
        {
            lobbyUI.OnRoomClicked -= LobbyOnRoomClickedHandler;
        }

        /// <summary>
        /// Callback from room buttons (OnClick)
        /// </summary>
        /// <param name="roomName"></param>
        private void LobbyOnRoomClickedHandler(string roomName)
        {
            //_roomToJoin = roomName;

            if (PhotonNetwork.IsConnectedAndReady)
            {
                JoinOrCreateRoom(roomName);
            }
            else
            {
                if (!PhotonNetwork.IsConnected)
                {
                    LogPhotonStateAndReconnect();
                }
            }
        }

        private void LogPhotonStateAndReconnect()
        {
            bool success;

            if (PhotonNetwork.NetworkClientState == ClientState.JoinedLobby)
            {
                success = PhotonNetwork.ReconnectAndRejoin();
            }
            else
            {
                success = PhotonNetwork.Reconnect();
            }
        }

        /// <summary>
        /// Callback for room creation with name  (OnClick)
        /// </summary>
        /// <param name="roomName"></param>
        private void JoinOrCreateRoom(string roomName)
        {
            isRoomClicked = true;
            PhotonNetwork.JoinOrCreateRoom(roomName, Config.roomOptions, TypedLobby.Default);
        }

        /// <summary>
        /// On disconnected from photon
        /// </summary>
        public void OnDisconnectedFromPhoton()
        {
            //_numConnectionAttempts = 0;
            Debug.LogWarning("Lobby.OnDisconnectedFromPhoton: was called by PUN");
        }

        /// <summary>
        /// On joined current room
        /// </summary>
        public override void OnJoinedRoom()
        {
            Debug.Log("Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
            // Note: 'manually' load the appropriate scene if we are the first player to join 
            // the networked room.  Otherwise, we can rely on PhotonNetwork.automaticallySyncScene
            // to sync our instance scene.
            PhotonNetwork.LoadLevel(1);
        }

        /// <summary>
        /// On joined current room failed
        /// </summary>
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

            // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
            //PhotonNetwork.CreateRoom(null, new RoomOptions());
        }

        /// <summary>
        /// On left current room
        /// </summary>
        public override void OnLeftRoom()
        {
            PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        }
    }
}
