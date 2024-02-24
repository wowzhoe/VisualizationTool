﻿using System.Collections.Generic;
using UnityEngine;
using VisualizationTool.Tools;
using VisualizationTool.Tools.Actions.Oculus;

namespace VisualizationTool.Interaction
{
    /// <summary>
    /// Pass to Hand for Interaction For OVR Grabbing config
    /// </summary>
    [RequireComponent(typeof (Rigidbody))]
    public class InteractionGrab : MonoBehaviour
    {
        public float grabBegin = 0.55f;
        public float grabEnd = 0.35f;

        private bool alreadyUpdated = false;

        [SerializeField] protected bool m_parentHeldObject = false;
        [SerializeField] protected bool m_moveHandPosition = false;
        [SerializeField] protected Transform m_gripTransform = null;
        [SerializeField] protected Collider[] m_grabVolumes = null;
        [SerializeField] protected OVRInput.Controller m_controller;
        [SerializeField] protected Transform m_parentTransform;
        [SerializeField] protected GameObject m_player;

        protected bool m_grabVolumeEnabled = true;
        protected Vector3 m_lastPos;
        protected Quaternion m_lastRot;
        protected Quaternion m_anchorOffsetRotation;
        protected Vector3 m_anchorOffsetPosition;
        protected float m_prevFlex;
        protected ActionGrab m_grabbedObj = null;
        protected Vector3 m_grabbedObjectPosOff;
        protected Quaternion m_grabbedObjectRotOff;
        protected Dictionary<ActionGrab, int> m_grabCandidates = new Dictionary<ActionGrab, int>();
        protected bool m_operatingWithoutOVRCameraRig = true;

        /// <summary>
        /// The currently grabbed object.
        /// </summary>
        public ActionGrab grabbedObject
        {
            get { return m_grabbedObj; }
        }

        public void ForceRelease(ActionGrab grabbable)
        {
            bool canRelease = (
                (m_grabbedObj != null) &&
                (m_grabbedObj == grabbable)
            );
            if (canRelease)
            {
                GrabEnd();
            }
        }

        protected virtual void Awake()
        {
            m_anchorOffsetPosition = transform.localPosition;
            m_anchorOffsetRotation = transform.localRotation;

            if (!m_moveHandPosition)
            {
                // If we are being used with an OVRCameraRig, let it drive input updates, which may come from Update or FixedUpdate.
                OVRCameraRig rig = transform.GetComponentInParent<OVRCameraRig>();
                if (rig != null)
                {
                    rig.UpdatedAnchors += (r) => { OnUpdatedAnchors(); };
                    m_operatingWithoutOVRCameraRig = false;
                }
            }
        }

        protected virtual void Start()
        {
            m_lastPos = transform.position;
            m_lastRot = transform.rotation;
            if (m_parentTransform == null)
            {
                m_parentTransform = gameObject.transform;
            }
            // We're going to setup the player collision to ignore the hand collision.
            SetPlayerIgnoreCollision(gameObject, true);
        }

        virtual public void Update()
        {
            alreadyUpdated = false;
        }

        virtual public void FixedUpdate()
        {
            if (m_operatingWithoutOVRCameraRig)
            {
                OnUpdatedAnchors();
            }
        }

        // Hands follow the touch anchors by calling MovePosition each frame to reach the anchor.
        // This is done instead of parenting to achieve workable physics. If you don't require physics on
        // your hands or held objects, you may wish to switch to parenting.
        private void OnUpdatedAnchors()
        {
            // Don't want to MovePosition multiple times in a frame, as it causes high judder in conjunction
            // with the hand position prediction in the runtime.
            if (alreadyUpdated) return;
            alreadyUpdated = true;

            Vector3 destPos = m_parentTransform.TransformPoint(m_anchorOffsetPosition);
            Quaternion destRot = m_parentTransform.rotation * m_anchorOffsetRotation;

            if (m_moveHandPosition)
            {
                GetComponent<Rigidbody>().MovePosition(destPos);
                GetComponent<Rigidbody>().MoveRotation(destRot);
            }

            if (!m_parentHeldObject)
            {
                MoveGrabbedObject(destPos, destRot);
            }

            m_lastPos = transform.position;
            m_lastRot = transform.rotation;

            float prevFlex = m_prevFlex;
            // Update values from inputs
            m_prevFlex = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);

            CheckForGrabOrRelease(prevFlex);
        }

        void OnDestroy()
        {
            if (m_grabbedObj != null)
            {
                GrabEnd();
            }
        }

