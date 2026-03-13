using System;

namespace ModelLayer.Entity
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

        // ================= ARITHMETIC OPERATION ENUM =================

        private enum ArithmeticOperation
        {
            ADD,
            SUBTRACT,
            DIVIDE
        }

        // ================= VALIDATION =================

        private void validateArithmeticOperands(Quantity<U> other, U targetUnit, bool targetRequired)
        {
            if (other == null)
                throw new ArgumentException("Other quantity cannot be null");

            if (targetRequired && targetUnit == null)
                throw new ArgumentException("Target unit cannot be null");

            if (double.IsNaN(Value) || double.IsInfinity(Value) ||
                double.IsNaN(other.Value) || double.IsInfinity(other.Value))
                throw new ArgumentException("Values must be finite numbers");
        }

        // ================= OPERATION SUPPORT VALIDATION =================

        private void validateOperationSupport(ArithmeticOperation operation)
        {
            // Try to call validateOperationSupport on the unit
            // This works for LengthUnit (class) and enums with extension methods
            dynamic unit = Unit;
            try
            {
                unit.validateOperationSupport(operation.ToString());
            }
            catch (Exception ex) when (ex.GetType().Name == "MissingMethodException" || 
                                       ex.GetType().Name == "RuntimeBinderException")
            {
                // If the method doesn't exist (older implementations), allow the operation
            }
        }

        // ================= CORE ARITHMETIC =================

        private double performBaseArithmetic(Quantity<U> other, ArithmeticOperation operation)
        {
            dynamic u1 = Unit;
            dynamic u2 = other.Unit;

            double base1 = u1.ConvertToBaseUnit(Value);
            double base2 = u2.ConvertToBaseUnit(other.Value);

            switch (operation)
            {
                case ArithmeticOperation.ADD:
                    return base1 + base2;

                case ArithmeticOperation.SUBTRACT:
                    return base1 - base2;

                case ArithmeticOperation.DIVIDE:

                    if (base2 == 0)
                        throw new ArithmeticException("Division by zero");

                    return base1 / base2;

                default:
                    throw new InvalidOperationException("Unknown operation");
            }
        }

        // ================= CONVERSION =================

        public Quantity<U> ConvertTo(U targetUnit)
        {
            dynamic current = Unit;
            dynamic target = targetUnit;

            double baseValue = current.ConvertToBaseUnit(Value);
            double converted = target.ConvertFromBaseUnit(baseValue);

            return new Quantity<U>(Math.Round(converted, 2), targetUnit);
        }

        // ================= ADDITION =================

        public Quantity<U> Add(Quantity<U> other)
        {
            return Add(other, Unit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            validateArithmeticOperands(other, targetUnit, true);
            
            // Validate that this unit type supports arithmetic operations
            validateOperationSupport(ArithmeticOperation.ADD);

            double baseResult = performBaseArithmetic(other, ArithmeticOperation.ADD);

            dynamic target = targetUnit;
            double result = target.ConvertFromBaseUnit(baseResult);

            return new Quantity<U>(Math.Round(result, 2), targetUnit);
        }

        // ================= SUBTRACTION =================

        public Quantity<U> Subtract(Quantity<U> other)
        {
            return Subtract(other, Unit);
        }

        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            validateArithmeticOperands(other, targetUnit, true);
            
            // Validate that this unit type supports arithmetic operations
            validateOperationSupport(ArithmeticOperation.SUBTRACT);

            double baseResult = performBaseArithmetic(other, ArithmeticOperation.SUBTRACT);

            dynamic target = targetUnit;
            double result = target.ConvertFromBaseUnit(baseResult);

            return new Quantity<U>(Math.Round(result, 2), targetUnit);
        }

        // ================= DIVISION =================

        public double Divide(Quantity<U> other)
        {
            validateArithmeticOperands(other, default(U), false);
            
            // Validate that this unit type supports arithmetic operations
            validateOperationSupport(ArithmeticOperation.DIVIDE);

            return performBaseArithmetic(other, ArithmeticOperation.DIVIDE);
        }

        // ================= EQUALITY =================

public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Quantity<U>))
                return false;

            Quantity<U> other = (Quantity<U>)obj;

            // Check if units are of the same category (same type)
            if (Unit.GetType() != other.Unit.GetType())
                return false;

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

