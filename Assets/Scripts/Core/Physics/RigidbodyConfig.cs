using UnityEngine;

namespace VisualizationTool.Core.Physics
{
    public class RigidbodyConfig
    {
        private bool isKinematic = true;
        private bool useGravity = false;

        /// <summary>
        /// Pass rigidbody of gameobject for constructor initialization
        /// </summary>
        /// <param name="rigidbody"></param>
        public RigidbodyConfig(Rigidbody rigidbody)
        {
            rigidbody.isKinematic = isKinematic;
            rigidbody.useGravity = useGravity;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
