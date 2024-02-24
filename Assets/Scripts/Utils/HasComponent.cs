using UnityEngine;

namespace VisualizationTool.Utils
{
    public static class Utils
    {
        public static bool HasComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>() != null;
        }

        public static bool HasOrAddComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>() ? obj.GetComponent<T>() : obj.AddComponent<T>();
        }

        public static bool HasOrAddComponent<T>(this Transform transform) where T : Component
        {
            return transform.GetComponent<T>() ? transform.GetComponent<T>() : transform.gameObject.AddComponent<T>();
        }

        public static T AddOrGetComponent<T>(this GameObject obj) where T : Component
        {
            return obj.GetComponent<T>() ? obj.GetComponent<T>() : obj.AddComponent<T>();
        }

        public static T AddOrGetComponent<T>(this Transform transform) where T : Component
        {
            return transform.GetComponent<T>() ? transform.GetComponent<T>() : transform.gameObject.AddComponent<T>();
        }
    }
}