using Photon.Pun;
using UnityEngine;
using VisualizationTool.Controls;
using VisualizationTool.Platform;

namespace VisualizationTool.Character
{
    public class CharacterControls : MonoBehaviour
    {
        private WASD wasd;

        void Start()
        {
            wasd = new WASD();
        }

        // Update is called once per frame
        void Update()
        {
            switch (Platform.Platform.Instance.PlatformType)
            {
                case PlatformType.Standalone:
                    // TODO temporary usage will be removed in next sprint
                    if (GetComponent<PhotonView>().ViewID == 0 || GetComponent<PhotonView>().IsMine)
                    {
                        wasd.Move(transform);
                    }
                    break;
                case PlatformType.Oculus:
                    break;
            }
        }
    }
}
