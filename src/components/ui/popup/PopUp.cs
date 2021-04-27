using Microsoft.Xna.Framework.Input;


namespace Abyss_Call
{
    class PopUp : Component
    {
        public string Name { get; set; }
        public Keys Key { get; set; }
        public Entity CorrespondingScene { get; set; }
    }
}
