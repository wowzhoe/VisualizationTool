using UnityEngine;

namespace VisualizationTool.Interaction
{
    /// <summary>
    /// Interaction Dummy Body class for sync headset and controllers with dummy head / hands
    /// </summary>
    public class InteractionBody : MonoBehaviour
    {
        //From this transforms
        [SerializeField] private Transform _OVRHead;
        [SerializeField] private Transform _OVRHand_Left;
        [SerializeField] private Transform _OVRHand_Right;

        //To this transforms
        [SerializeField] private Transform _avatarHead;
        [SerializeField] private Transform _avatarHand_Left;
        [SerializeField] private Transform _avatarHand_Right;

        private void Start()
        {
            _avatarHead = transform.root.GetComponent<Character.CharacterJoint>().HeadJoint;
            _avatarHand_Left = transform.root.GetComponent<Character.CharacterJoint>().LeftHandJoint;
            _avatarHand_Right = transform.root.GetComponent<Character.CharacterJoint>().RightHandJoint;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_OVRHead != null)
            {
                _avatarHead.position = _OVRHead.position;
                _avatarHead.rotation = _OVRHead.rotation;
            }

            if (_OVRHand_Left != null)
            {
                _avatarHand_Left.position = _OVRHand_Left.position;
                _avatarHand_Left.rotation = _OVRHand_Left.rotation;
            }

            if (_OVRHand_Right != null)
            {
                _avatarHand_Right.position = _OVRHand_Right.position;
                _avatarHand_Right.rotation = _OVRHand_Right.rotation;
            }
        }
    }
}