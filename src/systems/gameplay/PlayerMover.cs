using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Abyss_Call
{
    public class PlayerMover : System
    {
        public override bool Requirements(Entity e) => 
            e.HasComponent<Transform>() && e.HasComponent<Drawable>() && 
            e.HasComponent<Playable>() && e.HasComponent<Movable>() &&
            e.HasComponent<Animation>();
        
        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            Movable m = entity.GetComponent<Movable>();

            if (!m.CanMove)
                return;

            Vector2 p = entity.GetComponent<Transform>().Position;
            Drawable d = entity.GetComponent<Drawable>();
            Animation a = entity.GetComponent<Animation>();

            double dx = 0, dy = 0;

            double normalizer = deltaTime / 5f;

            
            if (Game.KeyboardManager.IsKeyDown(Keys.S))
            {
                dy += m.Speed * normalizer;
                d.Direction = 0;
            }
            if (Game.KeyboardManager.IsKeyDown(Keys.Z))
            {
                dy -= m.Speed * normalizer;
                d.Direction = 1;
            }
            if (Game.KeyboardManager.IsKeyDown(Keys.D))
            {
                dx += m.Speed * normalizer;
                d.Direction = 3;
            }
            if (Game.KeyboardManager.IsKeyDown(Keys.Q))
            {
                dx -= m.Speed * normalizer;
                d.Direction = 2;
            }

            m.Moving = (dx != 0 || dy != 0);
            a.Animated = m.Moving;

            if (dx != 0 && dy != 0)
            {
                dx /= 1.414;
                dy /= 1.414;
            }

            entity.GetComponent<Transform>().Position = new Vector2((float)(p.X + dx), (float)(p.Y + dy));
        }
    }
}
