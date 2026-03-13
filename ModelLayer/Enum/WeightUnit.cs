using System;

namespace ModelLayer
{
    // Class-based weight unit (similar to LengthUnit)
public class WeightUnit 
    {
        private readonly double factor;

        // Lambda expression to indicate that WeightUnit supports arithmetic operations
        public static readonly SupportsArithmeticHandler supportsArithmetic = () => true;

        private WeightUnit(double factor)
        {
            this.factor = factor;
        }

        // Base unit = Gram
        public static readonly WeightUnit Kilogram = new WeightUnit(1000.0);
        public static readonly WeightUnit Gram = new WeightUnit(1.0);

        public double ConvertToBaseUnit(double value)
        {
            return value * factor;
        }

        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / factor;
        }

        public string GetUnitName()
        {
            if (factor == 1000.0) return "Kilogram";
            return "Gram";
        }

        // Implementation of IUnit method for operation support
        public bool supportsArithmeticOperation()
        {
            return supportsArithmetic();
        }

        // Validate operation support - WeightUnit supports all operations
        public void validateOperationSupport(string operation)
        {
            // WeightUnit supports all operations, so no validation needed
        }

        public override string ToString()
        {
            if (factor == 1000.0) return "Kilogram";
            return "Gram";
        }
    }
}

