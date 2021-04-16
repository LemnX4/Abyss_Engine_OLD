using System.Collections.Generic;

namespace Abyss_Call
{
    public class Entity
    {
        public string Tag { get; set; } = null;

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
        public List<Component> Components { get; protected set; } = new List<Component>();
        public List<Entity> Entities { get; protected set; } = new List<Entity>();

        public Entity(string tag)
        {
            Tag = tag;
            _isUpdatable = true;

            AddComponent(new Transform());
            AddComponent(new Drawable());
        }
        public void AddComponent(Component component)
        {
            component.HostEntity = this;
            Components.Add(component);

            foreach (System s in Game.Systems)
            {
                if ((s.Requirements == null || this.HasComponents(s.Requirements)) && !s.EntityBucket.Contains(this))
                    s.EntityBucket.Add(this);
            }
        }

        public void RemoveComponent<T>() where T : Component
        {
            var toRemove = Components.Find(component => component is T);

            if (toRemove != null)
                Components.Remove(toRemove);

            foreach (System s in Game.Systems)
            {
                if (s.Requirements != null && !this.HasComponents(s.Requirements) && s.EntityBucket.Contains(this))
                    s.EntityBucket.Remove(this);
            }
        }

        public bool HasComponent<T>() where T : Component
        {
            return Components.Find(component => component is T) != null;
        }

        public bool HasComponent(string tag)
        {
            return Components.Find(component => component.Tag == tag) != null;
        }

        public bool HasComponents(IEnumerable<string> tags)
        {
            int total = 0;
            int nbr = 0;
            foreach (string tag in tags) {
                total++;
                if (Components.Find(component => component.Tag == tag) != null)
                    nbr++;
            }
            return nbr == total;
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)Components.Find(component => component is T);
        }
    }
}
