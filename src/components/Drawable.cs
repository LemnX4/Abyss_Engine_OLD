using Microsoft.Xna.Framework;


namespace Abyss_Call
{
    public enum Layer
    {
        Background = 0, 
        Playground = 1, 
        Foreground = 2,
        Userground = 3,
    }
    public class Drawable : Component
    {
        public bool Raw { get; set; } = false;
        public bool Hightlightened { get; set; } = false;
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
                        if (e.HasComponent<Drawable>())
                            e.GetComponent<Drawable>().Alpha = (float)value;

                _alpha = (float)value;
            }
        }
        private float _layerDepth;
        public float LayerDepth
        {
            get
            {
                return _layerDepth;
            }
            set
            {
                if (HostEntity != null)
                    foreach (Entity e in HostEntity.Entities)
                        if (e.HasComponent<Drawable>())
                            e.GetComponent<Drawable>().LayerDepth += value - _layerDepth;
                _layerDepth = value;
            }
        }
        private Layer _layer = Layer.Userground;
        public Layer Layer
        {
            get
            {
                return _layer;
            }
            set
            {
                if (HostEntity != null)
                    foreach (Entity e in HostEntity.Entities)
                        if (e.HasComponent<Drawable>())
                            e.GetComponent<Drawable>().Layer = value;
                _layer = value;
            }
        }
        public int Direction { get; set; } = 0;

        public Drawable()
        {
            _alpha = 1f;
        }
    }
}
