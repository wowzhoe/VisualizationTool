using Photon.Pun;
using UnityEngine;

namespace VisualizationTool.Tools.Actions
{
    [RequireComponent(typeof(Tool))]
    public class ActionShowAll : Action
    {
        public override void Trigger()
        {
            base.Trigger();
            View.RPC("RPC_UnhideAll", RpcTarget.AllBuffered);
        }

        [PunRPC]
        void RPC_UnhideAll()
        {
            Tool tool = transform.root.GetComponent<Tool>();
            for (int i = 0; i < tool.children.Count; i++)
            {
                Tool child = tool.children[i];
                child.GetComponent<ActionShowAll>()?.Trigger();
            }
        }
    }
}