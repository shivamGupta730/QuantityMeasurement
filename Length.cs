using System;

namespace QuantityMeasurement
{
    public class Length
    {
        private const double EPSILON = 1e-6;

        public double Value { get; }
        public LengthUnit Unit { get; }

        public Length(double value, LengthUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value");

            Value = value;
            Unit = unit;
        }

        private double ToBaseUnit()
        {
            return Unit.ConvertToBaseUnit(Value);
        }

        public static double Convert(double value, LengthUnit from, LengthUnit to)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value");

            double baseValue = from.ConvertToBaseUnit(value);
            return to.ConvertFromBaseUnit(baseValue);
        }

        public Length ConvertTo(LengthUnit targetUnit)
        {
            double baseValue = ToBaseUnit();
            double converted = targetUnit.ConvertFromBaseUnit(baseValue);
            return new Length(converted, targetUnit);   // ❌ NO ROUNDING
        }

        public Length Add(Length other)
        {
            return Add(other, this.Unit);
        }

        public Length Add(Length other, LengthUnit targetUnit)
        {
            if (other == null)
                throw new ArgumentException("Other length cannot be null");

            double sumBase = this.ToBaseUnit() + other.ToBaseUnit();
            double result = targetUnit.ConvertFromBaseUnit(sumBase);

            return new Length(result, targetUnit);   // ❌ NO ROUNDING
        }

        public override bool Equals(object obj)
        {
            if (obj is not Length other)
                return false;

            return Math.Abs(this.ToBaseUnit() - other.ToBaseUnit()) < EPSILON;
        }

        public override int GetHashCode()
        {
            return ToBaseUnit().GetHashCode();
        }

        public override string ToString()
        {
            return $"Quantity({Value}, {Unit})";
        }
    }
}