using Microsoft.Xna.Framework;

namespace Abyss_Call
{
    public class Transform : Component
    {
        private Vector2 _position;
        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (HostEntity != null)
                    foreach (Entity e in HostEntity.Entities)
                        if (e.HasComponent<Transform>())
                            e.GetComponent<Transform>().Position += value - _position;
                
                _position = value;
            }
        }
        public Vector2 Origin { get; set; } = new Vector2(0f, 0f);

        public float _xScale;
        public float XScale
        {
            get
            {
                return _xScale;
            }
            set
            {
                if (HostEntity != null)
                    foreach (Entity e in HostEntity.Entities)
                        if (e.HasComponent<Transform>())
                            e.GetComponent<Transform>().XScale *= value / _xScale;

                _xScale = value;
            }
        }
        public float _yScale;
        public float YScale
        {
            get
            {
                return _yScale;
            }
            set
            {
                if (HostEntity != null)
                    foreach (Entity e in HostEntity.Entities)
                        if (e.HasComponent<Transform>())
                            e.GetComponent<Transform>().YScale *= value / _yScale;

                _yScale = value;
            }
        }
        public float Rotation { get; set; } = 0f;
        public Transform()
        {
            _position = new Vector2(0f, 0f);
            _xScale = 1f;
            _yScale = 1f;
        }
    }
}
