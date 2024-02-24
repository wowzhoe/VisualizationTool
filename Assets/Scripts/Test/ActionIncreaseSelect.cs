using Photon.Pun;
using UnityEngine;

namespace VisualizationTool.Tools.Actions
{
    [RequireComponent(typeof(Tool))]
    public class ActionIncreaseSelect : Action
    {
        public override void Trigger()
        {
            base.Trigger();
            Tool tool = GetComponent<Tool>();
            View.RPC("RPC_SelectPart", RpcTarget.AllBuffered, tool.id);
        }

        [PunRPC]
        void RPC_SelectPart(string id)
        {
            Tool tool = transform.root.GetComponent<Tool>();
            for (int i = 0; i < tool.children.Count; i++)
            {
                Tool child = tool.children[i];
                child.GetComponent<ActionShowSelect>()?.Trigger();
            }
        }
    }
}