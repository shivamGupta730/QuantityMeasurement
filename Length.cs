using System;

namespace QuantityMeasurement
{
    public class Length
    {
        private readonly double value;
        private readonly LengthUnit unit;
        private const double EPSILON = 1e-6;

        public Length(double value, LengthUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid value");

            this.value = value;
            this.unit = unit;
        }

        public double Value => value;
        public LengthUnit Unit => unit;

        private static double ToFeet(double value, LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => value,
                LengthUnit.Inches => value / 12.0,
                LengthUnit.Yards => value * 3.0,
                LengthUnit.Centimeters => value * 0.0328084,
                _ => throw new ArgumentException("Invalid unit")
            };
        }

        private static double FromFeet(double feet, LengthUnit target)
        {
            return target switch
            {
                LengthUnit.Feet => feet,
                LengthUnit.Inches => feet * 12.0,
                LengthUnit.Yards => feet / 3.0,
                LengthUnit.Centimeters => feet / 0.0328084,
                _ => throw new ArgumentException("Invalid unit")
            };
        }

        public Length ConvertTo(LengthUnit targetUnit)
        {
            double feet = ToFeet(value, unit);
            double converted = FromFeet(feet, targetUnit);
            return new Length(converted, targetUnit);
        }

        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid value");

            double feet = ToFeet(value, source);
            return FromFeet(feet, target);
        }

        public Length Add(Length other)
        {
            if (other == null)
                throw new ArgumentException("Length cannot be null");

            double sumFeet = ToFeet(this.value, this.unit)
                           + ToFeet(other.value, other.unit);

            double result = FromFeet(sumFeet, this.unit);

            return new Length(result, this.unit);
        }

        public bool Compare(Length? other)
        {
            if (other is null)
                return false;

            double thisFeet = ToFeet(this.value, this.unit);
            double otherFeet = ToFeet(other.value, other.unit);

            return Math.Abs(thisFeet - otherFeet) < EPSILON;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Length other)
                return false;

            return Compare(other);
        }

        public override int GetHashCode()
        {
            double feet = ToFeet(value, unit);
            return feet.GetHashCode();
        }

        public override string ToString()
        {
            return $"{value} {unit}";
        }
    }
}