using System.Collections.Generic;


namespace Abyss_Call
{
    public class Entity
    {
        private bool _isUpdatable;
        public bool IsUpdatable
        {
            get
            {
                return _isUpdatable;
            }
            set
            {
                foreach (Entity e in Entities)
                    e.IsUpdatable = value;

                _isUpdatable = value;
            }
        }
        private bool _isRenderable;
        public bool IsRenderable
        {
            get
            {
                return _isRenderable;
            }
            set
            {
                foreach (Entity e in Entities)
                    e.IsRenderable = value;

                _isRenderable = value;
            }
        }
        public List<Component> Components { get; protected set; } = new List<Component>();
        public List<Entity> Entities { get; protected set; } = new List<Entity>();

        public Entity()
        {
            _isUpdatable = true;
            _isRenderable = true;
        }
        public void AddComponent(Component component)
        {
            component.HostEntity = this;
            Components.Add(component);

            foreach (System s in Game.Systems)
            {
                if (s.Requirements(this) && !s.EntityBucket.Contains(this))
                    s.EntityBucket.Add(this);
            }
        }

        public void RemoveComponent<T>() where T : Component
        {
            var toRemove = Components.Find(component => component is T);

            if (!(toRemove is null))
                Components.Remove(toRemove);

            foreach (System s in Game.Systems)
            {
                if (!s.Requirements(this) && s.EntityBucket.Contains(this))
                    s.EntityBucket.Remove(this);
            }
        }

        public bool HasComponent<T>() where T : Component
        {
            return Components.Find(component => component is T) != null;
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)Components.Find(component => component is T);
        }
    }
}
