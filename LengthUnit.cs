namespace QuantityMeasurement
{
    public enum LengthUnit
    {
        Feet,
        Inches,
        Yards,
        Centimeters
    }

    public static class LengthUnitExtensions
    {
        private const double FeetFactor = 1.0;          // base
        private const double InchesFactor = 1.0 / 12.0; // 1 inch = 1/12 feet
        private const double YardsFactor = 3.0;         // 1 yard = 3 feet
        private const double CmFactor = 1.0 / 30.48;    // 1 cm = 1/30.48 feet

        public static double ConvertToBaseUnit(this LengthUnit unit, double value)
        {
            return unit switch
            {
                LengthUnit.Feet => value * FeetFactor,
                LengthUnit.Inches => value * InchesFactor,
                LengthUnit.Yards => value * YardsFactor,
                LengthUnit.Centimeters => value * CmFactor,
                _ => throw new ArgumentException("Invalid Length Unit")
            };
        }

        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
        {
            return unit switch
            {
                LengthUnit.Feet => baseValue / FeetFactor,
                LengthUnit.Inches => baseValue / InchesFactor,
                LengthUnit.Yards => baseValue / YardsFactor,
                LengthUnit.Centimeters => baseValue / CmFactor,
                _ => throw new ArgumentException("Invalid Length Unit")
            };
        }
    }
}