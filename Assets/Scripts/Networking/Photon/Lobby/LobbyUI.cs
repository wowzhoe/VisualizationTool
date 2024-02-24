using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace VisualizationTool.Networking.Photon
{
    public class LobbyUI : MonoBehaviour
    {
        [Tooltip("Prefab with UI room button")]
        [SerializeField] GameObject roomPrefab;

        [Tooltip("Grid parent for correct sorting")]
        [SerializeField] Transform roomsParent;

        /// <summary>
        /// Call when room button clicked
        /// </summary>
        public event Action<string> OnRoomClicked;
        public event Action OnRoomsCreated;

        private Dictionary<string, LobbyRoom> rooms = new Dictionary<string, LobbyRoom>();
        private Dictionary<string, RoomInfo> cache = new Dictionary<string, RoomInfo>();
         
        public List<string> roomsNameList = new List<string>();

        private void Start()
        {
            OnRoomsCreated += UpdateRoomFromCache;
        }

        private void OnDestroy()
        {
            OnRoomsCreated -= UpdateRoomFromCache;
        }

        /// <summary>
        /// Animate rooms
        /// </summary>
        /// <param name="show"></param>
        public void ShowRooms(bool show)
        {
            foreach (var room in rooms.Values)
            {
                room.ShowRoom(show);
            }
        }

        /// <summary>
        /// Fill rooms on Photon's callback from Lobby
        /// </summary>
        /// <param name="updatedRooms"></param>
        public void CacheRooms(List<RoomInfo> updatedRooms)
        {
            cache.Clear();
            foreach (var key in updatedRooms)
            {
                cache.Add(key.Name, key);
            }
        }

        /// <summary>
        /// Created empty rooms. Needs to fill from SetRooms
        /// </summary>
        public void CreateRooms()
        {
            StartCoroutine(CreateRoomsWithDelay(0.5f));
        }

        /// <summary>
        /// Delete visual part of rooms if max players are reached
        /// </summary>
        public void DeleteRooms(int maxPlayersPerRoom)
        {
            List<GameObject> delList = new List<GameObject>();

            if (rooms.Count > 0)
            {
                var keys = rooms.Keys;
                foreach (var key in keys.Where(key => rooms != null))
                {
                    LobbyRoom tmpRoom;
                    if (rooms.TryGetValue(key, out tmpRoom))
                    {
                        delList.Add(rooms[key].gameObject);
                        //rooms.Remove(key);
                    }
                }

                foreach (var d in delList)
                {
                    Destroy(d.gameObject);
                }

                if (rooms != null)
                {
                    rooms.Clear();
                }

                delList.Clear();
            }
        }

        /// <summary>
        /// Update rooms
        /// </summary>
        public void UpdateRoomByName(string name, RoomInfo room)
        {
            if (rooms != null)
            {
                LobbyRoom tmpRoom;
                if (rooms.TryGetValue(name, out tmpRoom))
                {
                    rooms[name].RoomPlayerCount = room.PlayerCount;
                }
            }
        }

        /// <summary>
        /// Update rooms from cache
        /// </summary>
        private void UpdateRoomFromCache()
        {
            if (cache != null)
            {
                foreach (var room in cache)
                {
                    UpdateRoomByName(room.Key, room.Value);
                }
            }
        }


        /// <summary>
        /// Created room with delay in seconds
        /// </summary>
        private IEnumerator CreateRoomsWithDelay(float delay)
        {
            for (int i = 0; i < Lobby.Config.maxRoomsPerLobby; i++)
            {
                yield return new WaitForSeconds(delay);

                if (roomsNameList.Count > 0)
                {
                    string name = roomsNameList[i];
                    CreateRoom(name, i, 0, Lobby.Config.roomOptions.MaxPlayers, true);
                }
                else
                {
                    CreateRoom("Room" + i, i, 0, Lobby.Config.roomOptions.MaxPlayers, true);
                }
            }

            if (OnRoomsCreated != null)
            {
                OnRoomsCreated();
            }
        }

        /// <summary>
        /// Clear rooms before setup
        /// </summary>
        private void SetRoomsEmpty()
        {
            var keys = rooms.Keys;
            foreach (var key in keys)
            {
                rooms[key].RoomPlayerCount = 0;
            }
        }

        /// <summary>
        /// Create room with parameters
        /// </summary>
        private void CreateRoom(string roomName, int roomId, int playersCount, int maxPlayers, bool isActive = true)
        {
            if (rooms.Count <= Lobby.Config.maxRoomsPerLobby)
            {
                var tempRoom = Instantiate(roomPrefab, roomsParent);
                var tmpLobbyRoom = tempRoom.GetComponent<LobbyRoom>();
                tmpLobbyRoom.Init(roomName, playersCount, roomId, isActive, OnRoomButtonClickedHandler);
                rooms.Add(roomName, tmpLobbyRoom);
            }
        }

        /// <summary>
        /// Callback from room button
        /// </summary>
        private void OnRoomButtonClickedHandler(string roomName)
        {
            if (OnRoomClicked != null)
            {
                OnRoomClicked(roomName);
            }
        }
    }
}
