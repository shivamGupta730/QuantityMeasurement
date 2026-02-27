using System;

namespace QuantityMeasurement
{
  

    public sealed class Length
    {
        private const double EPSILON = 1e-6;
        private readonly double value;
        private readonly LengthUnit unit;

        public Length(double value, LengthUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value.");

            this.value = value;
            this.unit = unit;
        }

        public double Value => value;
        public LengthUnit Unit => unit;

        // ================= BASE CONVERSION =================

        private double ToBaseUnitFeet()
        {
            return unit switch
            {
                LengthUnit.Feet => value,
                LengthUnit.Inches => value / 12.0,
                LengthUnit.Yards => value * 3.0,
                LengthUnit.Centimeters => value / 30.48,
                _ => throw new ArgumentException("Unsupported unit")
            };
        }

        private static double FromBaseFeet(double feetValue, LengthUnit targetUnit)
        {
            return targetUnit switch
            {
                LengthUnit.Feet => feetValue,
                LengthUnit.Inches => feetValue * 12.0,
                LengthUnit.Yards => feetValue / 3.0,
                LengthUnit.Centimeters => feetValue * 30.48,
                _ => throw new ArgumentException("Unsupported target unit")
            };
        }

        // ================= UC5 STATIC CONVERT =================

        public static double Convert(double value, LengthUnit from, LengthUnit to)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value.");

            double baseFeet = from switch
            {
                LengthUnit.Feet => value,
                LengthUnit.Inches => value / 12.0,
                LengthUnit.Yards => value * 3.0,
                LengthUnit.Centimeters => value / 30.48,
                _ => throw new ArgumentException("Unsupported unit")
            };

            return FromBaseFeet(baseFeet, to);
        }

        public Length ConvertTo(LengthUnit targetUnit)
        {
            double baseFeet = ToBaseUnitFeet();
            double converted = FromBaseFeet(baseFeet, targetUnit);
            return new Length(converted, targetUnit);
        }

        // ================= UC6 ADD =================

        public Length Add(Length other)
        {
            if (other == null)
                throw new ArgumentException("Length cannot be null");

            double sumFeet = this.ToBaseUnitFeet() + other.ToBaseUnitFeet();
            double resultValue = FromBaseFeet(sumFeet, this.unit);

            return new Length(resultValue, this.unit);
        }

        // ================= UC7 ADD WITH TARGET =================

        public Length Add(Length other, LengthUnit targetUnit)
        {
            if (other == null)
                throw new ArgumentException("Length cannot be null");

            double sumFeet = this.ToBaseUnitFeet() + other.ToBaseUnitFeet();
            double resultValue = FromBaseFeet(sumFeet, targetUnit);

            return new Length(resultValue, targetUnit);
        }

        // ================= EQUALITY =================

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not Length other)
                return false;

            return Math.Abs(this.ToBaseUnitFeet() - other.ToBaseUnitFeet()) < EPSILON;
        }

        public override int GetHashCode()
        {
            return ToBaseUnitFeet().GetHashCode();
        }

        public override string ToString()
        {
            return $"{value} {unit}";
        }
    }
}