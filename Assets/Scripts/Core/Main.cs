using UnityEngine;
using VisualizationTool.Character;
using VisualizationTool.Character.Internal;

namespace VisualizationTool.Core
{
    public class Main : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            CharacterConfig cc = new CharacterConfig(CharacterRole.User, (CharacterType)Platform.Platform.Instance.PlatformType);
            CharacterFactory cf = new CharacterFactory();
            Character.Character character = cf.Create(cc, true);
        }
    }
}