using UnityEngine.SceneManagement;
using VisualizationTool.Utils;

namespace VisualizationTool.UI
{
    public class UIHelper : PersistentMonoBehaviour<UIHelper>
    {
        void Awake()
        {
            SceneManager.activeSceneChanged += OnSceneWasSwitched;
            SceneManager.sceneLoaded += OnSceneWasChanged;
        }

        void Destroy()
        {
            SceneManager.activeSceneChanged -= OnSceneWasSwitched;
            SceneManager.sceneLoaded -= OnSceneWasChanged;
        }

        /// <summary>
        /// Callback when scene was switched by unity edior and passing road from A to B scene
        /// </summary>
        /// <param name="scene"></param><param name="secondScene"></param>
        private void OnSceneWasSwitched(Scene scene, Scene secondScene)
        {
            CanvasHelper.Instance.PlatformDependency(Platform.Platform.Instance.PlatformType);
        }

        /// <summary>
        /// Callback when scene was switched by unity edior and passing scene mode
        /// </summary>
        /// <param name="scene"></param><param name="mode"></param>
        private void OnSceneWasChanged(Scene scene, LoadSceneMode mode)
        {

        }
    }
}