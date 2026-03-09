using System;

namespace QuantityMeasurement
{
    public class QuantityMeasurementApp
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("==== Quantity Measurement UC14 - Temperature Support ====\n");

            // ========== EXISTING FUNCTIONALITY (UC1-UC13) ==========
            Console.WriteLine("--- Existing Length, Weight, Volume Operations ---");
            
            var length1 = new Quantity<LengthUnit>(1.0, LengthUnit.Feet);
            var length2 = new Quantity<LengthUnit>(12.0, LengthUnit.Inches);

            QuantityMeasurementService.DemonstrateEquality(length1, length2);
            QuantityMeasurementService.DemonstrateConversion(length1, LengthUnit.Inches);
            QuantityMeasurementService.DemonstrateAddition(length1, length2, LengthUnit.Feet);

            var weight1 = new Quantity<WeightUnit>(1.0, WeightUnit.Kilogram);
            var weight2 = new Quantity<WeightUnit>(1000.0, WeightUnit.Gram);
            QuantityMeasurementService.DemonstrateEquality(weight1, weight2);

            // ========== NEW TEMPERATURE FUNCTIONALITY (UC14) ==========
            Console.WriteLine("\n--- Temperature Equality Comparisons ---");

            // Test Celsius to Celsius equality
            var celsius1 = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.CELSIUS);
            var celsius2 = new Quantity<TemperatureUnit>(0.0, TemperatureUnit.CELSIUS);
            Console.WriteLine($"0°C equals 0°C: {celsius1.Equals(celsius2)}"); // Expected: true

            // Test Fahrenheit to Fahrenheit equality
            var fahrenheit1 = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.FAHRENHEIT);
            var fahrenheit2 = new Quantity<TemperatureUnit>(32.0, TemperatureUnit.FAHRENHEIT);
            Console.WriteLine($"32°F equals 32°F: {fahrenheit1.Equals(fahrenheit2)}"); // Expected: true

            // Test Celsius to Fahrenheit equality (0°C = 32°F)
            Console.WriteLine($"0°C equals 32°F: {celsius1.Equals(fahrenheit1)}"); // Expected: true

            // Test 100°C = 212°F
            var celsius100 = new Quantity<TemperatureUnit>(100.0, TemperatureUnit.CELSIUS);
            var fahrenheit212 = new Quantity<TemperatureUnit>(212.0, TemperatureUnit.FAHRENHEIT);
            Console.WriteLine($"100°C equals 212°F: {celsius100.Equals(fahrenheit212)}"); // Expected: true

            // Test Kelvin to Celsius (273.15K = 0°C)
            var kelvin273 = new Quantity<TemperatureUnit>(273.15, TemperatureUnit.KELVIN);
            Console.WriteLine($"273.15K equals 0°C: {kelvin273.Equals(celsius1)}"); // Expected: true

            // Test -40°C = -40°F (equal point)
            var celsiusNeg40 = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.CELSIUS);
            var fahrenheitNeg40 = new Quantity<TemperatureUnit>(-40.0, TemperatureUnit.FAHRENHEIT);
            Console.WriteLine($"-40°C equals -40°F: {celsiusNeg40.Equals(fahrenheitNeg40)}"); // Expected: true

            // Test 50°C = 122°F
            var celsius50 = new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS);
            var fahrenheit122 = new Quantity<TemperatureUnit>(122.0, TemperatureUnit.FAHRENHEIT);
            Console.WriteLine($"50°C equals 122°F: {celsius50.Equals(fahrenheit122)}"); // Expected: true (within epsilon)

            Console.WriteLine("\n--- Temperature Conversions ---");

            // Celsius to Fahrenheit
            var converted1 = celsius100.ConvertTo(TemperatureUnit.FAHRENHEIT);
            Console.WriteLine($"100°C converted to Fahrenheit: {converted1.Value}°F"); // Expected: 212.0°F

            // Fahrenheit to Celsius
            var converted2 = fahrenheit212.ConvertTo(TemperatureUnit.CELSIUS);
            Console.WriteLine($"212°F converted to Celsius: {converted2.Value}°C"); // Expected: 100.0°C

            // Kelvin to Celsius
            var converted3 = kelvin273.ConvertTo(TemperatureUnit.CELSIUS);
            Console.WriteLine($"273.15K converted to Celsius: {converted3.Value}°C"); // Expected: 0.0°C

            // Celsius to Kelvin
            var converted4 = celsius1.ConvertTo(TemperatureUnit.KELVIN);
            Console.WriteLine($"0°C converted to Kelvin: {converted4.Value}K"); // Expected: 273.15K

            // Negative temperature conversion
            var converted5 = celsiusNeg40.ConvertTo(TemperatureUnit.FAHRENHEIT);
            Console.WriteLine($"-40°C converted to Fahrenheit: {converted5.Value}°F"); // Expected: -40.0°F

            Console.WriteLine("\n--- Temperature Unsupported Operations (Error Handling) ---");

            // Test unsupported addition
            try
            {
                var addResult = celsius100.Add(new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS));
                Console.WriteLine("Addition succeeded (unexpected): " + addResult.Value);
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Addition threw NotSupportedException: {ex.Message.Substring(0, Math.Min(60, ex.Message.Length))}...");
            }

            // Test unsupported subtraction
            try
            {
                var subResult = celsius100.Subtract(new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS));
                Console.WriteLine("Subtraction succeeded (unexpected): " + subResult.Value);
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Subtraction threw NotSupportedException: {ex.Message.Substring(0, Math.Min(60, ex.Message.Length))}...");
            }

            // Test unsupported division
            try
            {
                var divResult = celsius100.Divide(new Quantity<TemperatureUnit>(50.0, TemperatureUnit.CELSIUS));
                Console.WriteLine("Division succeeded (unexpected): " + divResult);
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Division threw NotSupportedException: {ex.Message.Substring(0, Math.Min(60, ex.Message.Length))}...");
            }

            Console.WriteLine("\n--- Cross-Category Prevention ---");

            // Temperature vs Length
            Console.WriteLine($"100°C equals 100 FEET: {celsius100.Equals(new Quantity<LengthUnit>(100.0, LengthUnit.Feet))}"); // Expected: false

            // Temperature vs Weight
            Console.WriteLine($"50°C equals 50 KG: {celsius50.Equals(new Quantity<WeightUnit>(50.0, WeightUnit.Kilogram))}"); // Expected: false

            Console.WriteLine("\n--- Operation Support Methods ---");

            // Test operation support
            Console.WriteLine($"TemperatureUnit.CELSIUS supports arithmetic: {TemperatureUnit.supportsArithmetic()}"); // Expected: false
            Console.WriteLine($"LengthUnit.Feet supports arithmetic: {LengthUnit.supportsArithmetic()}"); // Expected: true

            Console.WriteLine("\n==== UC14 Implementation Complete ====");
        }
    }
}

