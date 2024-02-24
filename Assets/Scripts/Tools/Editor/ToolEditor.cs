using UnityEditor;
using UnityEngine;

namespace VisualizationTool.Tools
{
    // Custom Editor using SerializedProperties.
    // Automatic handling of multi-object editing, undo, and Prefab overrides.
    [CustomEditor(typeof(Tool))]
    [CanEditMultipleObjects]
    public class ToolEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Tool tool = (Tool)target;
            if (tool.transform.root.Equals(tool.transform) && GUILayout.Button("Initialize"))
            {
                tool.EvaluateChildrenAndSelf();
            }
            DrawDefaultInspector();
        }
    }
}
