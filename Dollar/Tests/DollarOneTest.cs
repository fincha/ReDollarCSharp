using System;
using DollarRecognition;
using NUnit.Framework;

namespace NUnit.DollarRecognition
{
    [TestFixture]
    public class DollarOneTest
    {
        private DollarOne d;

        [SetUp]
        protected void SetUp()
        {
            d = new DollarOne();
            d.addGesture("line1", new Point[] {
                new Point(0f, 0f),
                new Point(10f, 0f)
            });

            d.addGesture("square", new Point[] {
                new Point(0f, 0f),
                new Point(0f, 40f),
                new Point(40f, 40f),
                new Point(40f, 0f),
                new Point(0f, 0f)
            });
        }

        [Test]
        public void Recognize()
        {
            string GestureName = d.recognize(new Point[]
            {
                new Point(1f, 0f),
                new Point(20f, 0f)
            });

            Assert.AreEqual("line1", GestureName);

            GestureName = d.recognize(new Point[]
            {
                 new Point(0f, 0f),
                new Point(0f, 40f),
                new Point(40f, 40f),
                new Point(40f, 0f),
                new Point(0f, 0f)
            });

            Assert.AreEqual("square", GestureName);


            GestureName = d.recognize(new Point[]
            {
                new Point(5f, 0f),
                new Point(0f, 37f),
                new Point(42f, 45f),
                new Point(42f, 6f),
                new Point(3f, 3f)
            });

            Assert.AreEqual("square", GestureName);
        }
    }
}