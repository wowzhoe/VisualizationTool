using UnityEngine;
using VisualizationTool.Interaction;

namespace VisualizationTool.Tools.Actions.Oculus
{
    /// <summary>
    /// An object that can be grabbed and thrown by OVRGrabber.
    /// </summary>
    public class ActionGrab : OVRGrabbable
    {
        public bool isGrabable = false;
        protected new InteractionGrab m_grabbedBy = null;

        private bool AllowOffhandGrab
        {
            get
            {
                return m_allowOffhandGrab;
            }
            set
            {
                m_allowOffhandGrab = value;
            }
        }

        public void Initialize(bool allowOffHandGrab)
        {
            AllowOffhandGrab = allowOffHandGrab;
        }

        public void SetgrabPoints(Collider[] colliders)
        {
            m_grabPoints = colliders;
        }

        /// <summary>
        /// Returns the OVRGrabber currently grabbing this object.
        /// </summary>
        public new InteractionGrab grabbedBy
        {
            get
            {
                return m_grabbedBy;
            }
        }

        /// <summary>
        /// Notifies the object that it has been grabbed.
        /// </summary>
        virtual public void GrabBegin(InteractionGrab hand, Collider grabPoint)
        {
            m_grabbedBy = hand;
            m_grabbedCollider = grabPoint;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}