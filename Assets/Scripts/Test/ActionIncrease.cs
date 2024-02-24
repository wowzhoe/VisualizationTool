using Photon.Pun;
using UnityEngine;

namespace VisualizationTool.Tools.Actions
{
    [RequireComponent(typeof(Tool))]
    public class ActionIncrease : Action
    {
        public override void Trigger()
        {
            base.Trigger();
            View.RPC("RPC_Increase", RpcTarget.AllBuffered, base.tool.id);
        }

        [PunRPC]
        void RPC_Increase(string id)
        {
            Tool tool = transform.root.GetComponent<Tool>();
            for (int i = 0; i < tool.children.Count; i++)
            {
                Tool child = tool.children[i];
                child.GetComponent<ActionIncrease>()?.Trigger();
            }
        }
    }
}