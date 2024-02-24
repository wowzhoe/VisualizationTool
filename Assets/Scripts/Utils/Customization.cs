using UnityEngine;

namespace VisualizationTool.Utils
{
    public static class Customization
    {
        public static void SetColor(GameObject obj)
        {
            obj.GetComponent<MeshRenderer>()?.material.SetColor("_Color", Color.red);
        }

        public static void SetColor(GameObject obj, Color color)
        {
            obj.GetComponent<MeshRenderer>()?.material.SetColor("_Color", color);
        }

        public static Color GetColor(GameObject obj)
        {
            return obj.GetComponent<MeshRenderer>().material.color;
        }

        public static bool GetColor(GameObject obj, Color color)
        {
            return obj.GetComponent<MeshRenderer>().material.color == color;
        }

        public static void Hide(GameObject obj)
        {
            obj.GetComponent<MeshRenderer>().enabled = false;
        }

        public static void UnHide(GameObject obj)
        {
            obj.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}