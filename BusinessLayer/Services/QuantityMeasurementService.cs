using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLayer.Entity;
using ModelLayer;
using ModelLayer.Enum;
using RepositoryLayer.Interface;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services
{
public class QuantityMeasurementService : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository _repository;
        private readonly ILogger<QuantityMeasurementService> _logger;

        public QuantityMeasurementService(IQuantityMeasurementRepository repository, ILogger<QuantityMeasurementService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Quantity<LengthUnit>> ConvertLengthAsync(Quantity<LengthUnit> quantity, LengthUnit targetUnit)
        {
            try
            {
                _logger.LogInformation("Converting length {Value} {Unit} to {TargetUnit}", quantity.Value, quantity.Unit, targetUnit);
                var result = quantity.ConvertTo(targetUnit);
                var record = new MeasurementRecord(0, quantity.Value, quantity.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now, MeasurementType.Length, OperationType.Convert);
                await _repository.SaveMeasurementAsync(record);
                _logger.LogInformation("Length conversion successful: {Value} {Unit} = {Result} {TargetUnit}", quantity.Value, quantity.Unit, result.Value, targetUnit);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Length conversion failed for {Value} {Unit} to {TargetUnit}", quantity.Value, quantity.Unit, targetUnit);
                throw;
            }
        }

        public async Task<Quantity<LengthUnit>> AddLengthsAsync(Quantity<LengthUnit> q1, Quantity<LengthUnit> q2, LengthUnit targetUnit)
        {
            try
            {
                _logger.LogInformation("Adding lengths {Value1} {Unit1} + {Value2} {Unit2} to {TargetUnit}", q1.Value, q1.Unit, q2.Value, q2.Unit, targetUnit);
                var result = q1.Add(q2, targetUnit);
                var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now, MeasurementType.Length, OperationType.Add);
                await _repository.SaveMeasurementAsync(record);
                _logger.LogInformation("Length addition successful: {Result} {TargetUnit}", result.Value, targetUnit);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Length addition failed for {Value1} {Unit1} + {Value2} {Unit2}", q1.Value, q1.Unit, q2.Value, q2.Unit);
                throw;
            }
        }

        public async Task<Quantity<LengthUnit>> SubtractLengthsAsync(Quantity<LengthUnit> q1, Quantity<LengthUnit> q2, LengthUnit targetUnit)
        {
            try
            {
                _logger.LogInformation("Subtracting lengths {Value1} {Unit1} - {Value2} {Unit2} to {TargetUnit}", q1.Value, q1.Unit, q2.Value, q2.Unit, targetUnit);
                var result = q1.Subtract(q2, targetUnit);
                var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now, MeasurementType.Length, OperationType.Subtract);
                await _repository.SaveMeasurementAsync(record);
                _logger.LogInformation("Length subtraction successful: {Result} {TargetUnit}", result.Value, targetUnit);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Length subtraction failed for {Value1} {Unit1} - {Value2} {Unit2}", q1.Value, q1.Unit, q2.Value, q2.Unit);
                throw;
            }
        }

        public async Task<bool> AreLengthsEqualAsync(Quantity<LengthUnit> q1, Quantity<LengthUnit> q2)
        {
            try
            {
                _logger.LogInformation("Comparing lengths {Value1} {Unit1} vs {Value2} {Unit2}", q1.Value, q1.Unit, q2.Value, q2.Unit);
                var result = q1.Equals(q2);
                var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), q2.Unit.ToString(), result ? 1.0 : 0.0, DateTime.Now, MeasurementType.Length, OperationType.Compare);
                await _repository.SaveMeasurementAsync(record);
                _logger.LogInformation("Length comparison result: {Result}", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Length comparison failed for {Value1} {Unit1} vs {Value2} {Unit2}", q1.Value, q1.Unit, q2.Value, q2.Unit);
                throw;
            }
        }

        // Volume (similar async updates)
        public async Task<Quantity<VolumeUnit>> ConvertVolumeAsync(Quantity<VolumeUnit> quantity, VolumeUnit targetUnit)
        {
            var result = quantity.ConvertTo(targetUnit);
            var record = new MeasurementRecord(0, quantity.Value, quantity.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now, MeasurementType.Volume, OperationType.Convert);
            await _repository.SaveMeasurementAsync(record);
            return result;
        }

        public async Task<Quantity<VolumeUnit>> AddVolumesAsync(Quantity<VolumeUnit> q1, Quantity<VolumeUnit> q2, VolumeUnit targetUnit)
        {
            var result = q1.Add(q2, targetUnit);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now, MeasurementType.Volume, OperationType.Add);
            await _repository.SaveMeasurementAsync(record);
            return result;
        }

        public async Task<Quantity<VolumeUnit>> SubtractVolumesAsync(Quantity<VolumeUnit> q1, Quantity<VolumeUnit> q2, VolumeUnit targetUnit)
        {
            var result = q1.Subtract(q2, targetUnit);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now, MeasurementType.Volume, OperationType.Subtract);
            await _repository.SaveMeasurementAsync(record);
            return result;
        }

        public async Task<bool> AreVolumesEqualAsync(Quantity<VolumeUnit> q1, Quantity<VolumeUnit> q2)
        {
            var result = q1.Equals(q2);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), q2.Unit.ToString(), result ? 1.0 : 0.0, DateTime.Now, MeasurementType.Volume, OperationType.Compare);
            await _repository.SaveMeasurementAsync(record);
            return result;
        }

        // Weight (async)
        public async Task<Quantity<WeightUnit>> ConvertWeightAsync(Quantity<WeightUnit> quantity, WeightUnit targetUnit)
        {
            var result = quantity.ConvertTo(targetUnit);
            var record = new MeasurementRecord(0, quantity.Value, quantity.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now, MeasurementType.Weight, OperationType.Convert);
            await _repository.SaveMeasurementAsync(record);
            return result;
        }

        public async Task<Quantity<WeightUnit>> AddWeightsAsync(Quantity<WeightUnit> q1, Quantity<WeightUnit> q2, WeightUnit targetUnit)
        {
            var result = q1.Add(q2, targetUnit);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now, MeasurementType.Weight, OperationType.Add);
            await _repository.SaveMeasurementAsync(record);
            return result;
        }

        public async Task<Quantity<WeightUnit>> SubtractWeightsAsync(Quantity<WeightUnit> q1, Quantity<WeightUnit> q2, WeightUnit targetUnit)
        {
            var result = q1.Subtract(q2, targetUnit);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now, MeasurementType.Weight, OperationType.Subtract);
            await _repository.SaveMeasurementAsync(record);
            return result;
        }

        public async Task<bool> AreWeightsEqualAsync(Quantity<WeightUnit> q1, Quantity<WeightUnit> q2)
        {
            var result = q1.Equals(q2);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), q2.Unit.ToString(), result ? 1.0 : 0.0, DateTime.Now, MeasurementType.Weight, OperationType.Compare);
            await _repository.SaveMeasurementAsync(record);
            return result;
        }

        // Temperature (async)
        public async Task<Quantity<TemperatureUnit>> ConvertTemperatureAsync(Quantity<TemperatureUnit> quantity, TemperatureUnit targetUnit)
        {
            var result = quantity.ConvertTo(targetUnit);
            var record = new MeasurementRecord(0, quantity.Value, quantity.Unit.ToString(), targetUnit.ToString(), result.Value, DateTime.Now, MeasurementType.Temperature, OperationType.Convert);
            await _repository.SaveMeasurementAsync(record);
            return result;
        }

        public async Task<bool> AreTemperaturesEqualAsync(Quantity<TemperatureUnit> q1, Quantity<TemperatureUnit> q2)
        {
            var result = q1.Equals(q2);
            var record = new MeasurementRecord(0, q1.Value, q1.Unit.ToString(), q2.Unit.ToString(), result ? 1.0 : 0.0, DateTime.Now, MeasurementType.Temperature, OperationType.Compare);
            await _repository.SaveMeasurementAsync(record);
            return result;
        }

        // History methods (update to async)
        public async Task<List<MeasurementRecord>> GetHistoryByOperationAsync(OperationType operation)
        {
            var all = await _repository.GetAllMeasurementsAsync();
            return all.Where(r => r.OperationType == operation).OrderByDescending(r => r.ConversionDateTime).ToList();
        }

        public async Task<List<MeasurementRecord>> GetHistoryByMeasurementTypeAsync(MeasurementType type)
        {
            var all = await _repository.GetAllMeasurementsAsync();
            return all.Where(r => r.MeasurementType == type).OrderByDescending(r => r.ConversionDateTime).ToList();
        }

        public async Task<List<MeasurementRecord>> GetErrorHistoryAsync()
        {
            var all = await _repository.GetAllMeasurementsAsync();
            return all.Where(r => r.IsError).OrderByDescending(r => r.ConversionDateTime).ToList();
        }

        public async Task<int> GetOperationCountAsync(OperationType operation)
        {
            var all = await _repository.GetAllMeasurementsAsync();
            return all.Count(r => r.OperationType == operation);
        }
    }
}
