namespace QuantityMeasurement
{
    public class QuantityMeasurementService
    {
        public bool CompareLengths(Length l1, Length l2)
        {
            if (l1 == null || l2 == null)
                return false;

            return l1.Equals(l2);
        }
    }
}