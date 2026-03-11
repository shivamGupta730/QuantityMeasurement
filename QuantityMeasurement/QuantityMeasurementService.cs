using System;

namespace QuantityMeasurement
{
    // Service class responsible for demonstrating quantity operations
    public class QuantityMeasurementService
    {
        public static void DemonstrateEquality<U>(Quantity<U> q1, Quantity<U> q2)
        {
            Console.WriteLine($"Comparing {q1} and {q2}");
            Console.WriteLine($"Equal: {q1.Equals(q2)}");
            Console.WriteLine();
        }

        public static void DemonstrateConversion<U>(Quantity<U> quantity, U targetUnit)
        {
            var converted = quantity.ConvertTo(targetUnit);
            Console.WriteLine($"Converted: {converted}");
            Console.WriteLine();
        }

        public static void DemonstrateAddition<U>(Quantity<U> q1, Quantity<U> q2, U targetUnit)
        {
            var result = q1.Add(q2, targetUnit);
            Console.WriteLine($"Addition Result: {result}");
            Console.WriteLine();
        }
    }
}