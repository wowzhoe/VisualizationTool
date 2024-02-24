using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace VisualizationTool.Networking.Photon
{
    public class LobbyRoom : MonoBehaviourPunCallbacks
    {
        private Action<string> OnRoomButtonClicked;

        public string Name
        {
            get
            {
                return roomName;
            }
            set
            {
                roomName = value;
            }
        }
        [SerializeField] private string roomName; 

        public int RoomID
        {
            get
            {
                return roomID;
            }
            set
            {
                roomID = value;
            }
        }
        [SerializeField] private int roomID;

        public int RoomPlayerCount
        {
            get
            {
                return roomPlayerCount;
            }
            set
            {
                roomPlayerCount = value;
            }
        }
        [SerializeField] private int roomPlayerCount;

        [SerializeField] private LobbyRoomUI roomButton;


        private void Start()
        {
            roomButton = transform.GetComponentInChildren<LobbyRoomUI>();
            roomButton.AddOnClickListener(OnClickButtonHandler);
        }

        private void Update()
        {
            roomButton?.UpdateRoomCount(roomPlayerCount);
        }


        /// <summary>
        /// Init Lobby room. To animate Fast-In/Easy-out use ShowRoom(bool)
        /// </summary>
        /// <param name="roomName"></param>
        /// <param name="playersCount"></param>
        /// <param name="roomID"></param>
        /// <param name="enabledButton"></param>
        /// <param name="callBack"></param>
        public void Init(string roomName, int playersCount, int roomID, bool enabledButton, Action<string> callBack)
        {
            Name = roomName;
            RoomPlayerCount = playersCount;
            RoomID = roomID;
            OnRoomButtonClicked = callBack;

            roomButton.Init(roomName);
        }

        /// <summary>
        /// Enable animations
        /// </summary>
        /// <param name="isShow"></param>
        public void ShowRoom(bool isShow)
        {
            this.gameObject.SetActive(isShow);
        }

        /// <summary>
        /// Temporary create room with roomID
        /// TODO:Change to roomName
        /// </summary>
        private void OnClickButtonHandler()
        {
            if (OnRoomButtonClicked != null)
            {
                OnRoomButtonClicked(roomName);   
            }
        }
    }
}
