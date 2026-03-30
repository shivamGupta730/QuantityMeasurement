using System.Text.Json.Serialization;

namespace QuantityMeasurementAPI.DTOs;

public class ErrorResponseDto
{
    public string Timestamp { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Details { get; set; }
    public string Path { get; set; } = string.Empty;
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TraceId { get; set; }
}
