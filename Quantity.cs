using System;

namespace QuantityMeasurement
{
    public class Quantity<U>
    {
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

        // ================= CONVERSION =================

        public Quantity<U> ConvertTo(U targetUnit)
        {
            dynamic currentUnit = Unit;
            dynamic target = targetUnit;

            double baseValue = currentUnit.ConvertToBaseUnit(Value);
            double converted = target.ConvertFromBaseUnit(baseValue);

            return new Quantity<U>(converted, targetUnit);
        }

        // ================= ADDITION =================

        public Quantity<U> Add(Quantity<U> other)
        {
            return Add(other, Unit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            if (other == null)
                throw new ArgumentException("Other quantity cannot be null");

            dynamic u1 = Unit;
            dynamic u2 = other.Unit;
            dynamic target = targetUnit;

            double base1 = u1.ConvertToBaseUnit(Value);
            double base2 = u2.ConvertToBaseUnit(other.Value);

            double resultBase = base1 + base2;

            double result = target.ConvertFromBaseUnit(resultBase);

            return new Quantity<U>(result, targetUnit);
        }

        // ================= SUBTRACTION =================

        public Quantity<U> Subtract(Quantity<U> other)
        {
            return Subtract(other, Unit);
        }

        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            if (other == null)
                throw new ArgumentException("Other quantity cannot be null");

            dynamic u1 = Unit;
            dynamic u2 = other.Unit;
            dynamic target = targetUnit;

            double base1 = u1.ConvertToBaseUnit(Value);
            double base2 = u2.ConvertToBaseUnit(other.Value);

            double resultBase = base1 - base2;

            double result = target.ConvertFromBaseUnit(resultBase);

            return new Quantity<U>(result, targetUnit);
        }

        // ================= DIVISION =================

        public double Divide(Quantity<U> other)
        {
            if (other == null)
                throw new ArgumentException("Other quantity cannot be null");

            dynamic u1 = Unit;
            dynamic u2 = other.Unit;

            double base1 = u1.ConvertToBaseUnit(Value);
            double base2 = u2.ConvertToBaseUnit(other.Value);

            if (base2 == 0)
                throw new ArithmeticException("Division by zero");

            return base1 / base2;
        }

        // ================= EQUALITY =================

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Quantity<U>))
                return false;

            Quantity<U> other = (Quantity<U>)obj;

            dynamic u1 = Unit;
            dynamic u2 = other.Unit;

            double base1 = u1.ConvertToBaseUnit(Value);
            double base2 = u2.ConvertToBaseUnit(other.Value);

            return Math.Abs(base1 - base2) < 1e-6;
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