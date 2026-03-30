using ModelLayer.Entity;
using ModelLayer.Enum;

namespace QuantityMeasurementAPI.DTOs
{
    public class HistoryResponseDto
    {
        public List<MeasurementRecord> History { get; set; } = new();
        public int TotalCount { get; set; }
    }
}
