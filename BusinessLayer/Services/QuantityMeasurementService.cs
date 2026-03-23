using System;
using ModelLayer.Entity;
using ModelLayer;
using RepositoryLayer.Interface;
using BusinessLayer.Services;

namespace BusinessLayer.Services
{
    public class QuantityMeasurementService : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository _repository;

        public QuantityMeasurementService(IQuantityMeasurementRepository repository)
        {
            _repository = repository;
        }

        public Quantity<LengthUnit> ConvertLength(Quantity<LengthUnit> quantity, LengthUnit targetUnit)
        {
            var result = quantity.ConvertTo(targetUnit);
            var record = new MeasurementRecord(0, quantity.Value, quantity.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now);
            _repository.SaveMeasurement(record);
            return result;
        }

        public Quantity<LengthUnit> AddLengths(Quantity<LengthUnit> q1, Quantity<LengthUnit> q2, LengthUnit targetUnit)
        {
            var result = q1.Add(q2, targetUnit);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now);
            _repository.SaveMeasurement(record);
            return result;
        }

        public Quantity<LengthUnit> SubtractLengths(Quantity<LengthUnit> q1, Quantity<LengthUnit> q2, LengthUnit targetUnit)
        {
            var result = q1.Subtract(q2, targetUnit);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now);
            _repository.SaveMeasurement(record);
            return result;
        }

        public bool AreLengthsEqual(Quantity<LengthUnit> q1, Quantity<LengthUnit> q2)
        {
            var result = q1.Equals(q2);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), q2.Unit.ToString(), result ? 1.0 : 0.0, DateTime.Now);
            _repository.SaveMeasurement(record);
            return result;
        }

        // Volume
        public Quantity<VolumeUnit> ConvertVolume(Quantity<VolumeUnit> quantity, VolumeUnit targetUnit)
        {
            var result = quantity.ConvertTo(targetUnit);
            var record = new MeasurementRecord(0, quantity.Value, quantity.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now);
            _repository.SaveMeasurement(record);
            return result;
        }

        public Quantity<VolumeUnit> AddVolumes(Quantity<VolumeUnit> q1, Quantity<VolumeUnit> q2, VolumeUnit targetUnit)
        {
            var result = q1.Add(q2, targetUnit);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now);
            _repository.SaveMeasurement(record);
            return result;
        }

        public Quantity<VolumeUnit> SubtractVolumes(Quantity<VolumeUnit> q1, Quantity<VolumeUnit> q2, VolumeUnit targetUnit)
        {
            var result = q1.Subtract(q2, targetUnit);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now);
            _repository.SaveMeasurement(record);
            return result;
        }

        public bool AreVolumesEqual(Quantity<VolumeUnit> q1, Quantity<VolumeUnit> q2)
        {
            var result = q1.Equals(q2);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), q2.Unit.ToString(), result ? 1.0 : 0.0, DateTime.Now);
            _repository.SaveMeasurement(record);
            return result;
        }

        // Weight
        public Quantity<WeightUnit> ConvertWeight(Quantity<WeightUnit> quantity, WeightUnit targetUnit)
        {
            var result = quantity.ConvertTo(targetUnit);
            var record = new MeasurementRecord(0, quantity.Value, quantity.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now);
            _repository.SaveMeasurement(record);
            return result;
        }

        public Quantity<WeightUnit> AddWeights(Quantity<WeightUnit> q1, Quantity<WeightUnit> q2, WeightUnit targetUnit)
        {
            var result = q1.Add(q2, targetUnit);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now);
            _repository.SaveMeasurement(record);
            return result;
        }

        public Quantity<WeightUnit> SubtractWeights(Quantity<WeightUnit> q1, Quantity<WeightUnit> q2, WeightUnit targetUnit)
        {
            var result = q1.Subtract(q2, targetUnit);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now);
            _repository.SaveMeasurement(record);
            return result;
        }

        public bool AreWeightsEqual(Quantity<WeightUnit> q1, Quantity<WeightUnit> q2)
        {
            var result = q1.Equals(q2);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), q2.Unit.ToString(), result ? 1.0 : 0.0, DateTime.Now);
            _repository.SaveMeasurement(record);
            return result;
        }

        // Temperature (Conversion only)
        public Quantity<TemperatureUnit> ConvertTemperature(Quantity<TemperatureUnit> quantity, TemperatureUnit targetUnit)
        {
            var result = quantity.ConvertTo(targetUnit);
            var record = new MeasurementRecord(0, quantity.Value, quantity.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now);
            _repository.SaveMeasurement(record);
            return result;
        }

        public bool AreTemperaturesEqual(Quantity<TemperatureUnit> q1, Quantity<TemperatureUnit> q2)
        {
            var result = q1.Equals(q2);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), q2.Unit.ToString(), result ? 1.0 : 0.0, DateTime.Now);
            _repository.SaveMeasurement(record);
            return result;
        }
    }
}
