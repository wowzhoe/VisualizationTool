namespace VisualizationTool.Character
{
    public class CharacterConfig
    {
        public CharacterRole Role;
        public CharacterType Type;

        /// <summary>
        /// Character Config constructor for assign role and type of character dummy
        /// </summary>
        /// <param name="role"></param><param name="type"></param>
        public CharacterConfig(CharacterRole role, CharacterType type)
        {
            this.Role = role;
            this.Type = type;
        }
    }
}
