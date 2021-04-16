using System.Collections.Generic;
using System.Diagnostics;

namespace Abyss_Call
{
    public abstract class System
    {
        public string Tag { get; protected set; } = null;
        public abstract bool Requirements(Entity e);
        public List<Entity> EntityBucket { get; set; } = new List<Entity>();

        public void Update(double deltaTime)
        {
            foreach (Entity e in EntityBucket)
                if (e.IsUpdatable)
                    UpdateEntity(e, deltaTime);
        }
        protected virtual void UpdateEntity(Entity entity, double deltaTime)
        {
            //Debug.WriteLine(entity.Tag, "Updating: ");
        }
    }
}
