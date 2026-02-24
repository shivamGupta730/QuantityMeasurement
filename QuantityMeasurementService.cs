namespace QuantityMeasurement
{
    // Service layer for handling comparison logic
    public class QuantityMeasurementService
    {
        public bool CompareLengths(Length length1, Length length2)
        {
            return length1.Equals(length2);
        }
    }
}