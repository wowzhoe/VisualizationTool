using UnityEngine;
using VisualizationTool.Utils;

namespace VisualizationTool.Tools.Actions
{
    public class Base
    {
        public void Init(GameObject obj)
        {
            //obj.AddOrGetComponent<ActionIncrease>();
            obj.AddOrGetComponent<ActionDisableSelect>();
            obj.AddOrGetComponent<ActionHideSelect>();
            obj.AddOrGetComponent<ActionIncreaseSelect>();
            obj.AddOrGetComponent<ActionShowAll>();
            obj.AddOrGetComponent<ActionShowSelect>();
        }

        public void Add<T>(GameObject obj) where T : Action
        {
            obj.AddOrGetComponent<T>();
        }
    }
}