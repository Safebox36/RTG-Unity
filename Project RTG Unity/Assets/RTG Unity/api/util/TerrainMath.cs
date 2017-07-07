namespace rtg.api.util
{
    using System;
    public class TerrainMath
    {
        public static double nextX(double x, double d, double s)
        {
            return x + (s * Math.Cos(d * Math.PI / 180.0));
        }

        public static double nextY(double y, double d, double s)
        {
            return y + (s * Math.Sin(d * Math.PI / 180.0));
        }

        public static double dis1(double n1, double n2)
        {
            return Math.Sqrt((n1 - n2) * (n1 - n2));
        }

        public static double dis2(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }
        public static double dis2Elliptic(double x1, double y1, double x2, double y2, double fX, double fY)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) / fX * fX + (y1 - y2) * (y1 - y2) / fY * fY);
        }

        public static float lerp(float a, float b, float f)
        {
            return (a * (1.0f - f)) + (b * f);
        }

        public static double dis3(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) + (z1 - z2) * (z1 - z2));
        }

        public static double dirToPoint2(double x1, double y1, double x2, double y2)
        {
            return Math.Atan2((y2 - y1), (x2 - x1)) * 180 / Math.PI;
        }
    }
}