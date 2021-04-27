namespace Abyss_Call
{
    public class Animator : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Animation>();

        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            entity.GetComponent<Animation>().ProperFrameTime += deltaTime;
        }
    }
}
