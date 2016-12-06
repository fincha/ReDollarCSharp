using System;
using DollarRecognition;
using NUnit.Framework;

namespace NUnit.DollarRecognition {
    [TestFixture]
	public class PolylineTest 
	{
        private Point p; 
        private Vector v;

        [SetUp]
		protected void SetUp() 
		{
            p = new Point(1f, 2f);
            v = new Vector(new Point[] { p });
        }
    }
} 