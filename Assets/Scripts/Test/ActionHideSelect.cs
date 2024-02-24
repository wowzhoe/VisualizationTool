using System;
using Photon.Pun;
using UnityEngine;

namespace VisualizationTool.Tools.Actions
{
    [RequireComponent(typeof(Tool))]
    public class ActionHideSelect : Action
    {
        public override void Trigger()
        {
            base.Trigger();
            Tool tool = GetComponent<Tool>();
            View.RPC("RPC_HideSelectedNode", RpcTarget.AllBuffered, tool.id);
        }

        [PunRPC]
        void RPC_HideSelectedNode(string id)
        {
            Tool tool = GetComponent<Tool>();
            for (int i = 0; i < tool.children.Count; i++)
            {
                Tool child = tool.children[i];
                if (child.id == Guid.Parse(id))
                {
                    child.GetComponent<ActionHideSelect>()?.Trigger();
                }
            }
        }
    }
}