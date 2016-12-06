using System;
using DollarRecognition;
using NUnit.Framework;

namespace NUnit.DollarRecognition
{
    [TestFixture]
    public class PointTest
    {
        private Point p;
        private Vector v;

        [SetUp]
        protected void SetUp()
        {
            p = new Point(1f, 2f);
            v = new Vector(new Point[] { p });
        }

        [Test]
        public void Initiliazing()
        {
            Assert.AreEqual(p.x, 1f);
            Assert.AreEqual(p.y, 2f);
        }
    }
}