using Photon.Pun;
using UnityEngine;

namespace VisualizationTool.Tools
{
    public class ToolsUI : MonoBehaviourPunCallbacks
    {
        public GameObject UIMenu;
        public ToolInteractionBridge Bridge;
        private Tool lastTool;

        // Start is called before the first frame update
        void Start()
        {
            //Bridge.ToolAssignedEvent.AddListener(Select);
        }

        void OnDestroy()
        {
            //Bridge.ToolAssignedEvent.RemoveListener(Select);
        }

        public void Select(Tool tool)
        {
            lastTool = tool;
        }



        public void ShareView()
        {
            // teleport all other players to HAND position.
        }

        public void ShowAll()
        {
            // only show last instrument and his parts to another ppl ( visible )
        }

        public void InscreaseSelection()
        {
            // inscrease selection ( added parent object to the selection part ) and whole selected instrument will be highlighted
        }

        public void Deselect()
        {
            // for everyone disable selection of instrument
        }

        public void ShowSelect()
        {
            // select something, only show selected part ( disable everything for everyone)
        }

        public void Hide()
        {
            // selected part will be hidden
        }
    }
}