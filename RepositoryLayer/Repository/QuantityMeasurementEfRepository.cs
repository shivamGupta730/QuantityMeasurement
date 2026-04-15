using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Interface;
using RepositoryLayer.Context;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class QuantityMeasurementEfRepository : IQuantityMeasurementRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<QuantityMeasurementEfRepository> _logger;

        public QuantityMeasurementEfRepository(
            AppDbContext context,
            ILogger<QuantityMeasurementEfRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SaveMeasurementAsync(MeasurementRecord record)
        {
            try
            {
                await _context.Measurements.AddAsync(record);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save measurement");
                throw;
            }
        }

        public async Task<List<MeasurementRecord>> GetAllMeasurementsAsync()
        {
            return await _context.Measurements
                .OrderByDescending(m => m.ConversionDateTime)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Measurements.CountAsync();
        }

        public async Task DeleteAllMeasurementsAsync()
        {
            var all = await _context.Measurements.ToListAsync();
            _context.Measurements.RemoveRange(all);
            await _context.SaveChangesAsync();
        }

        public List<MeasurementRecord> GetByOperationType(string operationType) => new();
        public List<MeasurementRecord> GetByMeasurementType(string measurementType) => new();
        public List<MeasurementRecord> GetErrorHistory() => new();
        public int GetOperationCount(string operationType) => 0;

        public Task PublishEventAsync(string eventJson) => Task.CompletedTask;
        public Task<string?> GetFromCacheAsync(string key) => Task.FromResult<string?>(null);
        public Task SetCacheAsync(string key, string value, TimeSpan expiry) => Task.CompletedTask;
    }
}