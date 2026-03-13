using System;
using ModelLayer.Entity;
using ModelLayer;


using BusinessLayer.Services;

namespace BusinessLayer.Services
{
    public class QuantityMeasurementService : IQuantityMeasurementService
    {
        // Length operations
        public Quantity<LengthUnit> ConvertLength(Quantity<LengthUnit> quantity, LengthUnit targetUnit)
        {
            return quantity.ConvertTo(targetUnit);
        }

        public Quantity<LengthUnit> AddLengths(Quantity<LengthUnit> q1, Quantity<LengthUnit> q2, LengthUnit targetUnit)
        {
            return q1.Add(q2, targetUnit);
        }

        public Quantity<LengthUnit> SubtractLengths(Quantity<LengthUnit> q1, Quantity<LengthUnit> q2, LengthUnit targetUnit)
        {
            return q1.Subtract(q2, targetUnit);
        }

        public bool AreLengthsEqual(Quantity<LengthUnit> q1, Quantity<LengthUnit> q2)
        {
            return q1.Equals(q2);
        }

        // Volume operations
        public Quantity<VolumeUnit> ConvertVolume(Quantity<VolumeUnit> quantity, VolumeUnit targetUnit)
        {
            return quantity.ConvertTo(targetUnit);
        }

        public Quantity<VolumeUnit> AddVolumes(Quantity<VolumeUnit> q1, Quantity<VolumeUnit> q2, VolumeUnit targetUnit)
        {
            return q1.Add(q2, targetUnit);
        }

        public Quantity<VolumeUnit> SubtractVolumes(Quantity<VolumeUnit> q1, Quantity<VolumeUnit> q2, VolumeUnit targetUnit)
        {
            return q1.Subtract(q2, targetUnit);
        }

        public bool AreVolumesEqual(Quantity<VolumeUnit> q1, Quantity<VolumeUnit> q2)
        {
            return q1.Equals(q2);
        }

        // Weight operations
        public Quantity<WeightUnit> ConvertWeight(Quantity<WeightUnit> quantity, WeightUnit targetUnit)
        {
            return quantity.ConvertTo(targetUnit);
        }

        public Quantity<WeightUnit> AddWeights(Quantity<WeightUnit> q1, Quantity<WeightUnit> q2, WeightUnit targetUnit)
        {
            return q1.Add(q2, targetUnit);
        }

        public Quantity<WeightUnit> SubtractWeights(Quantity<WeightUnit> q1, Quantity<WeightUnit> q2, WeightUnit targetUnit)
        {
            return q1.Subtract(q2, targetUnit);
        }

        public bool AreWeightsEqual(Quantity<WeightUnit> q1, Quantity<WeightUnit> q2)
        {
            return q1.Equals(q2);
        }

        // Temperature operations
        public Quantity<TemperatureUnit> ConvertTemperature(Quantity<TemperatureUnit> quantity, TemperatureUnit targetUnit)
        {
            return quantity.ConvertTo(targetUnit);
        }

        public bool AreTemperaturesEqual(Quantity<TemperatureUnit> q1, Quantity<TemperatureUnit> q2)
        {
            return q1.Equals(q2);
        }

        // Legacy static demo methods (kept for compatibility)
        public static void DemonstrateEquality<U>(Quantity<U> q1, Quantity<U> q2)
        {
            Console.WriteLine($"Comparing {q1} and {q2}");
            Console.WriteLine($"Equal: {q1.Equals(q2)}");
            Console.WriteLine();
        }

        public static void DemonstrateConversion<U>(Quantity<U> quantity, U targetUnit)
        {
            var converted = quantity.ConvertTo(targetUnit);
            Console.WriteLine($"Converted: {converted}");
            Console.WriteLine();
        }

        public static void DemonstrateAddition<U>(Quantity<U> q1, Quantity<U> q2, U targetUnit)
        {
            var result = q1.Add(q2, targetUnit);
            Console.WriteLine($"Addition Result: {result}");
            Console.WriteLine();
        }
    }
}
