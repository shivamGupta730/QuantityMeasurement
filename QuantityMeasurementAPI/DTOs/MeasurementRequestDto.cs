using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementAPI.DTOs;

public class MeasurementRequestDto
{
    [Required]
    public double Value { get; set; }
    
    [Required]
    public string SourceUnit { get; set; } = string.Empty;
    
    [Required]
    public string TargetUnit { get; set; } = string.Empty;
}
