using System;

namespace QuantityMeasurement
{
    public class LengthUnit
    {
        private readonly double factor;

        private LengthUnit(double factor)
        {
            this.factor = factor;
        }

        // Base unit = Feet
        public static readonly LengthUnit Feet = new LengthUnit(1);
        public static readonly LengthUnit Inches = new LengthUnit(1.0 / 12);
        public static readonly LengthUnit Yards = new LengthUnit(3);
        public static readonly LengthUnit Centimeters = new LengthUnit(0.0328084);

        public double ConvertToBaseUnit(double value)
        {
            return value * factor;
        }

        public double ConvertFromBaseUnit(double baseValue)
        {
            return baseValue / factor;
        }

        public override string ToString()
        {
            if (factor == 1) return "Feet";
            if (factor == 1.0 / 12) return "Inches";
            if (factor == 3) return "Yards";
            return "Centimeters";
        }
    }
}