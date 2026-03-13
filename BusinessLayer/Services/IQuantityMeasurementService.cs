using System;
using ModelLayer.Entity;
using ModelLayer;



namespace BusinessLayer.Services
{
    public interface IQuantityMeasurementService
    {
        // Length operations
        Quantity<LengthUnit> ConvertLength(Quantity<LengthUnit> quantity, LengthUnit targetUnit);
        Quantity<LengthUnit> AddLengths(Quantity<LengthUnit> q1, Quantity<LengthUnit> q2, LengthUnit targetUnit);
        Quantity<LengthUnit> SubtractLengths(Quantity<LengthUnit> q1, Quantity<LengthUnit> q2, LengthUnit targetUnit);
        bool AreLengthsEqual(Quantity<LengthUnit> q1, Quantity<LengthUnit> q2);

        // Volume operations
        Quantity<VolumeUnit> ConvertVolume(Quantity<VolumeUnit> quantity, VolumeUnit targetUnit);
        Quantity<VolumeUnit> AddVolumes(Quantity<VolumeUnit> q1, Quantity<VolumeUnit> q2, VolumeUnit targetUnit);
        Quantity<VolumeUnit> SubtractVolumes(Quantity<VolumeUnit> q1, Quantity<VolumeUnit> q2, VolumeUnit targetUnit);
        bool AreVolumesEqual(Quantity<VolumeUnit> q1, Quantity<VolumeUnit> q2);

        // Weight operations
        Quantity<WeightUnit> ConvertWeight(Quantity<WeightUnit> quantity, WeightUnit targetUnit);
        Quantity<WeightUnit> AddWeights(Quantity<WeightUnit> q1, Quantity<WeightUnit> q2, WeightUnit targetUnit);
        Quantity<WeightUnit> SubtractWeights(Quantity<WeightUnit> q1, Quantity<WeightUnit> q2, WeightUnit targetUnit);
        bool AreWeightsEqual(Quantity<WeightUnit> q1, Quantity<WeightUnit> q2);

        // Temperature operations
        Quantity<TemperatureUnit> ConvertTemperature(Quantity<TemperatureUnit> quantity, TemperatureUnit targetUnit);
        bool AreTemperaturesEqual(Quantity<TemperatureUnit> q1, Quantity<TemperatureUnit> q2);
    }
}
