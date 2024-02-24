using Photon.Pun;
using UnityEngine;
using VisualizationTool.Character;
using VisualizationTool.Tools.Actions;

namespace VisualizationTool.Tools
{
    public class ToolInteractionUI : ToolInteraction
    {
        public GameObject ToolMenu;

        // Start is called before the first frame update
        new void Start()
        {
            base.Start();
            base.Bridge.ToolMenuEnabled.AddListener(Activate);
        }

        new void OnDestroy()
        {
            base.OnDestroy();
            base.Bridge.ToolMenuEnabled.RemoveListener(Activate);
        }

        /// <summary>
        /// Callback from platform specific buttons / triggers on UI to Activate it (OnClick)
        /// </summary>
        /// <param name="enable"></param>
        public void Activate(bool enable)
        {
            ToolMenu.SetActive(enable);
        }

        /// <summary>
        /// Callback from platform specific buttons / triggers for Show Select (OnClick)
        /// </summary>
        public void OnClickShowSelect()
        {
            base.actions?.Trigger(ActionType.ShowSelect);
        }

        /// <summary>
        /// Callback from platform specific buttons / triggers for Hide Select (OnClick)
        /// </summary>
        public void OnClickHideSelect()
        {
            base.actions?.Trigger(ActionType.SelectHide);
        }

        /// <summary>
        /// Callback from platform specific buttons / triggers for Increase Select (OnClick)
        /// </summary>
        public void OnClickIncreaseSelect()
        {
            Transform root = base.tool.transform.root;
            base.tool = root.GetComponent<Tool>();
            base.actions = tool.GetComponent<Actions.Actions>();
        }

        /// <summary>
        /// Callback from platform specific buttons / triggers for Disable Select (OnClick)
        /// </summary>
        public void OnClickUnSelect()
        {
            base.actions?.Trigger(ActionType.SelectDisable);
        }

        /// <summary>
        /// Callback from platform specific buttons / triggers for Show All (OnClick)
        /// </summary>
        public void OnClickShowAll()
        {
            base.actions?.Trigger(ActionType.ShowAll);
        }

        /// <summary>
        /// Callback from platform specific buttons / triggers for Share View (OnClick)
        /// </summary>
        public void OnClickShareView()
        {
            transform.root.GetComponent<CharacterAdjustment>().
            GetComponent<PhotonView>().
            RPC("RPC_ShareView", RpcTarget.AllBuffered, 
            transform.position, transform.rotation.eulerAngles);
        }
    }
}