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

        // ================= UC5 : CONVERSION =================

        [TestMethod]
        public void Convert_FeetToInches()
        {
            double result = Length.Convert(1.0, LengthUnit.Feet, LengthUnit.Inches);
            Assert.AreEqual(12.0, result, 1e-6);
        }

        [TestMethod]
        public void Convert_InchesToFeet()
        {
            double result = Length.Convert(24.0, LengthUnit.Inches, LengthUnit.Feet);
            Assert.AreEqual(2.0, result, 1e-6);
        }

        [TestMethod]
        public void Convert_YardsToFeet()
        {
            double result = Length.Convert(3.0, LengthUnit.Yards, LengthUnit.Feet);
            Assert.AreEqual(9.0, result, 1e-6);
        }

        [TestMethod]
        public void Convert_CentimetersToInches()
        {
            double result = Length.Convert(2.54, LengthUnit.Centimeters, LengthUnit.Inches);
            Assert.AreEqual(1.0, result, 1e-6);
        }

        [TestMethod]
        public void Convert_InvalidValue_Throws()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                Length.Convert(double.NaN, LengthUnit.Feet, LengthUnit.Inches));
        }

        // ================= UC6 : ADDITION =================

        [TestMethod]
        public void Addition_SameUnit_FeetPlusFeet()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            var l2 = new Length(2.0, LengthUnit.Feet);

            var result = l1.Add(l2);

            Assert.AreEqual(new Length(3.0, LengthUnit.Feet), result);
        }

        [TestMethod]
        public void Addition_CrossUnit_FeetPlusInches()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            var l2 = new Length(12.0, LengthUnit.Inches);

            var result = l1.Add(l2);

            Assert.AreEqual(new Length(2.0, LengthUnit.Feet), result);
        }

        [TestMethod]
        public void Addition_Commutativity()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            var l2 = new Length(12.0, LengthUnit.Inches);

            var result1 = l1.Add(l2);
            var result2 = l2.Add(l1).ConvertTo(LengthUnit.Feet);

            Assert.AreEqual(result1, result2);
        }

        [TestMethod]
        public void Addition_WithZero()
        {
            var l1 = new Length(5.0, LengthUnit.Feet);
            var l2 = new Length(0.0, LengthUnit.Inches);

            var result = l1.Add(l2);

            Assert.AreEqual(l1, result);
        }

        [TestMethod]
        public void Addition_NegativeValues()
        {
            var l1 = new Length(5.0, LengthUnit.Feet);
            var l2 = new Length(-2.0, LengthUnit.Feet);

            var result = l1.Add(l2);

            Assert.AreEqual(new Length(3.0, LengthUnit.Feet), result);
        }
        [TestMethod]
public void Addition_WithExplicitTarget_Feet()
{
    var l1 = new Length(1.0, LengthUnit.Feet);
    var l2 = new Length(12.0, LengthUnit.Inches);

    var result = l1.Add(l2, LengthUnit.Feet);

    Assert.AreEqual(new Length(2.0, LengthUnit.Feet), result);
}

[TestMethod]
public void Addition_WithExplicitTarget_Inches()
{
    var l1 = new Length(1.0, LengthUnit.Feet);
    var l2 = new Length(12.0, LengthUnit.Inches);

    var result = l1.Add(l2, LengthUnit.Inches);

    Assert.AreEqual(new Length(24.0, LengthUnit.Inches), result);
}

[TestMethod]
public void Addition_WithExplicitTarget_Yards()
{
    var l1 = new Length(1.0, LengthUnit.Feet);
    var l2 = new Length(12.0, LengthUnit.Inches);

    var result = l1.Add(l2, LengthUnit.Yards);

    Assert.AreEqual(new Length(2.0 / 3.0, LengthUnit.Yards), result);
}
// ================= UC8 : STANDALONE ENUM TESTS =================

[TestMethod]
public void LengthUnit_Feet_ConvertToBase()
{
    double result = LengthUnit.Feet.ConvertToBaseUnit(5.0);
    Assert.AreEqual(5.0, result, 1e-6);
}

[TestMethod]
public void LengthUnit_Inches_ConvertToBase()
{
    double result = LengthUnit.Inches.ConvertToBaseUnit(12.0);
    Assert.AreEqual(1.0, result, 1e-6);
}

[TestMethod]
public void LengthUnit_Yards_ConvertToBase()
{
    double result = LengthUnit.Yards.ConvertToBaseUnit(1.0);
    Assert.AreEqual(3.0, result, 1e-6);
}

[TestMethod]
public void LengthUnit_Centimeters_ConvertToBase()
{
    double result = LengthUnit.Centimeters.ConvertToBaseUnit(30.48);
    Assert.AreEqual(1.0, result, 1e-6);
}

[TestMethod]
public void LengthUnit_FromBase_ToInches()
{
    double result = LengthUnit.Inches.ConvertFromBaseUnit(1.0);
    Assert.AreEqual(12.0, result, 1e-6);
}

[TestMethod]
public void QuantityLength_Refactored_ConvertTo()
{
    var length = new Length(1.0, LengthUnit.Feet);
    var result = length.ConvertTo(LengthUnit.Inches);

    Assert.AreEqual(new Length(12.0, LengthUnit.Inches), result);
}

[TestMethod]
public void QuantityLength_Refactored_Add_WithTargetUnit()
{
    var l1 = new Length(1.0, LengthUnit.Feet);
    var l2 = new Length(12.0, LengthUnit.Inches);

    var result = l1.Add(l2, LengthUnit.Yards);

    Assert.AreEqual(0.666667, result.Value, 1e-6);
    Assert.AreEqual(LengthUnit.Yards, result.Unit);
}
    }
}