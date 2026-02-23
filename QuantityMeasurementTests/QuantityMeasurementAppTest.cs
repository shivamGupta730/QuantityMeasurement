using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement;

namespace QuantityMeasurementTests
{
    [TestClass]
    public class QuantityMeasurementAppTest
    {
        // ---------------- FEET TESTS ----------------

        [TestMethod]
        public void Feet_SameValue_ReturnsTrue()
        {
            var f1 = new QuantityMeasurementApp.Feet(1.0);
            var f2 = new QuantityMeasurementApp.Feet(1.0);

            Assert.IsTrue(f1.Equals(f2));
        }

        [TestMethod]
        public void Feet_DifferentValue_ReturnsFalse()
        {
            var f1 = new QuantityMeasurementApp.Feet(1.0);
            var f2 = new QuantityMeasurementApp.Feet(2.0);

            Assert.IsFalse(f1.Equals(f2));
        }

        [TestMethod]
        public void Feet_NullComparison_ReturnsFalse()
        {
            var f1 = new QuantityMeasurementApp.Feet(1.0);

            Assert.IsFalse(f1.Equals(null));
        }

        [TestMethod]
        public void Feet_SameReference_ReturnsTrue()
        {
            var f1 = new QuantityMeasurementApp.Feet(1.0);

            Assert.IsTrue(f1.Equals(f1));
        }

        // ---------------- INCHES TESTS ----------------

        [TestMethod]
        public void Inches_SameValue_ReturnsTrue()
        {
            var i1 = new QuantityMeasurementApp.Inches(5.0);
            var i2 = new QuantityMeasurementApp.Inches(5.0);

            Assert.IsTrue(i1.Equals(i2));
        }

        [TestMethod]
        public void Inches_DifferentValue_ReturnsFalse()
        {
            var i1 = new QuantityMeasurementApp.Inches(5.0);
            var i2 = new QuantityMeasurementApp.Inches(10.0);

            Assert.IsFalse(i1.Equals(i2));
        }

        [TestMethod]
        public void Inches_NullComparison_ReturnsFalse()
        {
            var i1 = new QuantityMeasurementApp.Inches(5.0);

            Assert.IsFalse(i1.Equals(null));
        }

        [TestMethod]
        public void Inches_SameReference_ReturnsTrue()
        {
            var i1 = new QuantityMeasurementApp.Inches(5.0);

            Assert.IsTrue(i1.Equals(i1));
        }
    }
}