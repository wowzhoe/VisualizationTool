using System;
using System.Collections.Generic;
using UnityEngine;
using VisualizationTool.Utils;

namespace VisualizationTool.Tools
{
    public class ToolBase : MonoBehaviour, IToolEvaluatable
    {
        public List<Tool> children = new List<Tool>();
        public List<Collider> colliders = new List<Collider>();
        public Guid id;
        public ToolBase parentPart;

        internal void Awake()
        {
            id = Guid.NewGuid();
        }

        public virtual void EvaluateChildrenAndSelf()
        {
            children = GetChildren(this);
            colliders = GetChildrenColliders(this);
        }

        public virtual void EvaluateChildren()
        {

        }

        /// <summary>
        /// Evaluate children components of parent ( or linked ) tool to fit children list
        /// </summary>
        /// <param name="tool"></param>
        public virtual void EvaluateChildren(ToolBase tool)
        {
            children = GetChildren(tool);
            colliders = GetChildrenColliders(tool);
        }

        /// <summary>
        /// Callback from tool parent to get list of children with the same component on it
        /// </summary>
        /// <param name="tool"></param>
        private List<Tool> GetChildren(ToolBase tool = null)
        {
            List<Tool> children = new List<Tool>();
            int numberOfChildren = transform.childCount;

            for (int i = 0; i < numberOfChildren; i++)
            {
                Transform child = transform.GetChild(i);

                if (tool != null)
                {
                    id = Guid.NewGuid();
                    child.AddOrGetComponent<Tool>().EvaluateChildren(tool);
                }

                children.Add(child.GetComponent<Tool>());
            }

            return children;
        }

        private List<Collider> GetChildrenColliders(ToolBase tool = null)
        {
            colliders.Add(this.GetComponent<MeshCollider>());

            for (int i = 0; i < children.Count; i++)
            {
                colliders.Add(children[i].GetComponent<MeshCollider>());
            }

            return colliders;
        }
    }
}