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
                        e.GetComponent<Transform>().Position += value - _position;
                
                _position = value;
            }
        }
        public Vector2 Origin { get; set; } = new Vector2(0f, 0f);

        public float _scale;
        public float Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                if (HostEntity != null)
                    foreach (Entity e in HostEntity.Entities)
                        e.GetComponent<Transform>().Scale *= value / _scale;
                     
                _scale = value;
            }
        }
        public float Rotation { get; set; } = 0f;
        public Transform()
        {
            Tag = "transform";
            
            _position = new Vector2(0f, 0f);
            _scale = 1f;
        }
    }
}
