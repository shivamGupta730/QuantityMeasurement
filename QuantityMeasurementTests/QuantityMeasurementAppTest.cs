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

        // ================= UC1 : SAME UNIT =================

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

        // ================= UC2 : CROSS UNIT =================

        [TestMethod]
        public void FeetAndInches_Equivalent_ReturnsTrue()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            var l2 = new Length(12.0, LengthUnit.Inches);

            Assert.IsTrue(service.CompareLengths(l1, l2));
        }

        // ================= UC4 : YARDS =================

        [TestMethod]
        public void OneYard_Equals_ThreeFeet()
        {
            var yard = new Length(1.0, LengthUnit.Yards);
            var feet = new Length(3.0, LengthUnit.Feet);

            Assert.IsTrue(service.CompareLengths(yard, feet));
        }

        [TestMethod]
        public void OneYard_Equals_ThirtySixInches()
        {
            var yard = new Length(1.0, LengthUnit.Yards);
            var inches = new Length(36.0, LengthUnit.Inches);

            Assert.IsTrue(service.CompareLengths(yard, inches));
        }

        [TestMethod]
        public void Yards_DifferentValue_ReturnsFalse()
        {
            var l1 = new Length(1.0, LengthUnit.Yards);
            var l2 = new Length(2.0, LengthUnit.Yards);

            Assert.IsFalse(service.CompareLengths(l1, l2));
        }

        // ================= UC4 : CENTIMETERS =================

        [TestMethod]
        public void OneCm_Equals_Point393701_Inch()
        {
            var cm = new Length(1.0, LengthUnit.Centimeters);
            var inch = new Length(0.393701, LengthUnit.Inches);

            Assert.IsTrue(service.CompareLengths(cm, inch));
        }

        [TestMethod]
        public void Cm_NotEquals_Feet()
        {
            var cm = new Length(1.0, LengthUnit.Centimeters);
            var feet = new Length(1.0, LengthUnit.Feet);

            Assert.IsFalse(service.CompareLengths(cm, feet));
        }

        // ================= REFLEXIVE & NULL =================

        [TestMethod]
        public void SameReference_ReturnsTrue()
        {
            var l1 = new Length(5.0, LengthUnit.Inches);

            Assert.IsTrue(l1.Equals(l1));
        }

        [TestMethod]
        public void NullComparison_ReturnsFalse()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);

            Assert.IsFalse(l1.Equals(null));
        }
    }
}