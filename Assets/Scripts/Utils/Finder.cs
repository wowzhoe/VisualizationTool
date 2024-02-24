using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VisualizationTool.Utils
{
    public static class Finder
    {
        public static List<T> FindOfType<T>() where T : Component
        {
            List<T> components = new List<T>();
            GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var rootGameObject in rootGameObjects)
            {
                T component = rootGameObject.GetComponent<T>();

                if (rootGameObject.GetComponent<T>().Equals(component) && component != null)
                {
                    components.Add(component);
                }
            }
            return components;
        }

        public static List<T> FindOfTypeList<T>() where T : Component
        {
            return GameObject.FindObjectsOfType<T>().ToList();
        }
    }
}
