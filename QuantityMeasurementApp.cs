using System;

namespace QuantityMeasurement
{
    public class QuantityMeasurementApp
    {
        public static bool DemonstrateLengthEquality(Length l1, Length l2)
        {
            return l1.Equals(l2);
        }

        public static bool DemonstrateLengthComparison(
            double value1, LengthUnit unit1,
            double value2, LengthUnit unit2)
        {
            var l1 = new Length(value1, unit1);
            var l2 = new Length(value2, unit2);

            return DemonstrateLengthEquality(l1, l2);
        }

        // 🔹 Overloaded Method 1
        public static Length DemonstrateLengthConversion(
            double value,
            LengthUnit fromUnit,
            LengthUnit toUnit)
        {
            var length = new Length(value, fromUnit);
            return length.ConvertTo(toUnit);
        }

       
        public static Length DemonstrateLengthConversion(
            Length length,
            LengthUnit toUnit)
        {
            return length.ConvertTo(toUnit);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("1 Foot to Inches: " +
                Length.Convert(1, LengthUnit.Feet, LengthUnit.Inches));

            Console.WriteLine("3 Yards to Feet: " +
                Length.Convert(3, LengthUnit.Yards, LengthUnit.Feet));

            Console.WriteLine("36 Inches to Yards: " +
                Length.Convert(36, LengthUnit.Inches, LengthUnit.Yards));

            Console.WriteLine("1 CM to Inches: " +
                Length.Convert(1, LengthUnit.Centimeters, LengthUnit.Inches));
        }
    }
}