using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace Abyss_Call
{
    public class Mouseoverer : System
    {
        public Mouseoverer()
        {
            Tag = "mouseoverer";
            Requirements = new List<string> { "mouseover", "transform" };
        }
        protected override void UpdateEntity(Entity entity, int deltaTime)
        {
            base.UpdateEntity(entity, deltaTime);

            Rectangle area = entity.GetComponent<Mouseover>().Area;
            area.X = (int)(entity.GetComponent<Transform>().Position.X + area.X * Game.SPS);
            area.Y = (int)(entity.GetComponent<Transform>().Position.Y + area.Y * Game.SPS);
            area.Width *= Game.SPS;
            area.Height *= Game.SPS;

            entity.GetComponent<Mouseover>().Hovered = area.Contains(Game.MouseManager.Position);
        }
    }
}
