using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement;

namespace QuantityMeasurementTests
{
    [TestClass]
    public class FeetTests
    {
        [TestMethod]
        public void Equals_SameValue_ReturnsTrue()
        {
            var first = new QuantityMeasurementApp.Feet(1.0);
            var second = new QuantityMeasurementApp.Feet(1.0);

            Assert.IsTrue(first.Equals(second));
        }

        [TestMethod]
        public void Equals_DifferentValue_ReturnsFalse()
        {
            var first = new QuantityMeasurementApp.Feet(1.0);
            var second = new QuantityMeasurementApp.Feet(2.0);

            Assert.IsFalse(first.Equals(second));
        }

        [TestMethod]
        public void Equals_Null_ReturnsFalse()
        {
            var first = new QuantityMeasurementApp.Feet(1.0);

            Assert.IsFalse(first.Equals(null));
        }

        [TestMethod]
        public void Equals_SameReference_ReturnsTrue()
        {
            var first = new QuantityMeasurementApp.Feet(1.0);

            Assert.IsTrue(first.Equals(first));
        }
    }
}