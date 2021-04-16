using System.Collections.Generic;
using System.Diagnostics;

namespace Abyss_Call
{
    public class System
    {
        public string Tag { get; protected set; } = null;
        public List<string> Requirements { get; protected set; } = null;
        public List<Entity> EntityBucket { get; set; } = new List<Entity>();

        public void Update(int deltaTime)
        {
            foreach (Entity e in EntityBucket)
                if (e.IsUpdatable)
                    UpdateEntity(e, deltaTime);
        }
        protected virtual void UpdateEntity(Entity entity, int deltaTime)
        {
            //Debug.WriteLine(entity.Tag, "Updating: ");
        }
    }
}
