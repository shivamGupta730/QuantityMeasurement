using System;

namespace QuantityMeasurement
{
    // Generic Quantity class
    public class Quantity<U>
    {
        // Properties used by tests
        public double Value { get; }
        public U Unit { get; }

        public Quantity(double value, U unit)
        {
            if (unit == null)
                throw new ArgumentException("Unit cannot be null");

            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value");

            Value = value;
            Unit = unit;
        }

        // Convert to another unit
        public Quantity<U> ConvertTo(U targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentException("Target unit cannot be null");

            dynamic currentUnit = Unit;
            dynamic target = targetUnit;

            double baseValue = currentUnit.ConvertToBaseUnit(Value);
            double converted = target.ConvertFromBaseUnit(baseValue);

            return new Quantity<U>(Math.Round(converted, 6), targetUnit);
        }

        // Addition returning result in same unit
        public Quantity<U> Add(Quantity<U> other)
        {
            if (other == null)
                throw new ArgumentException("Quantity cannot be null");

            return Add(other, Unit);
        }

        // Addition with target unit
        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            if (other == null)
                throw new ArgumentException("Quantity cannot be null");

            dynamic u1 = Unit;
            dynamic u2 = other.Unit;
            dynamic target = targetUnit;

            double base1 = u1.ConvertToBaseUnit(Value);
            double base2 = u2.ConvertToBaseUnit(other.Value);

            double totalBase = base1 + base2;

            double result = target.ConvertFromBaseUnit(totalBase);

            return new Quantity<U>(Math.Round(result, 6), targetUnit);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Quantity<U>))
                return false;

            Quantity<U> other = (Quantity<U>)obj;

            dynamic u1 = Unit;
            dynamic u2 = other.Unit;

            double base1 = u1.ConvertToBaseUnit(Value);
            double base2 = u2.ConvertToBaseUnit(other.Value);

            return Math.Abs(base1 - base2) < 0.000001;
        }

        public override int GetHashCode()
        {
            dynamic unit = Unit;
            double baseValue = unit.ConvertToBaseUnit(Value);
            return baseValue.GetHashCode();
        }

        public override string ToString()
        {
            return $"Quantity({Value}, {Unit})";
        }
    }
}