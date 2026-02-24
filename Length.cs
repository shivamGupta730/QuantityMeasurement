using System;

namespace QuantityMeasurement
{
    // Generic Length class (UC3 implementation)
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

        // Convert all units to base unit (Inches)
        private double ConvertToBaseUnit()
        {
            return unit switch
            {
                LengthUnit.Feet => value * 12,
                LengthUnit.Inches => value,
                _ => throw new InvalidOperationException("Unsupported unit")
            };
        }

        public bool Compare(Length other)
        {
            if (other == null)
                return false;

            return ConvertToBaseUnit() == other.ConvertToBaseUnit();
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
    }
}