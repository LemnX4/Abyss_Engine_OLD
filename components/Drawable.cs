using Microsoft.Xna.Framework;


namespace Abyss_Call
{
    public class Drawable : Component
    {
        public string TextureTag { get; set; } = null;
        public Point TexturePosition { get; set; } = new Point(0, 0);
        public Point TextureSize { get; set; } = new Point(0, 0);
        public Point Offset { get; set; } = new Point(0, 0);
        public Color Color { get; set; } = Color.White;

        private float _alpha;
        public float Alpha
        {
            get
            {
                return _alpha;
            }
            set
            {
                if (HostEntity != null)
                    foreach (Entity e in HostEntity.Entities)
                        e.GetComponent<Drawable>().Alpha = (float)value;

                _alpha = (float)value;
            }
        }
        public float LayerDepth { get; set; } = 0f;
        public int Direction { get; set; } = 0;

        public Drawable()
        {
            _alpha = 1f;
        }
    }
}
