using System;
using UnityEngine;
using VisualizationTool.Interaction;
using VisualizationTool.Platform;
using VisualizationTool.Tools.Actions.Oculus;

namespace VisualizationTool.Tools
{
    public class ToolInteractionBridge : MonoBehaviour
    {
        public ToolAssign ToolAssigned = new ToolAssign();
        public ToolMenuAssign ToolMenuEnabled = new ToolMenuAssign();
        public Laser Laser;
        private bool inMenu = false;
        private bool isMenuEnabled = false;
        private int frameCountToClose = 10;

        // Update is called once per frame
        void Update()
        {
            PlatformBridge(Platform.Platform.Instance.PlatformType, (o, p) =>
            {
                Action result = o != true ? new Action(() => Laser.Disable()) : new Action(() => Interact(o));
                result.Invoke();
                Action res = new Action(() => Activate(p));
                res.Invoke();
            });

            Adjustment();
        }

        private void Adjustment()
        {
            if (isMenuEnabled)
            {
                if (frameCountToClose <= 0)
                {
                    ToolMenuEnabled.Invoke(false);
                    isMenuEnabled = false;
                }
                else
                {
                    frameCountToClose--;
                }
            }
        }

        /// <summary>
        /// Callback from platform specific buttons / triggers to activate Menu (OnClick)
        /// </summary>
        /// <param name="enable"></param>
        private void Activate(bool enable)
        {
            inMenu = enable;

            if (enable)
            {
                isMenuEnabled = true;
                Laser.Disable();
                ToolMenuEnabled.Invoke(true);
                frameCountToClose = 10;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (Platform.Platform.Instance.PlatformType == PlatformType.Standalone)
            {
                ToolMenuEnabled.Invoke(enable);
            }
        }

        /// <summary>
        /// Pass the flag to interact with hit
        /// </summary>
        /// <param name="enable"></param>
        private void Interact(bool enable)
        {
            if (!inMenu)
            {
                ToolMenuEnabled.Invoke(!enable);
                Transform hit = Laser.Interact();
                Select(hit);
            }
            else
            {
                inMenu = false;
            }
        }

        /// <summary>
        /// Selection gameobject.hit as target for interaction
        /// </summary>
        /// <param name="hit"></param>
        private void Select(Transform hit)
        {
            if (hit != null && hit.transform.GetComponent<Tool>() != null)
            {
                Tool tool = hit.transform.GetComponent<Tool>();
                ToolAssigned.Invoke(tool);

                tool.GetComponent<ActionGrab>().isGrabable = true;
            }
        }

        /// <summary>
        /// Callback from platform specific buttons / triggers pressed
        /// </summary>
        /// <param name="type"></param><param name="callback"></param>
        private void PlatformBridge(PlatformType type, Action<bool, bool> callback)
        {
            switch (type)
            {
                case PlatformType.Oculus:
                    callback(OVRInput.Get(OVRInput.Button.One), Thumbstick());
                    break;
                case PlatformType.Standalone:
                    callback(Input.GetMouseButton(0), Input.GetMouseButton(1));
                    break;
                case PlatformType.Vive:
                    break;
            }
        }

        private bool Thumbstick()
        {
            Vector2 result = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            return (Math.Abs(result[1]) > 0.3 || Math.Abs(result[0]) > 0.3);
        }
    }
}