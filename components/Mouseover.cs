using Microsoft.Xna.Framework;
using System.Diagnostics;


namespace Abyss_Call
{
    public class Mouseover : Component
    {
        private bool _hovered;
        public bool Hovered
        {
            get
            {
                return _hovered; 
            }
            set
            {
                if (!_hovered && value)
                    Game.AudioManager.PlayEffect("mouseover");
                _hovered = value;
            }
        }
        public Rectangle Area { get; set; } = new Rectangle(0, 0, 0, 0);
        public Mouseover()
        {
            _hovered = false;
        }
    }
}
