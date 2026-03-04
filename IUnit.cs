namespace QuantityMeasurement
{
    // Interface to define behavior for all measurement units
    public interface IUnit
    {
        double ConvertToBaseUnit(double value);
        double ConvertFromBaseUnit(double baseValue);
        string GetUnitName();
    }
}