using System.Collections.Generic;
using System.Linq;
using ModelLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Repository
{
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private readonly List<MeasurementRecord> _measurements = new();

        public void SaveMeasurement(MeasurementRecord record)
        {
            _measurements.Add(record);
        }

        public List<MeasurementRecord> GetAllMeasurements()
        {
            return _measurements.ToList();
        }

        public int GetTotalCount()
        {
            return _measurements.Count;
        }

        public void DeleteAllMeasurements()
        {
            _measurements.Clear();
        }
    }
}
