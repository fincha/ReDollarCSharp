using System;
using DollarRecognition;
using NUnit.Framework;

namespace NUnit.DollarRecognition
{
    [TestFixture]
    public class VectorTest
    {
        [Test]
        public void Vectorize()
        {
            Point p1 = new Point(1f, 2f);
            Point p2 = new Point(2f, 2f);
            Vector v1 = new Vector(new Point[] { p1 });
            Vector v2 = new Vector(new Point[] { p1, p2 });

            Assert.AreEqual(v1.vector.Count, 2);
            Assert.AreEqual(v2.vector.Count, 4);
        }
    }
}
