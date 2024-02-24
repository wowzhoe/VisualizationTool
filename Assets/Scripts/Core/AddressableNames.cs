namespace VisualizationTool.Core
{
    /// <summary>
    /// Constants container with all active gameobject names
    /// </summary>
    public static class AddressableNames
    {
        public const string Character = "Character/Character";
        public const string Standalone = "Standalone/Standalone";
        public const string OVR = "OVR/OVR";

        public static class UI
        {
            public const string Standalone = "UI/Interaction.UI.Menu.Standalone";
            public const string OVR = "UI/Interaction.UI.Menu.OVR";
        }

        public static class Interaction
        {
            public const string ToolBridge = "Interaction/Interaction.ToolBridge";
        }
    }
}