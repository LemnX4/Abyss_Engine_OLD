using Microsoft.Xna.Framework;


namespace Abyss_Call
{
    public class Mouseover : Component
    {
        public bool Hovered { get; set; } = false;
        public Rectangle Area { get; set; } = new Rectangle(0, 0, 0, 0);
    }
}
