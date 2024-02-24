using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace VisualizationTool.Networking.Photon
{
    public class LobbyConfig
    {
        private bool isVisible = true;
        private bool isOpen = true;
        private bool isOfflineMode = false;
        private string gameVersion = "1";
        private int maxPlayersPerRoom = 4;
        private int _maxRoomsPerLobby = 8;
        public int maxRoomsPerLobby
        {
            get
            {
                return _maxRoomsPerLobby;
            }
        }
        public RoomOptions roomOptions;

        public LobbyConfig()
        {
            roomOptions = new RoomOptions()
            {
                IsVisible = isVisible,
                IsOpen = isOpen,
                MaxPlayers = (byte)maxPlayersPerRoom
            };

            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.OfflineMode = isOfflineMode;
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
            else
            {
                //Debug.Log("We are connected already.");
            }
        }

        public void Init() { }
    }
}