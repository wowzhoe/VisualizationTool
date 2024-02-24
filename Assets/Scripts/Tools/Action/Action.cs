using Photon.Pun;
using UnityEngine;

namespace VisualizationTool.Tools.Actions
{
    [RequireComponent(typeof(PhotonView))]
    [System.Serializable]
    public class Action : MonoBehaviour
    {
        public Tool tool;
        public PhotonView View
        {
            get { return photonView; }
        }
        private PhotonView photonView;

        // Start is called before the first frame update
        private void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            photonView = GetComponent<PhotonView>();
            tool = GetComponent<Tool>();
        }

        public virtual void Trigger() { }

        /// <summary>
        /// Callback from platform specific buttons / triggers with actionType specific (OnClick)
        /// </summary>
        /// <param name="actionType"></param>
        public virtual void Trigger(ActionType actionType) { }
    }
}