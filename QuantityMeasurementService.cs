namespace QuantityMeasurement
{
    public class QuantityMeasurementService
    {
        public bool CompareLengths(Length l1, Length l2)
        {
            return l1.Compare(l2);
        }
    }
}