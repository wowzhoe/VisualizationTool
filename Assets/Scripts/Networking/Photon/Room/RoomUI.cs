using UnityEngine;

namespace VisualizationTool.Networking.Photon
{
    public class RoomUI : MonoBehaviour
    {
        public Room Room;

        public void Join()
        {
            Room.Join();
        }

        public void Leave()
        {
            Room.Leave();
        }
    }
}
