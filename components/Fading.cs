using System;

namespace Abyss_Call
{
    class Fading : Component
    {
        public enum FadingStyle
        {
            None, FadeIn, FadeOut
        }

        public FadingStyle Style { get; set; } = FadingStyle.FadeIn;
        public float LifePercent => ((float)ProperTime)/Lifetime;
        public int Lifetime { get; set; } = 500;
        private int _properTime;
        public int ProperTime
        {
            get
            {
                return _properTime;
            }
            set
            {
                _properTime = Math.Max(0, Math.Min(value, Lifetime));
            }
        }
        public Fading()
        {
            Tag = "fading";
            _properTime = 0;
        }
    }
}
