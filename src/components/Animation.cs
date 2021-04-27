using System;
using System.Diagnostics;

namespace Abyss_Call
{
    public enum AnimationType
    {
        Linear, Random
    }
    public class Animation : Component
    {
        private bool _animated = true;
        public bool Animated
        {
            get
            {
                return _animated;
            }
            set
            {
                if (value && !_animated)
                {
                    if (HostEntity.GetType().Name == nameof(NPC))
                    {
                        Random rand = new Random();
                        Keyframe = rand.Next(0, 2) * 2;
                    }
                }
                else if (!value && _animated)
                {
                    _properFrameTime = 0;
                    Keyframe = 0;
                }

                _animated = value;
            }
        }
        public AnimationType Type { get; set; } = AnimationType.Linear;
        public bool Loop { get; set; } = true;
        public int Keyframe { get; private set; }
        public int MaxKeyframe { get; set; } = 4;

        private double _properFrameTime;
        public double ProperFrameTime
        {
            get
            {
                return _properFrameTime;
            }
            set
            {
                _properFrameTime = value;

                if (!Animated)
                    return;

                if (_properFrameTime >= TimePerFrame)
                {
                    switch (Type)
                    {
                        case AnimationType.Linear:
                            Keyframe = Loop ? (Keyframe + 1) % MaxKeyframe : Math.Min(Keyframe+1, MaxKeyframe);
                            break;
                        case AnimationType.Random:
                            Random rnd = new Random();
                            Keyframe = rnd.Next(0, MaxKeyframe);
                            break;
                    }
                    _properFrameTime = 0;
                }
            }
        }
        public int TimePerFrame { get; set; } = 150;

        public int MaxTime => MaxKeyframe * TimePerFrame;

        public Animation()
        {
            _properFrameTime = 0;
        }
    }
}
