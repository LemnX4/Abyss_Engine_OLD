using Microsoft.Xna.Framework;


namespace Abyss_Call
{
    public class Mouseoverer : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Transform>() && e.HasComponent<Mouseover>();
        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            Transform t = entity.GetComponent<Transform>();
            Mouseover mo = entity.GetComponent<Mouseover>();

            Rectangle area = mo.Area;
            area.X = (int)(t.Position.X + area.X * Game.SPS);
            area.Y = (int)(t.Position.Y + area.Y * Game.SPS);
            area.Width *= Game.SPS;
            area.Height *= Game.SPS;

            bool hovered = area.Contains(Game.MouseManager.Position);

            if (!mo.Hovered && hovered)
                Game.AudioManager.PlayEffect("mouseover", -0.5f);

            mo.Hovered = hovered;
        }
    }
}
