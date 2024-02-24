using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VisualizationTool.Platform;
using VisualizationTool.Utils;

namespace VisualizationTool.UI.Internal
{
    public static class Canvas
    {
        /// <summary>
        /// Pass Platform specific type for Inialization Corrected Canvases
        /// </summary>
        /// <param name="type"></param>
        public static void PlatformDependency(PlatformType type)
        {
            List<UnityEngine.Canvas> canvases = Finder.FindOfTypeList<UnityEngine.Canvas>();

            try
            {
                foreach (var canvas in canvases)
                {
                    List<Component> components = new List<Component>();

                    GraphicRaycaster gr = Add<GraphicRaycaster>(canvas.gameObject);
                    OVRRaycaster ovrr = Add<OVRRaycaster>(canvas.gameObject);

                    gr.enabled = false;
                    ovrr.enabled = false;

                    components.Add(gr);
                    components.Add(ovrr);

                    switch (type)
                    {
                        case PlatformType.Standalone:
                            gr.enabled = true;
                            break;
                        case PlatformType.Oculus:
                            ovrr.enabled = true;
                            break;
                        case PlatformType.Vive:
                            break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Pass gameobject for removing generic component from it
        /// </summary>
        /// <param name="obj"></param>
        public static void Remove<T>(GameObject obj) where T : Component
        {
            if (obj.HasComponent<T>())
            {
                UnityEngine.Object.Destroy(obj.GetComponent<T>());
            }
        }

        /// <summary>
        /// Pass gameobject for adding generic component to it
        /// </summary>
        /// <param name="obj"></param>
        private static T Add<T>(GameObject obj) where T : Component
        {
            if (!obj.GetComponent<T>())
            {
                obj.AddComponent<T>();
            }

            return obj.GetComponent<T>() as T;
        }
    }
}