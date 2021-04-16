namespace Abyss_Call
{
    class Fader : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Fading>();
        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            entity.GetComponent<Fading>().ProperTime += deltaTime;

            if (entity.GetComponent<Fading>().LifePercent == 1)
            {
                if (entity.GetComponent<Fading>().Style == FadingStyle.FadeOut)
                    entity.GetComponent<Drawable>().IsRenderable = false;

                entity.RemoveComponent<Fading>();
            }
        }
    }
}
