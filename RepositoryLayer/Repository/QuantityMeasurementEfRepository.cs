using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        public QuantityMeasurementEfRepository(AppDbContext context, ILogger<QuantityMeasurementEfRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SaveMeasurementAsync(MeasurementRecord record)
        {
            try
            {
                _logger.LogInformation("Saving measurement record {FromUnit} -> {ToUnit}: {InputValue} = {ResultValue}", record.FromUnit, record.ToUnit, record.InputValue, record.ResultValue);
                await _context.Measurements.AddAsync(record);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Measurement record saved successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save measurement {FromUnit} -> {ToUnit}", record.FromUnit, record.ToUnit);
                throw;
            }
        }

        public async Task<List<MeasurementRecord>> GetAllMeasurementsAsync()
        {
            try
            {
                _logger.LogInformation("Fetching all measurement records");
                var measurements = await _context.Measurements
                    .OrderByDescending(m => m.ConversionDateTime)
                    .ToListAsync();
                _logger.LogInformation("Fetched {Count} measurement records", measurements.Count);
                return measurements;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch all measurements");
                throw;
            }
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Measurements.CountAsync();
        }

        public async Task DeleteAllMeasurementsAsync()
        {
            var allMeasurements = await _context.Measurements.ToListAsync();
            _context.Measurements.RemoveRange(allMeasurements);
            await _context.SaveChangesAsync();
        }

        // Stubs preserved
        public List<MeasurementRecord> GetByOperationType(string operationType) => new();
        public List<MeasurementRecord> GetByMeasurementType(string measurementType) => new();
        public List<MeasurementRecord> GetErrorHistory() => new();
        public int GetOperationCount(string operationType) => 0;

        public Task PublishEventAsync(string eventJson) => Task.CompletedTask;
        public Task<string?> GetFromCacheAsync(string key) => Task.FromResult<string?>(null);
        public Task SetCacheAsync(string key, string value, TimeSpan expiry) => Task.CompletedTask;
    }
}
