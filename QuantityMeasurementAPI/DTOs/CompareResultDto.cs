namespace QuantityMeasurementAPI.DTOs;

public class CompareResultDto
{
    public bool IsEqual { get; set; }
    
    public string Message { get; set; } = string.Empty;
    
    public double Value1Base { get; set; }
    
    public double Value2Base { get; set; }
}

