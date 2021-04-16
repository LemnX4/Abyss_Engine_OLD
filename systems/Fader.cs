using System.Collections.Generic;


namespace Abyss_Call
{
    class Fader : System
    {
        public Fader()
        {
            Tag = "fader";
            Requirements = new List<string> { "fading" };
        }
        protected override void UpdateEntity(Entity entity, int deltaTime)
        {
            Fading f = entity.GetComponent<Fading>();
            f.ProperTime += deltaTime;

            if (f.Style == Fading.FadingStyle.FadeIn)
                entity.GetComponent<Drawable>().Alpha = f.LifePercent;
            if (f.Style == Fading.FadingStyle.FadeOut)
                entity.GetComponent<Drawable>().Alpha = 1 - f.LifePercent;
        }
    }
}
