using System;

namespace ModelLayer.Entity
{
    public record MeasurementRecord(
        int Id,
        double InputValue,
        string FromUnit,
        string ToUnit,
        double ResultValue,
        DateTime ConversionDateTime
    );
}
