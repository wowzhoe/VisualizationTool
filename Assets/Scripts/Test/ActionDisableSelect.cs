using Photon.Pun;
using UnityEngine;

namespace VisualizationTool.Tools.Actions
{
    [RequireComponent(typeof(Tool))]
    public class ActionDisableSelect : Action
    {
        public override void Trigger()
        {
            base.Trigger();
            View.RPC("RPC_UnselectAll", RpcTarget.AllBuffered);
        }

        [PunRPC]
        public void RPC_UnselectAll()
        {
            Tool tool = GetComponent<Tool>();
            for (int i = 0; i < tool.children.Count; i++)
            {
                Tool child = tool.children[i];
                child.GetComponent<ActionDisableSelect>()?.Trigger();
            }
        }
    }
}