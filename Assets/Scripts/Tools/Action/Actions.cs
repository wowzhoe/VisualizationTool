using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using VisualizationTool.Tools.Actions.Oculus;
using VisualizationTool.Utils;

namespace VisualizationTool.Tools.Actions
{
    [RequireComponent(typeof(Tool))]
    public class Actions : Action
    {
        /// <summary>
        /// Pass gameobject as parent to constructor for inialization
        /// </summary>
        /// <param name="obj"></param>
        public Actions(GameObject obj)
        {
            obj.AddOrGetComponent<Actions>();
        }

        public List<ActionType> ActionTypes = new List<ActionType>();

        /// <summary>
        /// Callback from platform specific buttons / triggers on tool to Activate Action on it (OnClick)
        /// </summary>
        /// <param name="actionType"></param>
        public override void Trigger(ActionType actionType)
        {
            Debug.Log("actionType : " + actionType);
            if (ActionTypes.Contains(actionType))
            {
                switch (actionType)
                {
                    case ActionType.SelectDisable:
                        base.View.RPC("RPC_UnselectAll", RpcTarget.AllBuffered);
                        break;
                    case ActionType.SelectHide:
                        base.View.RPC("RPC_HideSelectedNode", RpcTarget.AllBuffered, base.tool.id.ToString());
                        break;
                    case ActionType.SelectIncrease:
                        base.View.RPC("RPC_SelectPart", RpcTarget.AllBuffered, base.tool.id.ToString());
                        break;
                    case ActionType.ShareView:
                        break;
                    case ActionType.ShowAll:
                        base.View.RPC("RPC_UnhideAll", RpcTarget.AllBuffered);
                        break;
                    case ActionType.ShowSelect:
                        base.View.RPC("RPC_ShowSelect", RpcTarget.AllBuffered, base.tool.id.ToString());
                        break;
                }
            }
        }

        [PunRPC]
        public void RPC_UnselectAll()
        {
            List<Tool> tools = Finder.FindOfTypeList<Tool>();

            for (int i = 0; i < tools.Count; i++)
            {
                if (Customization.GetColor(tools[i].gameObject, Color.red))
                {
                    Customization.SetColor(tools[i].gameObject, Color.white);
                }

                tools[i].gameObject.GetComponent<ActionGrab>().isGrabable = false;
            }

            Customization.UnHide(tool.gameObject);
        }

        [PunRPC]
        public void RPC_HideSelectedNode(string id)
        {
            Tool tool = GetComponent<Tool>();
            tool.GetComponent<ActionGrab>().isGrabable = false;
            Customization.Hide(tool.gameObject);
        }

        [PunRPC]
        public void RPC_SelectPart(string id)
        {
            Tool tool = transform.root.GetComponent<Tool>();

            if (tool.children.Count > 0)
            {
                for (int i = 0; i < tool.children.Count; i++)
                {
                    Tool child = tool.children[i];
                }
            }
        }

        [PunRPC]
        public void RPC_UnhideAll()
        {
            base.View.RPC("RPC_UnselectAll", RpcTarget.AllBuffered);

            Tool tool = transform.root.GetComponent<Tool>();
            Customization.UnHide(tool.gameObject);

            if (tool.children.Count > 0)
            {
                for (int i = 0; i < tool.children.Count; i++)
                {
                    Tool child = tool.children[i];
                    Customization.UnHide(child.gameObject);
                }
            }
        }

        [PunRPC]
        public void RPC_ShowSelect(string id)
        {
            /*
            List<Tool> tools = Finder.FindOfTypeList<Tool>();

            for (int i = 0; i < tools.Count; i++)
            {
                if (tools[i] != base.tool)
                {
                    Customization.Hide(tools[i].gameObject);
                }

                tools[i].gameObject.GetComponent<ActionGrab>().isGrabable = false;
            }
            */

            Tool tool = GetComponent<Tool>();
            Customization.SetColor(tool.gameObject);

            if (tool.children.Count > 0)
            {
                for (int i = 0; i < tool.children.Count; i++)
                {
                    Tool child = tool.children[i];
                    Customization.SetColor(child.gameObject);
                }
            }
        }
    }
}