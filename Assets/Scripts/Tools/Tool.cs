using UnityEngine;
using VisualizationTool.Networking.Photon;
using VisualizationTool.Core.Physics;
using VisualizationTool.Tools.Actions.Oculus;
using VisualizationTool.Utils;
using VisualizationTool.Tools.Node;

namespace VisualizationTool.Tools
{
    public class Tool : ToolBase
    {
        public ToolBase Base
        {
            get
            {
                return (ToolBase)this;
            }
        }

        new void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// Evaluate parent & children components of parent ( or linked ) tool to fit children list
        /// </summary>
        public override void EvaluateChildrenAndSelf()
        {
            Initialize(this.transform);
            base.EvaluateChildrenAndSelf();
        }

        /// <summary>
        /// Evaluate children components of parent ( or linked ) tool to fit children list
        /// </summary>
        /// <param name="tool"></param>
        public override void EvaluateChildren(ToolBase tool)
        {
            Initialize(this.transform);
            base.EvaluateChildren(tool);
        }

        /// <summary>
        /// Pass transform of gameobject for instantiation of tools components on it
        /// </summary>
        /// <param name="trans"></param>
        public void Initialize(Transform trans)
        {
            Tool tool = trans.AddOrGetComponent<Tool>();
            ToolBase parent = trans.parent != null ? trans.parent.AddOrGetComponent<Tool>().Base : tool.Base;
            base.parentPart = parent;

            ViewConfig vc = new ViewConfig(trans.gameObject);

            Rigidbody rigidbody = trans.AddOrGetComponent<Rigidbody>();
            RigidbodyConfig rc = new RigidbodyConfig(rigidbody);        

            Node<MeshRenderer> node = new Node<MeshRenderer>(trans.gameObject);

            ActionGrab grab = trans.AddOrGetComponent<ActionGrab>();
            grab.Initialize(true);
            grab.SetgrabPoints(tool.colliders.ToArray());

            Actions.Actions actions = new Actions.Actions(trans.gameObject);
        }
    }
}