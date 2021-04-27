using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;


namespace Abyss_Call
{
    public static class MathsManager
    {
        public static int PointsToDirection(Vector2 p1, Vector2 p2)
        {
            int angle = (int)(AngleBetween(p1, p2) + 45) % 360;

            if (angle > 0 && angle <= 90) return 3;
            if (angle > 90 && angle <= 2 * 90) return 0;
            if (angle > 2 * 90 && angle <= 3 * 90) return 2;
            if (angle > 3 * 90 && angle <= 4 * 90) return 1;
            return 0;
        }
        public static double AngleBetween(Vector2 p1, Vector2 p2)
        {
            return Math.Atan2(p1.Y - p2.Y, p1.X - p2.X) * 180 / Math.PI + 180;
        }
        public static bool LineLineIntersect(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
        {
            float t_num = (a1.X - b1.X) * (b1.Y - b2.Y) - (a1.Y - b1.Y) * (b1.X - b2.X);
            float u_num = -(a1.X - a2.X) * (a1.Y - b1.Y) + (a1.Y - a2.Y) * (a1.X - b1.X);

            float denum = (a1.X - a2.X) * (b1.Y - b2.Y) - (a1.Y - a2.Y) * (b1.X - b2.X);

            if (denum == 0) return false;

            return t_num >= 0 && t_num <= denum && u_num >= 0 && u_num <= denum;
        }

        public static bool LineRectangleIntersect(Vector2 p1, Vector2 p2, Rectangle rectangle)
        {
            Vector2 topLeft = new Vector2(rectangle.Top, rectangle.Left);
            Vector2 topRight = new Vector2(rectangle.Top, rectangle.Right);
            Vector2 bottomLeft = new Vector2(rectangle.Bottom, rectangle.Left);
            Vector2 bottomRight = new Vector2(rectangle.Bottom, rectangle.Right);

            bool top = LineLineIntersect(p1, p2, topLeft, topRight);
            bool bottom = LineLineIntersect(p1, p2, bottomLeft, bottomRight);
            bool left = LineLineIntersect(p1, p2, topLeft, bottomLeft);
            bool right = LineLineIntersect(p1, p2, topRight, bottomRight);

            return top || bottom || left || right;
        }
    }
}
