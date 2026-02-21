using System;

namespace QuantityMeasurement
{
    public class QuantityMeasurementApp
    {
        // Feet class banayi hai jo feet value store karegi
        public class Feet
        {
            // yaha feet ki value store ho rahi hai
            // readonly isliye ki baad me change na ho
            private readonly double value;

            // constructor se value set kar rahe hai
            public Feet(double value)
            {
                this.value = value;
            }

            // Equals method override kiya hai taaki
            // do Feet objects ko compare kar sake
            public override bool Equals(object obj)
            {
                // agar dono same object hai toh true
                if (ReferenceEquals(this, obj))
                    return true;

                // agar null hai toh equal nahi ho sakta
                if (obj == null)
                    return false;

                // agar type alag hai toh compare nahi karenge
                if (obj.GetType() != typeof(Feet))
                    return false;

                // type cast kar rahe hai
                Feet other = (Feet)obj;

                // dono ki value compare kar rahe hai
                return this.value == other.value;
            }

            // equals override kiya hai toh gethashcode bhi karna padta hai
            public override int GetHashCode()
            {
                return value.GetHashCode();
            }
        }
    }
}