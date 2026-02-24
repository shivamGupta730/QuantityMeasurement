using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement;

namespace QuantityMeasurementTests
{
    [TestClass]
    public class QuantityMeasurementAppTest
    {
        private QuantityMeasurementService service;

        [TestInitialize]
        public void Setup()
        {
            service = new QuantityMeasurementService();
        }

        // -------- SAME UNIT TESTS --------

        [TestMethod]
        public void Feet_SameValue_ReturnsTrue()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            var l2 = new Length(1.0, LengthUnit.Feet);

            Assert.IsTrue(service.CompareLengths(l1, l2));
        }

        [TestMethod]
        public void Feet_DifferentValue_ReturnsFalse()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            var l2 = new Length(2.0, LengthUnit.Feet);

            Assert.IsFalse(service.CompareLengths(l1, l2));
        }

        [TestMethod]
        public void Inches_SameValue_ReturnsTrue()
        {
            var l1 = new Length(5.0, LengthUnit.Inches);
            var l2 = new Length(5.0, LengthUnit.Inches);

            Assert.IsTrue(service.CompareLengths(l1, l2));
        }

        [TestMethod]
        public void Inches_DifferentValue_ReturnsFalse()
        {
            var l1 = new Length(5.0, LengthUnit.Inches);
            var l2 = new Length(10.0, LengthUnit.Inches);

            Assert.IsFalse(service.CompareLengths(l1, l2));
        }

        // -------- CROSS UNIT TEST --------

        [TestMethod]
        public void FeetAndInches_Equivalent_ReturnsTrue()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            var l2 = new Length(12.0, LengthUnit.Inches);

            Assert.IsTrue(service.CompareLengths(l1, l2));
        }

        // -------- NULL TEST --------

        [TestMethod]
        public void NullComparison_ReturnsFalse()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);

            Assert.IsFalse(l1.Equals(null));
        }

        // -------- SAME REFERENCE --------

        [TestMethod]
        public void SameReference_ReturnsTrue()
        {
            var l1 = new Length(5.0, LengthUnit.Inches);

            Assert.IsTrue(l1.Equals(l1));
        }
    }
}