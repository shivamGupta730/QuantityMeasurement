namespace QuantityMeasurement
{
    public interface IUnit
    {
        double ConvertToBaseUnit(double value);
        double ConvertFromBaseUnit(double baseValue);
    }
}