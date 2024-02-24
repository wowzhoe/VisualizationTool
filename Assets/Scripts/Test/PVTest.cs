using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VisualizationTool.Character;
using VisualizationTool.Controls;
using VisualizationTool.Platform;

public class GGWPP : MonoBehaviour
{
    public void GG()
    {
        Debug.Log(Platform.Instance.PlatformType);
    }
}

public class PVTest : MonoBehaviour
{
    public Camera camera;
    public WASD wasd;

    void Start()
    {

        wasd = new WASD();
    }

    void Update()
    {
        CharacterConfig cc = new CharacterConfig(CharacterRole.User, CharacterType.Standalone);
        //KeyboardController.Move(this.transform);
        WASD wasd = new WASD();
        wasd.Move(this.transform);
    }
}

//public void O
//    private void o
//    cont CONT_HELLO
//    namespace 
//
//
//
//namespace Player
//{
//    using UI;
//
//    public class PlayerUI
//    {
//        public void OnClickButton()
//        {
//            UIInterface.OpenMenu();
//        }
//    }
//}
//
//
//namespace UI
//{
//    using UI.Internal;
//
//    public class UIHelper / UIGateway
//    {
//        public void OpenMenu()
//        {
//            // magic here
//            //Magic.OpenMenu();
//        }
//    }
//}
//
//namespace UI.Internal
//{
//    public class Magic
//    {
//        public void OpenMenu()
//        {
//           
//        }
//    }
//}
