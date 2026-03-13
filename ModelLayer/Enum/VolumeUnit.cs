using System;

namespace ModelLayer
{
    // Class-based volume unit (similar to LengthUnit)
public class VolumeUnit 
    {
        private readonly double factor;

        // Lambda expression to indicate that VolumeUnit supports arithmetic operations
        public static readonly SupportsArithmeticHandler supportsArithmetic = () => true;

        private VolumeUnit(double factor)
        {
            this.factor = factor;
        }

        // Base unit = Litre
        public static readonly VolumeUnit Litre = new VolumeUnit(1.0);
        public static readonly VolumeUnit Millilitre = new VolumeUnit(0.001);
        public static readonly VolumeUnit Gallon = new VolumeUnit(3.78541);

        public double GetConversionFactor()
        {
            return factor;
        }

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
            if (factor == 1.0) return "Litre";
            if (factor == 0.001) return "Millilitre";
            return "Gallon";
        }

        // Implementation of IUnit method for operation support
        public bool supportsArithmeticOperation()
        {
            return supportsArithmetic();
        }

        // Validate operation support - VolumeUnit supports all operations
        public void validateOperationSupport(string operation)
        {
            // VolumeUnit supports all operations, so no validation needed
        }

        public override string ToString()
        {
            if (factor == 1.0) return "Litre";
            if (factor == 0.001) return "Millilitre";
            return "Gallon";
        }
    }
}

