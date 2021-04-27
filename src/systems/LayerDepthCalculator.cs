using System;
using System.Diagnostics;

namespace Abyss_Call
{
    public class LayerDepthCalculator : System
    {
        public override bool Requirements(Entity e) => 
            e.HasComponent<Transform>() && e.HasComponent<Drawable>() && 
            (e.HasComponent<NPCC>() || e.HasComponent<Playable>());

        public override void Update(double deltaTime)
        {
            if (EntityBucket.Count == 0)
                return;

            float maxLD = EntityBucket[0].GetComponent<Drawable>().LayerDepth;
            float minLD = maxLD;
            float minY = EntityBucket[0].GetComponent<Transform>().Position.Y;
            float maxY = minY;

            foreach (Entity e in EntityBucket)
            {
                maxLD = Math.Max(maxLD, e.GetComponent<Drawable>().LayerDepth);
                minLD = Math.Min(minLD, e.GetComponent<Drawable>().LayerDepth);

                minY = Math.Min(minY, e.GetComponent<Transform>().Position.Y);
                maxY = Math.Max(maxY, e.GetComponent<Transform>().Position.Y);
            }

            foreach (Entity e in EntityBucket)
            {
                // 0 to 1                                   minY to maxY
                e.GetComponent<Drawable>().LayerDepth = (minY - e.GetComponent<Transform>().Position.Y) / (minY - maxY);
            }
        }
    }
}
