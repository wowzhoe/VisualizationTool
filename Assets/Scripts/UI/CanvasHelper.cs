using VisualizationTool.Platform;
using VisualizationTool.Utils;
using VisualizationTool.UI.Internal;

namespace VisualizationTool.UI
{
    public class CanvasHelper : PersistentMonoBehaviour<CanvasHelper>
    {
        /// <summary>
        /// Upper Level for Passing Platform specific type for Inialization Corrected Canvases
        /// </summary>
        /// <param name="type"></param>
        public void PlatformDependency(PlatformType type)
        {
            Canvas.PlatformDependency(type);
        }
    }
}