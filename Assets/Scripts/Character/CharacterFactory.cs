using UnityEngine;
using VisualizationTool.Core;
using VisualizationTool.Utils;

namespace VisualizationTool.Character.Internal
{
    public class CharacterFactory : MonoBehaviour
    {
        public CharacterFactory()
        {

        }

        /// <summary>
        /// Return fresh created empty character from empty gameobject
        /// </summary>
        public Character Create()
        {
            GameObject characterHolder = new GameObject("Character");
            Character character = characterHolder.AddComponent<Character>();

            return character;
        }

        /// <summary>
        /// Pass Character Config for creating character class and pass information for Lobby character or not
        /// </summary>
        /// <param name="config"></param><param name="forLobby"></param>
        public Character Create(CharacterConfig config, bool forLobby)
        {
            return Create(config, forLobby, null);
        }

        /// <summary>
        /// Pass Character Config for creating character class and pass information for Lobby character or not
        /// And passing gameobject to where we want add character components from factory
        /// </summary>
        /// <param name="config"></param><param name="forLobby"></param><param name="obj"></param>
        public Character Create(CharacterConfig config, bool forLobby, GameObject obj = null)
        {
            GameObject characterHolder = null;
            characterHolder = obj ?? Loader.Instantiate<GameObject>(AddressableNames.Character);
            Character character = characterHolder.AddComponent<Character>();

            character.ControllerAnchor = characterHolder.transform.GetChild(0).gameObject;
            character.DummyAnchor = characterHolder.transform.GetChild(1).gameObject;
            character.HelpersAnchor = characterHolder.transform.GetChild(2).gameObject;

            CharacterJoint characterJoint = character.GetComponent<CharacterJoint>();

            switch (config.Role)
            {
                case CharacterRole.User:
                    break;
                case CharacterRole.Dummy:
                    break;
            }

            switch (config.Type)
            {
                case CharacterType.Standalone:

                    GameObject standalone = Loader.Instantiate<GameObject>(AddressableNames.Standalone);
                    standalone.transform.parent = character.ControllerAnchor.transform;

                    if (!forLobby)
                    {
                        GameObject standaloneUI = Loader.Instantiate<GameObject>(AddressableNames.UI.Standalone);
                        standaloneUI.transform.parent = characterJoint.HeadJoint;

                        GameObject standaloneInteraction = Loader.Instantiate<GameObject>(AddressableNames.Interaction.ToolBridge);
                        standaloneInteraction.transform.parent = standaloneUI.transform;
                    }

                    break;
                case CharacterType.Oculus:

                    GameObject ovr = Loader.Instantiate<GameObject>(AddressableNames.OVR);
                    ovr.transform.parent = character.ControllerAnchor.transform;

                    character.HelpersAnchor.SetActive(true);

                    if (!forLobby)
                    {
                        GameObject ovrUI = Loader.Instantiate<GameObject>(AddressableNames.UI.OVR);
                        ovrUI.transform.parent = characterJoint.RightHandJoint;

                        GameObject standaloneInteraction = Loader.Instantiate<GameObject>(AddressableNames.Interaction.ToolBridge);
                        standaloneInteraction.transform.parent = ovrUI.transform;
                    }

                    break;
                case CharacterType.Vive:
                    break;
            }

            return character;
        }

        /// <summary>
        /// Return fresh created empty gameobject to where we will add character component
        /// </summary>
        /// <param name="character"></param>
        public GameObject InstantiateCharacter(GameObject character)
        {
            GameObject characterObj = Instantiate<GameObject>(character) as GameObject;
            characterObj.AddComponent<Character>();

            return characterObj;
        }
    }
}
