using UnityEngine;
using VisualizationTool.Platform;
using VisualizationTool.Utils;

namespace VisualizationTool.UI
{
    /// <summary>
    /// Event System with Singletone for UI platform type correction
    /// </summary>
    public class EventSystem : PersistentMonoBehaviour<EventSystem>
    {
        public GameObject StandaloneEventSystem;
        public GameObject OVREventSystem;

        // Start is called before the first frame update
        void Start()
        {
            switch (Platform.Platform.Instance.PlatformType)
            {
                    case PlatformType.Standalone:
                    StandaloneEventSystem.SetActive(true);
                    break;
                    case PlatformType.Oculus:
                    OVREventSystem.SetActive(true);
                    break;
            }
        }
    }
}
