using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurement;

namespace QuantityMeasurementTests
{
    [TestClass]
    public class UC9_GenericQuantityTests
    {
        private const double EPSILON = 1e-6;

        // ================= BASE ENUM RESPONSIBILITY =================

        [TestMethod]
        public void LengthUnit_ConvertToBase_Feet()
        {
            double result = LengthUnit.Feet.ConvertToBaseUnit(5.0);
            Assert.AreEqual(5.0, result, EPSILON);
        }

        [TestMethod]
        public void LengthUnit_ConvertToBase_Inches()
        {
            double result = LengthUnit.Inches.ConvertToBaseUnit(12.0);
            Assert.AreEqual(1.0, result, EPSILON);
        }

        [TestMethod]
        public void LengthUnit_ConvertFromBase_Yards()
        {
            double result = LengthUnit.Yards.ConvertFromBaseUnit(3.0);
            Assert.AreEqual(1.0, result, EPSILON);
        }

        // ================= GENERIC EQUALITY =================

        [TestMethod]
        public void GenericQuantity_Equality_CrossUnit()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            var l2 = new Length(12.0, LengthUnit.Inches);

            Assert.IsTrue(l1.Equals(l2));
        }

        [TestMethod]
        public void GenericQuantity_Equality_DifferentValue()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            var l2 = new Length(2.0, LengthUnit.Feet);

            Assert.IsFalse(l1.Equals(l2));
        }

        // ================= GENERIC CONVERSION =================

        [TestMethod]
        public void GenericQuantity_ConvertTo()
        {
            var length = new Length(1.0, LengthUnit.Feet);
            var result = length.ConvertTo(LengthUnit.Inches);

            Assert.AreEqual(new Length(12.0, LengthUnit.Inches), result);
        }

        [TestMethod]
        public void GenericQuantity_RoundTripConversion()
        {
            var original = new Length(5.0, LengthUnit.Feet);

            var inches = original.ConvertTo(LengthUnit.Inches);
            var backToFeet = inches.ConvertTo(LengthUnit.Feet);

            Assert.IsTrue(original.Equals(backToFeet));
        }

        // ================= GENERIC ADDITION =================

        [TestMethod]
        public void GenericQuantity_Add_SameUnit()
        {
            var l1 = new Length(2.0, LengthUnit.Feet);
            var l2 = new Length(3.0, LengthUnit.Feet);

            var result = l1.Add(l2);

            Assert.AreEqual(new Length(5.0, LengthUnit.Feet), result);
        }

        [TestMethod]
        public void GenericQuantity_Add_CrossUnit()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            var l2 = new Length(12.0, LengthUnit.Inches);

            var result = l1.Add(l2, LengthUnit.Feet);

            Assert.AreEqual(new Length(2.0, LengthUnit.Feet), result);
        }

        [TestMethod]
        public void GenericQuantity_Add_WithExplicitTarget_Yards()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            var l2 = new Length(12.0, LengthUnit.Inches);

            var result = l1.Add(l2, LengthUnit.Yards);

            Assert.AreEqual(0.6666666666666666, result.Value, EPSILON);
            Assert.AreEqual(LengthUnit.Yards, result.Unit);
        }

        [TestMethod]
        public void GenericQuantity_Add_Commutativity()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            var l2 = new Length(12.0, LengthUnit.Inches);

            var r1 = l1.Add(l2, LengthUnit.Feet);
            var r2 = l2.Add(l1, LengthUnit.Feet);

            Assert.IsTrue(r1.Equals(r2));
        }

        // ================= EDGE CASES =================

        [TestMethod]
        public void GenericQuantity_Add_WithZero()
        {
            var l1 = new Length(5.0, LengthUnit.Feet);
            var l2 = new Length(0.0, LengthUnit.Inches);

            var result = l1.Add(l2);

            Assert.AreEqual(l1, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GenericQuantity_InvalidValue_Throws()
        {
            var l = new Length(double.NaN, LengthUnit.Feet);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GenericQuantity_Add_Null_Throws()
        {
            var l1 = new Length(1.0, LengthUnit.Feet);
            l1.Add(null);
        }
    }


    [TestClass]
    public class UC12_SubtractionDivisionTests
    {
        private const double EPSILON = 1e-6;

        // ================= SUBTRACTION =================

        [TestMethod]
        public void Subtraction_SameUnit()
        {
            var l1 = new Length(10.0, LengthUnit.Feet);
            var l2 = new Length(5.0, LengthUnit.Feet);

            var result = l1.Subtract(l2);

            Assert.AreEqual(new Length(5.0, LengthUnit.Feet), result);
        }

        [TestMethod]
        public void Subtraction_CrossUnit()
        {
            var l1 = new Length(10.0, LengthUnit.Feet);
            var l2 = new Length(6.0, LengthUnit.Inches);

            var result = l1.Subtract(l2);

            Assert.AreEqual(9.5, result.Value, EPSILON);
        }

        [TestMethod]
        public void Subtraction_ResultNegative()
        {
            var l1 = new Length(5.0, LengthUnit.Feet);
            var l2 = new Length(10.0, LengthUnit.Feet);

            var result = l1.Subtract(l2);

            Assert.AreEqual(-5.0, result.Value, EPSILON);
        }

        [TestMethod]
        public void Subtraction_ResultZero()
        {
            var l1 = new Length(10.0, LengthUnit.Feet);
            var l2 = new Length(120.0, LengthUnit.Inches);

            var result = l1.Subtract(l2);

            Assert.AreEqual(0.0, result.Value, EPSILON);
        }

        // ================= DIVISION =================

        [TestMethod]
        public void Division_SameUnit()
        {
            var l1 = new Length(10.0, LengthUnit.Feet);
            var l2 = new Length(2.0, LengthUnit.Feet);

            double result = l1.Divide(l2);

            Assert.AreEqual(5.0, result, EPSILON);
        }

        [TestMethod]
        public void Division_CrossUnit()
        {
            var l1 = new Length(24.0, LengthUnit.Inches);
            var l2 = new Length(2.0, LengthUnit.Feet);

            double result = l1.Divide(l2);

            Assert.AreEqual(1.0, result, EPSILON);
        }

        [TestMethod]
        public void Division_ResultLessThanOne()
        {
            var l1 = new Length(5.0, LengthUnit.Feet);
            var l2 = new Length(10.0, LengthUnit.Feet);

            double result = l1.Divide(l2);

            Assert.AreEqual(0.5, result, EPSILON);
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void Division_ByZero()
        {
            var l1 = new Length(10.0, LengthUnit.Feet);
            var l2 = new Length(0.0, LengthUnit.Feet);

            l1.Divide(l2);
        }
    }
}