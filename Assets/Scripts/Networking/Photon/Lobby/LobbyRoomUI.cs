using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VisualizationTool.Networking.Photon
{
    public class LobbyRoomUI : MonoBehaviour
    {
        public Text RoomName;
        public Text RoomCount;

        public UnityEvent onClick;

        public void AddOnClickListener(UnityAction onClickAction)
        {
            onClick.AddListener(onClickAction);
        }

        public void RemoveAllListeners()
        {
           onClick.RemoveAllListeners();
        }

        public void OnClick()
        {
            onClick.Invoke();
        }

        public void Init(string roomName)
        {
            RoomName.text = roomName;
        }

        public void UpdateRoomCount(int roomPlayersCount)
        {
            RoomCount.text = roomPlayersCount.ToString();
        }
    }
}
