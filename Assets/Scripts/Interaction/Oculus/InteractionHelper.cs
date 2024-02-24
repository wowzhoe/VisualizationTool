using Photon.Pun;
using UnityEngine;

namespace VisualizationTool.Interaction
{
    public class InteractionHelper : MonoBehaviour
    {
        public Canvas canvas;

        // Start is called before the first frame update
        private void Awake()
        {
            Transform root = transform.root;
            PhotonView view = root.GetComponent<PhotonView>();

            if (view.IsMine)
            {
                Debug.Log("transform.parent.gameObject : " + transform.root.gameObject);
//                GameObject rootParent = transform.parent.gameObject;
//                GameObject firstChild = rootParent.transform.GetChild(0).gameObject; //controller
//                GameObject ovrChild = firstChild.transform.GetChild(0).gameObject; //ovr
//                GameObject ovrSecondChild = ovrChild.transform.GetChild(1).gameObject; // OVRCameraRig
//                GameObject ovrThirdChild = ovrSecondChild.transform.GetChild(0).gameObject; // TrackingSpace
//                GameObject eyeAnchor = ovrThirdChild.transform.GetChild(1).gameObject;
//
//                if (eyeAnchor != null)
//                {
//                    canvas.renderMode = RenderMode.WorldSpace;
//                    canvas.worldCamera = eyeAnchor.GetComponent<Camera>();
//                    canvas.gameObject.SetActive(true);
//                    Debug.Log(eyeAnchor.name);
//                }
            }
        }
    }
}