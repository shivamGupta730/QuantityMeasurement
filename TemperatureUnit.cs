using System;

namespace QuantityMeasurement
{
    // Class-based temperature unit
    public class TemperatureUnit : IUnit
    {
        // Lambda expression to indicate that TemperatureUnit does NOT support arithmetic operations
        public static readonly SupportsArithmeticHandler supportsArithmetic = () => false;

        // Functional Interface and Lambda Expression for Celsius to Celsius conversion (identity function)
        public static readonly Func<double, double> CELSIUS_TO_CELSIUS = (celsius) => celsius;

        private readonly string name;

        private TemperatureUnit(string name)
        {
            this.name = name;
        }

        // Temperature units - Celsius is the base unit
        public static readonly TemperatureUnit CELSIUS = new TemperatureUnit("Celsius");
        public static readonly TemperatureUnit FAHRENHEIT = new TemperatureUnit("Fahrenheit");
        public static readonly TemperatureUnit KELVIN = new TemperatureUnit("Kelvin");

        // Get conversion factor (for compatibility, but temperature uses formulas, not factors)
        public double GetConversionFactor()
        {
            // Temperature conversions use formulas, not simple multiplication factors
            // Return 1.0 for base unit compatibility
            return 1.0;
        }

        // Convert temperature to base unit (Celsius)
        public double ConvertToBaseUnit(double value)
        {
            if (this == CELSIUS)
                return value;
            if (this == FAHRENHEIT)
                return (value - 32) * 5.0 / 9.0;
            if (this == KELVIN)
                return value - 273.15;
            
            throw new ArgumentException("Invalid Temperature Unit");
        }

        // Convert from base unit (Celsius) to target unit
        public double ConvertFromBaseUnit(double baseValue)
        {
            if (this == CELSIUS)
                return baseValue;
            if (this == FAHRENHEIT)
                return baseValue * 9.0 / 5.0 + 32;
            if (this == KELVIN)
                return baseValue + 273.15;
            
            throw new ArgumentException("Invalid Temperature Unit");
        }

        public string GetUnitName()
        {
            return name;
        }

        // Check if arithmetic is supported - returns false for temperature
        public bool supportsArithmeticOperation()
        {
            return supportsArithmetic();
        }

        // Validate operation support - throws exception for unsupported operations
        public void validateOperationSupport(string operation)
        {
            if (!supportsArithmetic())
            {
                throw new NotSupportedException(
                    $"Temperature does not support {operation} operation. " +
                    "Temperature is an absolute scale; arithmetic operations on absolute temperatures are not meaningful. " +
                    "Consider using temperature differences instead.");
            }
        }

        public override string ToString()
        {
            return name;
        }
    }
}

