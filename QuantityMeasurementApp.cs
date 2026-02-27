namespace QuantityMeasurement
{
    public class QuantityMeasurementApp
    {
        public static bool DemonstrateLengthEquality(Length l1, Length l2)
        {
            return l1.Equals(l2);
        }

        public static Length DemonstrateLengthConversion(double value,
            LengthUnit from,
            LengthUnit to)
        {
            double result = Length.Convert(value, from, to);
            return new Length(result, to);
        }

        public static Length DemonstrateLengthAddition(Length l1, Length l2)
        {
            return l1.Add(l2);
        }
    }
}