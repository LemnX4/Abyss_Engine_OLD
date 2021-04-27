using System.Collections.Generic;


namespace Abyss_Call
{
    public abstract class System
    {
        public abstract bool Requirements(Entity e);
        public List<Entity> EntityBucket { get; set; } = new List<Entity>();

        public virtual void Update(double deltaTime)
        {
            for (int i = EntityBucket.Count - 1; i >= 0; i--)
                if (this.GetType().Name == nameof(SceneFader) || EntityBucket[i].IsUpdatable)
                    UpdateEntity(EntityBucket[i], deltaTime);
        }
        protected virtual void UpdateEntity(Entity entity, double deltaTime)
        {

        }
    }
}
