using Microsoft.Xna.Framework;


namespace Abyss_Call
{
    public class CameraManager
    {
        public Matrix Transform { get; set; }
        public Transform PlayerTransform { get; set; }

        public void Update(double gameTime)
        {
            var offset = Matrix.CreateTranslation(Game.Width / 2f, Game.Height / 2f, 0);
            Transform = Matrix.CreateTranslation(-(int)PlayerTransform.Position.X, -(int)PlayerTransform.Position.Y, 0) * offset;
        }
    }
}
