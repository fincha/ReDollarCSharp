using System;
using System.Collections.Generic;

namespace DollarRecognition
{
    class Vector
    {
        public List<double> vector;

        public Vector(Point[] points)
        {
            float sum = 0;
            vector = new List<double>();
            int len = points.Length;

            for (int i = 0; i < len; i++)
            {
                float x = points[i].x;
                float y = points[i].y;
                vector.Add(x);
                vector.Add(y);
                sum += x * x + y * y;
            }

            double magnitude = Math.Sqrt(sum);
            len <<= 1;

            for (int i = 0; i < len; i++)
            {
                vector[i] /= magnitude;
            }
        }
    }
}
