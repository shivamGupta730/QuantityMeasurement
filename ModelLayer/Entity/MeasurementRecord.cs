using System;
using ModelLayer.Enum;

namespace ModelLayer.Entity
{
    public class MeasurementRecord
    {
        // ✅ EF Core ke liye empty constructor
        public MeasurementRecord()
        {
        }

        public int Id { get; set; }
        public double InputValue { get; set; }
        public string FromUnit { get; set; } = string.Empty;
        public string ToUnit { get; set; } = string.Empty;
        public double ResultValue { get; set; }
        public DateTime ConversionDateTime { get; set; }
        public MeasurementType MeasurementType { get; set; }
        public OperationType OperationType { get; set; }
        public bool IsError { get; set; }
        public string? ErrorMessage { get; set; }

        // Constructor for backward compatibility
        public MeasurementRecord(
            int id,
            double inputValue,
            string fromUnit,
            string toUnit,
            double resultValue,
            DateTime conversionDateTime)
        {
            Id = id;
            InputValue = inputValue;
            FromUnit = fromUnit;
            ToUnit = toUnit;
            ResultValue = resultValue;
            ConversionDateTime = conversionDateTime;
            MeasurementType = MeasurementType.Length;
            OperationType = OperationType.Convert;
            IsError = false;
        }

        // Full constructor
        public MeasurementRecord(
            int id,
            double inputValue,
            string fromUnit,
            string toUnit,
            double resultValue,
            DateTime conversionDateTime,
            MeasurementType measurementType,
            OperationType operationType,
            bool isError = false,
            string? errorMessage = null)
        {
            Id = id;
            InputValue = inputValue;
            FromUnit = fromUnit;
            ToUnit = toUnit;
            ResultValue = resultValue;
            ConversionDateTime = conversionDateTime;
            MeasurementType = measurementType;
            OperationType = operationType;
            IsError = isError;
            ErrorMessage = errorMessage;
        }
    }
}