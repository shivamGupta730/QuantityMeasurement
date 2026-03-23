using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementAPI.DTOs;

public class AddMeasurementRequestDto
{
    [Required]
    public double Value1 { get; set; }
    
    [Required]
    public string Unit1 { get; set; } = string.Empty;
    
    [Required]
    public double Value2 { get; set; }
    
    [Required]
    public string Unit2 { get; set; } = string.Empty;
    
    [Required]
    public string TargetUnit { get; set; } = string.Empty;
}
