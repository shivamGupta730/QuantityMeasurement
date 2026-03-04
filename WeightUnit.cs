namespace QuantityMeasurement
{
    // Enum representing weight units
    public enum WeightUnit
    {
        Kilogram,
        Gram
    }

    public static class WeightUnitExtensions
    {
        // Conversion factor relative to base unit (GRAM)
        public static double GetConversionFactor(this WeightUnit unit)
        {
            switch (unit)
            {
                case WeightUnit.Kilogram: return 1000.0;
                case WeightUnit.Gram: return 1.0;
                default: return 1.0;
            }
        }

        // Convert given value to base unit (GRAM)
        public static double ConvertToBaseUnit(this WeightUnit unit, double value)
        {
            return value * unit.GetConversionFactor();
        }

        // Convert from base unit (GRAM) to target unit
        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
        {
            return baseValue / unit.GetConversionFactor();
        }

        public static string GetUnitName(this WeightUnit unit)
        {
            return unit.ToString();
        }
    }
}