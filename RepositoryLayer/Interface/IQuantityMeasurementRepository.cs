using System.Collections.Generic;
using ModelLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IQuantityMeasurementRepository
    {
        void SaveMeasurement(MeasurementRecord record);
        List<MeasurementRecord> GetAllMeasurements();
        int GetTotalCount();
        void DeleteAllMeasurements();
    }
}
