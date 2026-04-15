using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer;
using ModelLayer.Enum;
using ModelLayer.Entity;

namespace BusinessLayer.Services
{
    public interface IQuantityMeasurementService
    {
        Task SaveMeasurementAsync(MeasurementRecord record);
        Task<Quantity<LengthUnit>> ConvertLengthAsync(Quantity<LengthUnit> quantity, LengthUnit targetUnit);
        Task<Quantity<LengthUnit>> AddLengthsAsync(Quantity<LengthUnit> q1, Quantity<LengthUnit> q2, LengthUnit targetUnit);

        Task<Quantity<VolumeUnit>> ConvertVolumeAsync(Quantity<VolumeUnit> quantity, VolumeUnit targetUnit);
        Task<Quantity<VolumeUnit>> AddVolumesAsync(Quantity<VolumeUnit> q1, Quantity<VolumeUnit> q2, VolumeUnit targetUnit);

        Task<Quantity<WeightUnit>> ConvertWeightAsync(Quantity<WeightUnit> quantity, WeightUnit targetUnit);
        Task<Quantity<WeightUnit>> AddWeightsAsync(Quantity<WeightUnit> q1, Quantity<WeightUnit> q2, WeightUnit targetUnit);

        Task<Quantity<TemperatureUnit>> ConvertTemperatureAsync(Quantity<TemperatureUnit> quantity, TemperatureUnit targetUnit);

        Task<List<MeasurementRecord>> GetHistoryByOperationAsync(OperationType operation);
        Task<List<MeasurementRecord>> GetHistoryByMeasurementTypeAsync(MeasurementType type);
        Task<List<MeasurementRecord>> GetErrorHistoryAsync();

        Task<int> GetOperationCountAsync(OperationType operation);
    }
}