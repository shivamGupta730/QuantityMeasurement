using System;

namespace QuantityMeasurement
{
    public class Length : Quantity<LengthUnit>
    {
        public Length(double value, LengthUnit unit) : base(value, unit) { }

        protected override double ToBaseUnit()
        {
            return Unit.ConvertToBaseUnit(Value);
        }

        protected override double FromBaseUnit(double baseValue)
        {
            return Unit.ConvertFromBaseUnit(baseValue);
        }

        public Length ConvertTo(LengthUnit targetUnit)
        {
            double baseValue = ToBaseUnit();
            double converted = targetUnit.ConvertFromBaseUnit(baseValue);
            return new Length(converted, targetUnit);
        }

        public static double Convert(double value, LengthUnit from, LengthUnit to)
        {
            double baseValue = from.ConvertToBaseUnit(value);
            return to.ConvertFromBaseUnit(baseValue);
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

            return new Length(result, targetUnit);
        }

        public override bool Equals(object obj)
        {
            return obj is Length other && IsEqual(other);
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