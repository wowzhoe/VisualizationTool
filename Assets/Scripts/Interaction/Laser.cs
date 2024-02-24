using System;
using Photon.Pun;
using UnityEngine;

namespace VisualizationTool.Interaction
{
    public class Laser : MonoBehaviourPunCallbacks, IPunObservable
    {
        public LineRenderer LineRenderer;
        private bool enable;

        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        [SerializeField] private Vector3 laserOffset;
        [SerializeField] private Vector3 rayOffset;

        // Update is called once per frame
        void Update()
        {
            if (!photonView.IsMine && PhotonNetwork.IsConnected)
            {
                LineRenderer.enabled = UpdateState();
                return;
            }
        }

        private bool UpdateState()
        {
            Action result = enable != false ? new Action(() => GetHit()) : new Action(() => GetState());
            result.Invoke();
            return enable;
        }
        private bool GetState()
        {
            return enable;
        }
        private void State(RaycastHit hit)
        {
            enable = hit.transform != null;
            LineRenderer.enabled = UpdateState();
        }

        public Transform Interact()
        {
            RaycastHit hit = GetHit();
            Action result = hit.transform != null ? new Action(() => State(hit)) : new Action(() => UpdateState());
            result.Invoke();
            return hit.transform;
        }
        public void Disable()
        {
            enable = false;
            LineRenderer.enabled = UpdateState();
        }
        private RaycastHit GetHit()
        {
            RaycastHit hit;
            Physics.Raycast(startPoint.position + startPoint.right * rayOffset.x + startPoint.up * rayOffset.y + startPoint.forward * rayOffset.z, startPoint.forward, out hit);
            endPoint.position = hit.point;
            LineRenderer.SetPosition(0, startPoint.position + startPoint.right * laserOffset.x + startPoint.up * laserOffset.y + startPoint.forward * laserOffset.z);
            LineRenderer.SetPosition(1, endPoint.position);
            return hit;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(enable);
            }
            else
            {
                // Network player, receive data
                enable = (bool)stream.ReceiveNext();
            }
        }
    }
}