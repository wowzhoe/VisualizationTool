﻿using UnityEngine;

namespace VisualizationTool.Utils
{
    /// <summary>
    /// Factory design pattern with generic twist!
    /// </summary>
    public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
    {
        // Reference to prefab of whatever type.
        [SerializeField] private T prefab;

        /// <summary>
        /// Creating new instance of prefab.
        /// </summary>
        /// <returns>New initialized instance of prefab.</returns>
        public T GetInstance()
        {
            return Instantiate(prefab);
        }
    }
}
