using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;


namespace Abyss_Call
{
    public class NPCHandler : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Transform>() && e.HasComponent<Drawable>() && e.HasComponent<NPCC>();
        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            Vector2 ep = entity.GetComponent<Transform>().Position;
            Vector2 pp = Game.Player.GetComponent<Transform>().Position;

            NPCC n = entity.GetComponent<NPCC>();

            if ((ep - pp).Length() <= 100)
            {
                n.LookAt = Game.Player;
                n.CanTalk = true;
                entity.GetComponent<Drawable>().Direction = MathsManager.PointsToDirection(ep, pp);
            }
            else
            {
                entity.GetComponent<Drawable>().Direction = entity.GetComponent<NPCC>().NativeDirection;
                n.CanTalk = false;
            }
        }
    }
}
