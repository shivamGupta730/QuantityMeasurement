using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementAPI.DTOs;

public class ConversionRequestDto
{
    [Required]
    public double InputValue { get; set; }
    
    [Required]
    public string FromUnit { get; set; } = string.Empty;
    
    [Required]
    public string ToUnit { get; set; } = string.Empty;
    
    public string UnitType { get; set; } = string.Empty;
}
