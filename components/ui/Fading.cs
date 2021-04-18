using System;

namespace Abyss_Call
{
    public enum FadingStyle
    {
        None, FadeIn, FadeOut
    }
    class Fading : Component
    {
        private FadingStyle _style;
        public FadingStyle Style
        {
            get
            {
                return _style;
            }
            set
            {
                if (_style != value && ProperTime != 0)
                    ProperTime = Lifetime - ProperTime;

                _style = value;
            }
        }
        public double LifePercent => ProperTime/Lifetime;
        public double Lifetime { get; set; } = 1000;
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
            _style = FadingStyle.FadeIn;
        }
    }
}
