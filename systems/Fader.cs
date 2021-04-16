namespace Abyss_Call
{
    class Fader : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Fading>();
        public Fader()
        {
            Tag = "fader";
        }
        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            Fading f = entity.GetComponent<Fading>();
            f.ProperTime += deltaTime;

            if (f.Style == Fading.FadingStyle.FadeIn)
                entity.GetComponent<Drawable>().Alpha = (float)f.LifePercent;
            if (f.Style == Fading.FadingStyle.FadeOut)
                entity.GetComponent<Drawable>().Alpha = (float)(1 - f.LifePercent);
        }
    }
}
