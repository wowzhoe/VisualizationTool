using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using VisualizationTool.Character;
using VisualizationTool.Character.Internal;
using VisualizationTool.Core;

namespace VisualizationTool.Networking.Photon
{
    public class Room : MonoBehaviourPunCallbacks
    {
        private bool inRoom = false;

        // Start is called before the first frame update
        void Start()
        {
            Join();
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }

        /// <summary>
        /// Called when the player joined the room. We need to instantiate player prefab.
        /// </summary>
        public override void OnJoinedRoom()
        {
            Join();
        }

        public void Join()
        {
            if (!inRoom)
            {
                inRoom = true;

                GameObject obj = PhotonNetwork.Instantiate(AddressableNames.Character, transform.position, Quaternion.identity, 0);
                CharacterConfig cc = new CharacterConfig(CharacterRole.User, (CharacterType)Platform.Platform.Instance.PlatformType);
                CharacterFactory cf = new CharacterFactory();
                Character.Character character = cf.Create(cc, false, obj);
            }
        }

        public void Leave()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
