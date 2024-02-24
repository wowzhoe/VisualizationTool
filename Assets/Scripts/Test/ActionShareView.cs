using Photon.Pun;
using UnityEngine;

namespace VisualizationTool.Tools.Actions
{
    public class ActionShareView : Action
    {
        protected override void Initialize()
        {

        }

        [PunRPC]
        void RPC_ShareView(Vector3 pos, Vector3 rot)
        {
            FreeFlyCamera2[] userList = FindObjectsOfType<FreeFlyCamera2>();
            foreach (FreeFlyCamera2 userComponent in userList)
            {
                userComponent.transform.position = pos;
                userComponent.transform.eulerAngles = new Vector3(rot.x, rot.y, 0);
            }
        }
    }
}