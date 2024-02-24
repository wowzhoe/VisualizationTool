using UnityEngine;

namespace VisualizationTool.Tools
{
    public class ToolInteraction : MonoBehaviour
    {
        public ToolInteractionBridge Bridge;
        public Tool AssignedTool
        {
            get
            {
                return tool;
            }
        }
        public Tool tool;

        public Actions.Actions actions;

        public void Start()
        {
            Bridge = transform.GetComponentInChildren<ToolInteractionBridge>();
            Bridge.ToolAssigned.AddListener(ToolAssigned);
        }

        public void OnDestroy()
        {
            Bridge.ToolAssigned.RemoveListener(ToolAssigned);
        }

        /// <summary>
        /// Pass selected tool (component of gameobject ) to assign for actions
        /// </summary>
        /// <param name="selectedTool"></param>
        public void ToolAssigned(Tool selectedTool)
        {
            tool = selectedTool;
            actions = tool.GetComponent<Actions.Actions>();
        }
    }
}