using System;

namespace DollarRecognition
{
    class Gesture
    {
        public string name;
        public Vector vector;

        public Gesture(string n, Vector v)
        {
            name = n;
            vector = v;
        }
    }
}

