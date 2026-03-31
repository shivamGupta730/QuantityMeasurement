using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurementAPI.DTOs
{
    public class GoogleLoginDto
    {
        [Required]
        public string IdToken { get; set; } = string.Empty;
    }
}
