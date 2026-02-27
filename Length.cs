using System;

namespace QuantityMeasurement
{
    public class Length
    {
        private readonly double value;
        private readonly LengthUnit unit;

        private const double Tolerance = 0.000001;

        public Length(double value, LengthUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            this.value = value;
            this.unit = unit;
        }

        // 🔹 Convert to base unit (Inches)
        private double ConvertToBaseUnit()
        {
            return unit switch
            {
                LengthUnit.Feet => value * 12,
                LengthUnit.Inches => value,
                LengthUnit.Yards => value * 36,
                LengthUnit.Centimeters => value * 0.393701,
                _ => throw new InvalidOperationException("Unsupported unit")
            };
        }

        public Length ConvertTo(LengthUnit targetUnit)
        {
            if (targetUnit == unit)
                return new Length(value, unit);

            double inches = ConvertToBaseUnit();

            double convertedValue = targetUnit switch
            {
                LengthUnit.Feet => inches / 12,
                LengthUnit.Inches => inches,
                LengthUnit.Yards => inches / 36,
                LengthUnit.Centimeters => inches / 0.393701,
                _ => throw new InvalidOperationException("Unsupported unit")
            };

            return new Length(convertedValue, targetUnit);
        }

        
        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            if (source == target)
                return value;

            var length = new Length(value, source);
            var converted = length.ConvertTo(target);

            return converted.value;
        }

        public bool Compare(Length other)
        {
            if (other is null)
                return false;

            return Math.Abs(ConvertToBaseUnit() - other.ConvertToBaseUnit()) < Tolerance;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not Length other)
                return false;

            return Compare(other);
        }

        public override int GetHashCode()
        {
            return ConvertToBaseUnit().GetHashCode();
        }

        public override string ToString()
        {
            return $"{value} {unit}";
        }
    }
}