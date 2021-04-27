using System;


namespace Abyss_Call
{
    public class HighlightHandler : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Drawable>() && e.HasComponent<Highlight>();
        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            Highlight h = entity.GetComponent<Highlight>();
            Drawable d = entity.GetComponent<Drawable>();

            var n = h.EntityToHighlight.GetComponent<NPCC>();
            if (!(n is null))
                entity.IsRenderable = n.CanTalk;

            if (!entity.IsRenderable || h.ProperTime >= h.Period)
            {
                h.ProperTime = 0;
                return;
            }

            d.Direction = h.EntityToHighlight.GetComponent<Drawable>().Direction;

            h.ProperTime += deltaTime;

            d.Alpha = (1 + (float)Math.Cos(h.ProperTime / h.Period * 2 * Math.PI)) / 2f;
        }
    }
}
