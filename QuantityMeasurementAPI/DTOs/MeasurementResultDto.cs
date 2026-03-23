namespace QuantityMeasurementAPI.DTOs;

public class MeasurementResultDto
{
    public double Value { get; set; }
    
    public string Unit { get; set; } = string.Empty;
}
