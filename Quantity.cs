using System;

namespace QuantityMeasurement
{
    public abstract class Quantity<TUnit> where TUnit : Enum
    {
        protected const double EPSILON = 1e-6;

        public double Value { get; }
        public TUnit Unit { get; }

        protected Quantity(double value, TUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value");

            Value = value;
            Unit = unit;
        }

        protected abstract double ToBaseUnit();
        protected abstract double FromBaseUnit(double baseValue);

        public bool IsEqual(Quantity<TUnit> other)
        {
            if (other == null)
                return false;

            return Math.Abs(this.ToBaseUnit() - other.ToBaseUnit()) < EPSILON;
        }

        protected double AddBase(Quantity<TUnit> other)
        {
            return this.ToBaseUnit() + other.ToBaseUnit();
        }
    }
}