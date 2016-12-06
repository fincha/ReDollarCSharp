using System;
using System.Collections;
using System.Collections.Generic;

namespace DollarRecognition
{
    class Polyline
    {
        private Point[] points;
        private Point[] origPoints;
        public double ratio1D = 0.2;
        public double rotationInvariance = Math.PI / 4;
        public int normalPointCount = 40;
        public int normalSize = 200;
        public bool ignoreRotate = false;
        public Vector vector;

        private Point firstPoint;
        private Point centroid;
        private float[] aabb;
        private double angle;

        public Polyline(Point[] p)
        {
            points = p;
        }

        public void init(bool transform = false)
        {
            origPoints = points;
            
            if (transform)
            {
                points = Utils.resample(origPoints, normalPointCount).ToArray();
            }

            firstPoint = points[0];
            centroid = getCentroid();
            translateTo(new Point(0, 0));

            aabb = Utils.getAABB(points);
            if (transform)
            {
                scaleTo(normalSize, null);
                angle = indicativeAngle();
                rotateBy(-angle);
            }
            vector = new Vector(points);
        }

        public double length()
        {
            return Utils.polylineLength(points);
        }

        public double indicativeAngle()
        {
            double iAngle = Math.Atan2(firstPoint.y, firstPoint.x);
            if (rotationInvariance > 0)
            {
                double r = rotationInvariance;
                double baseOrientation = r * Math.Floor((iAngle + r / 2) / r);
                return iAngle - baseOrientation;
            }
            return iAngle;
        }

        public Point getCentroid()
        {
            float x = 0, y = 0;
            for (int i = 0; i < points.Length; i++)
            {
                x += points[i].x;
                y += points[i].y;
            }

            x /= points.Length;
            y /= points.Length;

            return new Point(x, y);
        }

        public void translateTo(Point point)
        {
            Point c = centroid;

            c.x -= point.x;
            c.y -= point.y;

            for (int i = 0; i < points.Length; i++)
            {
                Point p = points[i];
                float qx = p.x - c.x;
                float qy = p.y - c.y;
                p.x = qx;
                p.y = qy;

                points[i] = p;
            }
        }

        public void rotateBy(double radians)
        {
            double cos = Math.Cos(radians);
            double sin = Math.Sin(radians);
            for (var i = 0; i < points.Length; i++)
            {
                Point p = points[i];
                double qx = p.x * cos - p.y * sin;
                double qy = p.x * sin + p.y * cos;
                p.x = Convert.ToSingle(qx);
                p.y = Convert.ToSingle(qy);

                points[i] = p;
            }
        }

        public void scale(float scaleX, float scaleY)
        {
            for (var i = 0; i < points.Length; i++)
            {
                Point p = points[i];
                var qx = p.x * scaleX;
                var qy = p.y * scaleY;
                p.x = qx;
                p.y = qy;

                points[i] = p;
            }
        }

        public void scaleTo(double width, double? height)
        {
            height = height.HasValue ? height.Value : width;

            double scaleX, scaleY;
            if (ratio1D > 0)
            {
                double longSide = Math.Max(aabb[4], aabb[5]);
                double shortSide = Math.Min(aabb[4], aabb[5]);
                bool uniformly = shortSide / longSide < ratio1D;
                if (uniformly)
                {
                    scaleX = width / longSide;
                    scaleY = height.Value / longSide;
                    scale(Convert.ToSingle(scaleX), Convert.ToSingle(scaleY));
                }
            }
            else {
                scaleX = width / aabb[4];
                scaleY = height.Value / aabb[5];

                scale(Convert.ToSingle(scaleX), Convert.ToSingle(scaleY));
            }
        }
    }
}