        public void OnTriggerEnter(Collider otherCollider)
        {
            // Get the grab trigger
            ActionGrab grabbable = otherCollider.GetComponent<ActionGrab>();
            if (grabbable.isGrabable)
            {
                // Add the grabbable
                int refCount = 0;
                m_grabCandidates.TryGetValue(grabbable, out refCount);
                m_grabCandidates[grabbable] = refCount + 1;
                return;
            }
            else
            {
                Tool tool = grabbable.transform.GetComponent<Tool>();
                if (tool != null)
                {
                    grabbable = tool.GetComponent<ActionGrab>();
                }
                else
                {
                    grabbable = null;
                }
            }
        }

        public void OnTriggerExit(Collider otherCollider)
        {
            ActionGrab grabbable = otherCollider.GetComponent<ActionGrab>();
            if (grabbable.isGrabable)
            {
                // Remove the grabbable
                int refCount = 0;
                bool found = m_grabCandidates.TryGetValue(grabbable, out refCount);
                if (!found)
                {
                    return;
                }

                if (refCount > 1)
                {
                    m_grabCandidates[grabbable] = refCount - 1;
                }
                else
                {
                    m_grabCandidates.Remove(grabbable);
                }
                return;
            }
            else
            {
                Tool tool = grabbable.transform.GetComponent<Tool>();
                if (tool != null)
                {
                    grabbable = tool.GetComponent<ActionGrab>();
                }
                else
                {
                    grabbable = null;
                }
            }
        }

        protected void CheckForGrabOrRelease(float prevFlex)
        {
            if ((m_prevFlex >= grabBegin) && (prevFlex < grabBegin))
            {
                GrabBegin();
            }
            else if ((m_prevFlex <= grabEnd) && (prevFlex > grabEnd))
            {
                GrabEnd();
            }
        }

        protected virtual void GrabBegin()
        {
            float closestMagSq = float.MaxValue;
            ActionGrab closestGrabbable = null;
            Collider closestGrabbableCollider = null;

            // Iterate grab candidates and find the closest grabbable candidate
            foreach (ActionGrab grabbable in m_grabCandidates.Keys)
            {
                bool canGrab = !(grabbable.isGrabbed && !grabbable.allowOffhandGrab) && grabbable.isGrabable;
                if (!canGrab)
                {
                    continue;
                }

                for (int j = 0; j < grabbable.grabPoints.Length; ++j)
                {
                    Collider grabbableCollider = grabbable.grabPoints[j];
                    // Store the closest grabbable
                    Vector3 closestPointOnBounds = grabbableCollider.ClosestPointOnBounds(m_gripTransform.position);
                    float grabbableMagSq = (m_gripTransform.position - closestPointOnBounds).sqrMagnitude;
                    if (grabbableMagSq < closestMagSq)
                    {
                        closestMagSq = grabbableMagSq;
                        closestGrabbable = grabbable;
                        closestGrabbableCollider = grabbableCollider;
                    }
                }
            }

            // Disable grab volumes to prevent overlaps
            GrabVolumeEnable(false);

            if (closestGrabbable != null)
            {
                if (closestGrabbable.isGrabbed)
                {
                    closestGrabbable.grabbedBy.OffhandGrabbed(closestGrabbable);
                }

                m_grabbedObj = closestGrabbable;
                m_grabbedObj.GrabBegin(this, closestGrabbableCollider);

                m_lastPos = transform.position;
                m_lastRot = transform.rotation;

                // Set up offsets for grabbed object desired position relative to hand.
                if (m_grabbedObj.snapPosition)
                {
                    m_grabbedObjectPosOff = m_gripTransform.localPosition;
                    if (m_grabbedObj.snapOffset)
                    {
                        Vector3 snapOffset = m_grabbedObj.snapOffset.position;
                        if (m_controller == OVRInput.Controller.LTouch) snapOffset.x = -snapOffset.x;
                        m_grabbedObjectPosOff += snapOffset;
                    }
                }
                else
                {
                    Vector3 relPos = m_grabbedObj.transform.position - transform.position;
                    relPos = Quaternion.Inverse(transform.rotation) * relPos;
                    m_grabbedObjectPosOff = relPos;
                }

                if (m_grabbedObj.snapOrientation)
                {
                    m_grabbedObjectRotOff = m_gripTransform.localRotation;
                    if (m_grabbedObj.snapOffset)
                    {
                        m_grabbedObjectRotOff = m_grabbedObj.snapOffset.rotation * m_grabbedObjectRotOff;
                    }
                }
                else
                {
                    Quaternion relOri = Quaternion.Inverse(transform.rotation) * m_grabbedObj.transform.rotation;
                    m_grabbedObjectRotOff = relOri;
                }

                // Note: force teleport on grab, to avoid high-speed travel to dest which hits a lot of other objects at high
                // speed and sends them flying. The grabbed object may still teleport inside of other objects, but fixing that
                // is beyond the scope of this demo.
                //MoveGrabbedObject(m_lastPos,m_lastRot,true);
                SetPlayerIgnoreCollision(m_grabbedObj.gameObject, true);
                if (m_parentHeldObject)
                {
                    m_grabbedObj.transform.parent = transform;
                }
            }
        }

