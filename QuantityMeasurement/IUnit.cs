using System;

namespace QuantityMeasurement
{
    // Functional interface (delegate) to indicate whether a measurable unit supports arithmetic operations
    public delegate bool SupportsArithmeticHandler();

    // Interface to define behavior for all measurement units
    public interface IUnit
    {
        double ConvertToBaseUnit(double value);
        double ConvertFromBaseUnit(double baseValue);
        string GetUnitName();

        // Method to check if arithmetic is supported
        bool supportsArithmeticOperation();

        // Method to validate arithmetic operation support
        // Subclasses can override to throw UnsupportedOperationException for unsupported operations
        void validateOperationSupport(string operation);
    }

    // Abstract base class providing default implementations for IUnit
    public abstract class UnitBase : IUnit
    {
        public abstract double ConvertToBaseUnit(double value);
        public abstract double ConvertFromBaseUnit(double baseValue);
        public abstract string GetUnitName();

        // Default implementation - all units support arithmetic by default
        public virtual bool supportsArithmeticOperation()
        {
            return true;
        }

        // Default implementation - does nothing (all operations allowed)
        public virtual void validateOperationSupport(string operation)
        {
            // Default implementation does nothing - all units support all operations by default
        }
    }
}

