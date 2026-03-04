using System;

namespace QuantityMeasurement
{
    public class QuantityMeasurementApp
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("==== Quantity Measurement UC10 ====\n");

            var length1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var length2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            QuantityMeasurementService.DemonstrateEquality(length1, length2);

            QuantityMeasurementService.DemonstrateConversion(length1, LengthUnit.Inches);

            QuantityMeasurementService.DemonstrateAddition(length1, length2, LengthUnit.Feet);

            var yard = new Quantity<LengthUnit>(1.0, LengthUnit.Yards);
            var feet = new Quantity<LengthUnit>(3.0, LengthUnit.Feet);

            QuantityMeasurementService.DemonstrateEquality(yard, feet);

            var cm = new Quantity<LengthUnit>(1.0, LengthUnit.Centimeters);
            var inch = new Quantity<LengthUnit>(0.393701, LengthUnit.Inches);

            QuantityMeasurementService.DemonstrateEquality(cm, inch);

            var weight1 = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            var weight2 = new Quantity<WeightUnit>(1000.0, WeightUnit.Gram);

            QuantityMeasurementService.DemonstrateEquality(weight1, weight2);
        }
    }
}