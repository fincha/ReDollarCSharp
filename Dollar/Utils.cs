using System;
using System.Collections;
using System.Collections.Generic;

namespace DollarRecognition
{
    class Utils
    {
        const double DEG_TO_RAD = Math.PI / 360f;
        const double RAD_TO_DEG = 180 / Math.PI;
        const double HALF_PI = Math.PI / 2;
        const double DOUBLE_PI = Math.PI * 2;

        private Point[] points;
        
        public Utils(Point[] p)
        {
            points = p;
        }

        public static double distance(Point p1, Point p2) {
            var dx = p2.x - p1.x;
            var dy = p2.y - p1.y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static float[] getAABB(Point[] points)
        {
            float minX = float.MaxValue;
                float maxX = float.MinValue;
            float minY = float.MaxValue;
                float maxY = float.MinValue;
            for (int i = 0, len = points.Length; i < len; i++)
            {
                Point p = points[i];
                minX = Math.Min(minX, p.x);
                maxX = Math.Max(maxX, p.x);
                minY = Math.Min(minY, p.y);
                maxY = Math.Max(maxY, p.y);
            }
            
            return new float[6] { minX, minY, maxX, maxY, maxX - minX, maxY - minY };
        }

        public double cosSimilarity (Vector vector1, Vector vector2)
        {
            double dot = 0;
            double sum1 = 0;
            double sum2 = 0;

            for (var i = 0; i < vector1.vector.Count; i++)
            {
                double v1 = vector1.vector[i],
                    v2 = vector1.vector[i];
                dot += v1 * v2;
                sum1 += v1 * v1;
                sum2 += v2 * v2;
            }
            return dot / Math.Sqrt(sum1 * sum2);
        }

        public double cosDistance(Vector vector1, Vector vector2)
        {
            // return 1-Utils.cosSimilarity(vector1,vector2);
            double a = 0;
            double b = 0;
            for (int i = 0; i < vector1.vector.Count; i += 2)
            {
                a += vector1.vector[i] * vector2.vector[i] + vector1.vector[i + 1] * vector2.vector[i + 1];
                b += vector1.vector[i] * vector2.vector[i + 1] - vector1.vector[i + 1] * vector2.vector[i];
            }
            double angle = Math.Atan(b / a);
            double d = Math.Acos(a * Math.Cos(angle) + b * Math.Sin(angle));
            return d;
        }

        public static double polylineLength(Point[] points)
        {
            double d = 0;

            for (int i = 1, len = points.Length; i < len; i++)
            {
                d += distance(points[i - 1], points[i]);
            }

            return d;
        }

        public static List<Point> resample(Point[] points, double n)
        {
            double I = polylineLength(points) / (n - 1);
            double D = 0;
            Point p1 = points[0];
            List<Point> newPoints = new List<Point>();
            newPoints.Add(
                new Point(p1.x, p1.y)
            );

            int len = points.Length;
            for (int i = 1; i < len;)
            {
                var p2 = points[i];
                var d = Utils.distance(p1, p2);
                if ((D + d) >= I)
                {
                    var k = (I - D) / d;
                    float qx = Convert.ToSingle(p1.x + k * (p2.x - p1.x));
                    float qy = Convert.ToSingle(p1.y + k * (p2.y - p1.y));
                    Point q = new Point(qx, qy);
                    newPoints.Add(q);
                    D = 0;
                    p1 = q;
                }
                else {
                    D += d;
                    p1 = p2;
                    i++;
                }
            }

            if (newPoints.Count == n - 1)
            {
                newPoints.Add(new Point(points[len - 1].x, points[len - 1].y));
            }

            return newPoints;
        }
    }
}