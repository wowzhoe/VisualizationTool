using UnityEngine;

namespace VisualizationTool.Character
{
    /// <summary>
    /// Character Joints for assign UI or other objects via factory
    /// </summary>
    public class CharacterJoint : MonoBehaviour
    {
        public Transform HeadJoint = null;
        public Transform BodyJoint = null;

        public Transform LeftArmJoint = null;
        public Transform RightArmJoin = null;

        public Transform LeftHandJoint = null;
        public Transform RightHandJoint = null;

        public Transform LeftLegJoint = null;
        public Transform RightLegJoint = null;
    }
}
