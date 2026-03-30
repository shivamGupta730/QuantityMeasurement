using System.Collections.Generic;
using System.Threading.Tasks;
using ModelLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IQuantityMeasurementRepository
    {
        Task SaveMeasurementAsync(MeasurementRecord record);
        Task<List<MeasurementRecord>> GetAllMeasurementsAsync();
        Task<int> GetTotalCountAsync();
        Task DeleteAllMeasurementsAsync();
    }
}
