using UnityEngine;
using VisualizationTool.Character;
using VisualizationTool.Character.Internal;
using VisualizationTool.Controls;
using VisualizationTool.Platform;
using VisualizationTool.Utils;

public class CharacterCreationTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Init()
    {
        // Fill configs
        CharacterConfig cc = new CharacterConfig(CharacterRole.User, (CharacterType)Platform.Instance.PlatformType);
        CharacterFactory cf = new CharacterFactory();

        // Instantiate character
        Character character = cf.Create(cc, true);
    }
}
