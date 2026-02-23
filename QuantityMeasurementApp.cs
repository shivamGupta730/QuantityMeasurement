using System;

namespace QuantityMeasurement
{
    // Main class for Quantity Measurement Application
    public class QuantityMeasurementApp
    {
        // -------------------- FEET CLASS --------------------
        // This class represents measurement in Feet
        public class Feet
        {
            // storing the value of feet
            private readonly double value;

            // constructor to initialize feet value
            public Feet(double value)
            {
                this.value = value;
            }

            // overriding Equals method to compare two Feet objects
            public override bool Equals(object obj)
            {
                // checking if both references are same
                if (ReferenceEquals(this, obj))
                    return true;

                // checking if object is null
                if (obj == null)
                    return false;

                // checking if object is of same type
                if (obj.GetType() != typeof(Feet))
                    return false;

                // type casting
                Feet other = (Feet)obj;

                // comparing values
                return this.value == other.value;
            }

            // overriding GetHashCode method
            public override int GetHashCode()
            {
                return value.GetHashCode();
            }
        }

        // -------------------- INCHES CLASS --------------------
        // This class represents measurement in Inches
        public class Inches
        {
            // storing the value of inches
            private readonly double value;

            // constructor to initialize inches value
            public Inches(double value)
            {
                this.value = value;
            }

            // overriding Equals method to compare two Inches objects
            public override bool Equals(object obj)
            {
                // checking if both references are same
                if (ReferenceEquals(this, obj))
                    return true;

                // checking if object is null
                if (obj == null)
                    return false;

                // checking if object is of same type
                if (obj.GetType() != typeof(Inches))
                    return false;

                // type casting
                Inches other = (Inches)obj;

                // comparing values
                return this.value == other.value;
            }

            // overriding GetHashCode method
            public override int GetHashCode()
            {
                return value.GetHashCode();
            }
        }
    }
}