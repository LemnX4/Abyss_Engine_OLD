using System;


namespace Abyss_Call
{
    public enum AnimationType
    {
        Linear, Random
    }
    public class Animation : Component
    {
        public AnimationType Type = AnimationType.Linear;
        public bool Loop { set; get; } = true;
        public int Keyframe { private set; get; }
        public int MaxKeyframe { set; get; } = 3;

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
        public int TimePerFrame { set; get; } = 200;

        public int MaxTime => MaxKeyframe * TimePerFrame;

        public Animation()
        {
            _properFrameTime = 0;
        }
    }
}
