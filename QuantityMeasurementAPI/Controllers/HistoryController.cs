using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services;
using ModelLayer.Enum;
using QuantityMeasurementAPI.DTOs;

namespace QuantityMeasurementAPI.Controllers
{
[ApiController]
[Route("api/[controller]")]
[Microsoft.AspNetCore.Authorization.Authorize]
public class HistoryController : ControllerBase
{
    private readonly IQuantityMeasurementService _service;
    private readonly ILogger<HistoryController> _logger;

    public HistoryController(IQuantityMeasurementService service, ILogger<HistoryController> logger)
    {
        _service = service;
        _logger = logger;
    }

[HttpGet("by-operation/{operation}")]
        public async Task<IActionResult> GetHistoryByOperationAsync(OperationType operation)
        {
            var history = await _service.GetHistoryByOperationAsync(operation);
            return Ok(new HistoryResponseDto { History = history, TotalCount = history.Count() });
        }

[HttpGet("by-type/{type}")]
        public async Task<IActionResult> GetHistoryByMeasurementTypeAsync(MeasurementType type)
        {
            var history = await _service.GetHistoryByMeasurementTypeAsync(type);
            return Ok(new HistoryResponseDto { History = history, TotalCount = history.Count() });
        }

[HttpGet("errors")]
        public async Task<IActionResult> GetErrorHistoryAsync()
        {
            var history = await _service.GetErrorHistoryAsync();
            return Ok(new HistoryResponseDto { History = history, TotalCount = history.Count() });
        }

[HttpGet("count/{operation}")]
        public async Task<IActionResult> GetOperationCountAsync(OperationType operation)
        {
            var count = await _service.GetOperationCountAsync(operation);
            return Ok(new { Operation = operation, Count = count });
        }
    }
}

