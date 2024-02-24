using UnityEngine;
using VisualizationTool.Utils;

namespace VisualizationTool.Tools.Node
{
    public class Node<T> where T : Component
    {
        /// <summary>
        /// Pass gameobject for constructor instantiation
        /// </summary>
        /// <param name="obj"></param>
        public Node(GameObject obj)
        {
            T component = obj.AddOrGetComponent<T>();
            MeshCollider collider = obj.AddOrGetComponent<MeshCollider>();
            collider.convex = true;
            collider.isTrigger = true;
        }
    }
}