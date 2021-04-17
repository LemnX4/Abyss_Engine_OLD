using Microsoft.Xna.Framework;


namespace Abyss_Call
{
    public class MouseoverDrawer : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Mouseover>() && e.HasComponent<Drawable>();
        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            Drawable d = entity.GetComponent<Drawable>();

            d.TexturePosition = new Point(d.TexturePosition.X, (entity.GetComponent<Mouseover>().Hovered ? 1 : 0) * d.TextureSize.Y);
        }
    }
}
