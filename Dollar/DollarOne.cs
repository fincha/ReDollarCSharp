using System;
using System.Collections;
using System.Collections.Generic;

namespace DollarRecognition
{
    class DollarOne
    {
        List<Gesture> gesturePool;

        double threshold = 0.3;

        double ratio1D = 0.2;
        double rotationInvariance = Math.PI / 4;
        int normalPointCount = 40;
        int normalSize = 200;

        public DollarOne()
        {
            gesturePool = new List<Gesture>();
        }

        public string recognize(Point[] points, bool first = true)
        {
            string match = "";
            Polyline polyline = createPolyline(points);
            polyline.init();
            Vector vector = polyline.vector;
            double minDis = threshold;

            foreach (Gesture gesture in gesturePool)
            {
                var d = Utils.cosDistance(gesture.vector, vector);
                if (d < minDis)
                {
                    minDis = d;
                    match = gesture.name;
                    if (first)
                    {
                        return match;
                    }
                }
            }

            return match;
        }

        public Polyline createPolyline(Point[] points)
        {
            Polyline polyline = new Polyline(points);

            polyline.ratio1D = ratio1D;
            polyline.rotationInvariance = rotationInvariance;
            polyline.normalPointCount = normalPointCount;
            polyline.normalSize = normalSize;

            return polyline;
        }

        public void addGesture(string name, Point[] points, bool transform = false)
        {
            Polyline polyline = createPolyline(points);
            polyline.init(transform);
            gesturePool.Add(new Gesture(name, polyline.vector));
        }
    }
}