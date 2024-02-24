using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using VisualizationTool.Utils;

namespace VisualizationTool.Networking.Photon
{
    public class ViewConfig
    {
        private bool synchronizePosition = true;
        private bool synchronizeRotation = true;
        private bool synchronizeScale = false;

        public ViewConfig(GameObject obj)
        {
            PhotonTransformView view = obj.AddOrGetComponent<PhotonTransformView>();
            view.m_SynchronizePosition = synchronizePosition;
            view.m_SynchronizeRotation = synchronizeRotation;
            view.m_SynchronizeScale = synchronizeScale;

            PhotonView pView = obj.AddOrGetComponent<PhotonView>();
            pView.ObservedComponents = new List<Component>();
            pView.ObservedComponents.Add(view);
            pView.Synchronization = ViewSynchronization.UnreliableOnChange;
        }

        public void AddComponent<T>(GameObject obj) where T : Component
        {
            T component = obj.AddComponent<T>();
            PhotonView pView = obj.GetComponent<PhotonView>();
            pView.ObservedComponents.Add(component);
        }
    }
}