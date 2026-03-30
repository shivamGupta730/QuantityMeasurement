using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelLayer.Entity;
using RepositoryLayer.Interface;
using Microsoft.Extensions.Logging;

namespace RepositoryLayer.Repository
{
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private readonly List<MeasurementRecord> _measurements = new();
        private readonly ILogger<QuantityMeasurementCacheRepository> _logger;

        public QuantityMeasurementCacheRepository(ILogger<QuantityMeasurementCacheRepository> logger)
        {
            _logger = logger;
        }

        public Task SaveMeasurementAsync(MeasurementRecord record)
        {
            _measurements.Add(record);
            _logger.LogInformation("Saved measurement to cache: {FromUnit} -> {ToUnit}", record.FromUnit, record.ToUnit);
            return Task.CompletedTask;
        }

        public Task<List<MeasurementRecord>> GetAllMeasurementsAsync()
        {
            _logger.LogInformation("Fetched {Count} cache measurements", _measurements.Count);
            return Task.FromResult(_measurements.OrderByDescending(m => m.ConversionDateTime).ToList());
        }

        public Task<int> GetTotalCountAsync()
        {
            return Task.FromResult(_measurements.Count);
        }

        public Task DeleteAllMeasurementsAsync()
        {
            _measurements.Clear();
            _logger.LogInformation("Cleared all cache measurements");
            return Task.CompletedTask;
        }
    }
}
