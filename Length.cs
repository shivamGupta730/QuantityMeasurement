using System;

namespace QuantityMeasurement
{
    
    public class Length
    {
        private readonly double value;
        private readonly LengthUnit unit;

        public Length(double value, LengthUnit unit)
        {
            if (value < 0)
                throw new ArgumentException("Length value cannot be negative.");

            this.value = value;
            this.unit = unit;
        }

        // Base Unit = Inches
        private double ConvertToBaseUnit()
        {
            return unit switch
            {
                LengthUnit.Feet => value * 12,
                LengthUnit.Inches => value,
                LengthUnit.Yards => value * 36,               // 1 Yard = 36 Inches
                LengthUnit.Centimeters => value * 0.393701,   // 1 cm = 0.393701 Inches
                _ => throw new InvalidOperationException("Unsupported unit")
            };
        }

        public bool Compare(Length? other)
        {
            if (other is null)
                return false;

            return Math.Abs(ConvertToBaseUnit() - other.ConvertToBaseUnit()) < 0.0001;
        }

        public override bool Equals(object? obj)
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
    }
}