        protected virtual void MoveGrabbedObject(Vector3 pos, Quaternion rot, bool forceTeleport = false)
        {
            if (m_grabbedObj == null)
            {
                return;
            }

            Rigidbody grabbedRigidbody = m_grabbedObj.grabbedRigidbody;
            Vector3 grabbablePosition = pos + rot * m_grabbedObjectPosOff;
            Quaternion grabbableRotation = rot * m_grabbedObjectRotOff;

            if (forceTeleport)
            {
                grabbedRigidbody.transform.position = grabbablePosition;
                grabbedRigidbody.transform.rotation = grabbableRotation;
            }
            else
            {
                grabbedRigidbody.MovePosition(grabbablePosition);
                grabbedRigidbody.MoveRotation(grabbableRotation);
            }
        }

        protected void GrabEnd()
        {
            if (m_grabbedObj != null)
            {
                OVRPose localPose = new OVRPose { position = OVRInput.GetLocalControllerPosition(m_controller), orientation = OVRInput.GetLocalControllerRotation(m_controller) };
                OVRPose offsetPose = new OVRPose { position = m_anchorOffsetPosition, orientation = m_anchorOffsetRotation };
                localPose = localPose * offsetPose;

                OVRPose trackingSpace = transform.ToOVRPose() * localPose.Inverse();
                Vector3 linearVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerVelocity(m_controller);
                Vector3 angularVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerAngularVelocity(m_controller);

                GrabbableRelease(linearVelocity, angularVelocity);
            }

            // Re-enable grab volumes to allow overlap events
            GrabVolumeEnable(true);
        }

        protected void GrabbableRelease(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            m_grabbedObj.GrabEnd(linearVelocity, angularVelocity);
            if (m_parentHeldObject)
            {
                Tool tool = m_grabbedObj.transform.GetComponent<Tool>();

                if (tool.transform.root != null)
                {
                    m_grabbedObj.transform.parent = tool.transform.root;
                }
                else
                {
                    m_grabbedObj.transform.parent = null;
                }

//                InstrumentPart instrumentPart = m_grabbedObj.transform.GetComponent<InstrumentPart>();
//                if (instrumentPart.parentPart != null)
//                {
//                    m_grabbedObj.transform.parent = instrumentPart.parentPart.transform;
//                }
//                else
//                {
//                    m_grabbedObj.transform.parent = null;
//                }
            }

            SetPlayerIgnoreCollision(m_grabbedObj.gameObject, false);
            m_grabbedObj = null;
        }

        protected virtual void GrabVolumeEnable(bool enabled)
        {
            if (m_grabVolumeEnabled == enabled)
            {
                return;
            }

            m_grabVolumeEnabled = enabled;
            for (int i = 0; i < m_grabVolumes.Length; ++i)
            {
                Collider grabVolume = m_grabVolumes[i];
                grabVolume.enabled = m_grabVolumeEnabled;
            }

            if (!m_grabVolumeEnabled)
            {
                m_grabCandidates.Clear();
            }
        }

        protected virtual void OffhandGrabbed(ActionGrab grabbable)
        {
            if (m_grabbedObj == grabbable)
            {
                GrabbableRelease(Vector3.zero, Vector3.zero);
            }
        }

        protected void SetPlayerIgnoreCollision(GameObject grabbable, bool ignore)
        {
            if (m_player != null)
            {
                Collider[] playerColliders = m_player.GetComponentsInChildren<Collider>();
                foreach (Collider pc in playerColliders)
                {
                    Collider[] colliders = grabbable.GetComponentsInChildren<Collider>();
                    foreach (Collider c in colliders)
                    {
                        Physics.IgnoreCollision(c, pc, ignore);
                    }
                }
            }
        }
    }
}