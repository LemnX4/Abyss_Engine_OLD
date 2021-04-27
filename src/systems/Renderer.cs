using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Abyss_Call
{
    public class Renderer : System
    {
        public GraphicsDeviceManager DeviceManager;
        public SpriteBatch SpriteBatch;
        public override bool Requirements(Entity e) => e.HasComponent<Transform>() && e.HasComponent<Drawable>();

        public Renderer(Game game)
        {
            DeviceManager = new GraphicsDeviceManager(game);
        }

        public void CreateSpriteBatch()
        {
            SpriteBatch = new SpriteBatch(DeviceManager.GraphicsDevice);
        }

        public void Draw()
        {
            SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, Game.CameraManager.Transform);

            foreach (Entity e in EntityBucket)
                if (e.IsRenderable && !e.CameraLocked)
                    DrawEntity(e);

            SpriteBatch.End();

            SpriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

            foreach (Entity e in EntityBucket)
                if (e.IsRenderable && e.CameraLocked)
                    DrawEntity(e);

            SpriteBatch.End();
        }

        public void DrawEntity(Entity entity)
        {
            Transform t = entity.GetComponent<Transform>();
            Drawable d = entity.GetComponent<Drawable>();

            var texture = Game.TextureManager.GetTexture(d.TextureTag, d.Hightlightened);
            Point TexturePosition = new Point(d.TexturePosition.X, d.TexturePosition.Y);

            if (texture == null)
                return;

            TexturePosition.Y += d.Direction * d.TextureSize.Y;

            var mo = entity.GetComponent<Mouseover>();
            if (!(mo is null))
                TexturePosition.Y += (mo.Hovered ? 1 : 0) * d.TextureSize.Y;

            var a = entity.GetComponent<Animation>();
            if (!(a is null))
                TexturePosition.X += a.Keyframe * d.TextureSize.X;

            SpriteBatch.Draw(texture,
                new Rectangle((int)t.Position.X - d.Offset.X * Game.SPS, (int)t.Position.Y - d.Offset.Y * Game.SPS, 
                (int)(d.TextureSize.X * (d.Raw ? 1 : Game.SPS) * t.XScale), (int)(d.TextureSize.Y * (d.Raw ? 1 : Game.SPS) * t.YScale)),
                new Rectangle(TexturePosition, d.TextureSize), d.Color * d.Alpha, t.Rotation, new Vector2(0, 0),
                SpriteEffects.None, ((float)d.Layer + d.LayerDepth)/10.0f);
        }
    }
}
