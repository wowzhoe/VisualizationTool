namespace VisualizationTool.Tools.Actions
{
    public interface IActionable
    {
        void GetAction(Action action);
    }
}

///            foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
    ///            {
    ///                if (typeof(Action).IsAssignableFrom(type) && type.IsSubclassOf(typeof(Action)))
    ///                {
    ///                    //Debug.Log(type.FullName);
    ///                    //AlterData(new ActionDisableSelect());
    ///                }
    ///            }
    /// 