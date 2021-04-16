using Microsoft.Xna.Framework;

namespace Abyss_Call
{
    public static class MathsManager
    {
        public static bool LineLineIntersect(Point a1, Point a2, Point b1, Point b2)
        {
            float t_num = (a1.X - b1.X) * (b1.Y - b2.Y) - (a1.Y - b1.Y) * (b1.X - b2.X);
            float u_num = -(a1.X - a2.X) * (a1.Y - b1.Y) + (a1.Y - a2.Y) * (a1.X - b1.X);

            float denum = (a1.X - a2.X) * (b1.Y - b2.Y) - (a1.Y - a2.Y) * (b1.X - b2.X);

            if (denum == 0) return false;

            return (t_num >= 0 && t_num <= denum) && (u_num >= 0 && u_num <= denum);
        }

        public static bool LineRectangleIntersect(Point p1, Point p2, Rectangle rectangle)
        {
            Point topLeft = new Point(rectangle.Top, rectangle.Left);
            Point topRight = new Point(rectangle.Top, rectangle.Right);
            Point bottomLeft = new Point(rectangle.Bottom, rectangle.Left);
            Point bottomRight = new Point(rectangle.Bottom, rectangle.Right);

            bool top = LineLineIntersect(p1, p2, topLeft, topRight);
            bool bottom = LineLineIntersect(p1, p2, bottomLeft, bottomRight);
            bool left = LineLineIntersect(p1, p2, topLeft, bottomLeft);
            bool right = LineLineIntersect(p1, p2, topRight, bottomRight);

            return top || bottom || left || right;
        }
    }
}
