using System;

namespace Abyss_Call
{
    class TextAnimator : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Transform>() && e.HasComponent<Drawable>() && e.HasComponent<TextBlock>();

        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            TextBlock tb = entity.GetComponent<TextBlock>();

            if (!tb.Animated) return;

            tb.ProperTime += deltaTime;

            int x = 0;

            for (int l = 0; l < tb.Lines.Count; l++)
            {
                var line = tb.Lines[l];

                for (int i = 0; i < line.Count; i++)
                {
                    if (tb.ProperTime >= x * tb.TimePerLetter)
                    {
                        if (line[i].IsRenderable == false && !tb.AnimationSkipped)
                        {
                            Random rand = new Random();
                            Game.AudioManager.PlayEffect("letter", -rand.Next(0, 100) / 100f);
                        }
                        line[i].IsRenderable = true;
                    }
                    else
                    {
                        line[i].IsRenderable = false;
                    }

                    x++;
                }
            }
        }
    }
}
