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
        public double LifePercent => ProperTime/Lifetime;
        public double Lifetime { get; set; } = 500;
        private double _properTime;
        public double ProperTime
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
            _properTime = 0;
        }
    }
}
