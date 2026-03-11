namespace QuantityMeasurement
{
    // Wrapper class to maintain backward compatibility with old tests
    public class Length : Quantity<LengthUnit>
    {
        public Length(double value, LengthUnit unit) : base(value, unit)
        {
        }
    }
}