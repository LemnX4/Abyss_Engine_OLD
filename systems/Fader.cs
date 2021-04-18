namespace Abyss_Call
{
    class Fader : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Fading>() && e.HasComponent<Drawable>();
        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            Fading f = entity.GetComponent<Fading>();
            Drawable d = entity.GetComponent<Drawable>();

            f.ProperTime += deltaTime;

            if (f.LifePercent == 1)
            {
                if (f.Style == FadingStyle.FadeOut)
                    entity.IsRenderable = false;

                entity.RemoveComponent<Fading>();
            }

            if (f.Style == FadingStyle.FadeIn)
                d.Alpha = (float)f.LifePercent;
            if (f.Style == FadingStyle.FadeOut)
                d.Alpha = (float)(1 - f.LifePercent);
        }
    }
}
