using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services;
using RepositoryLayer.Interface;
using ModelLayer.Entity;
using ModelLayer;
using QuantityMeasurementAPI.DTOs;
using ModelLayer.Enum;

namespace QuantityMeasurementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class MeasurementController : ControllerBase
{
    private readonly IQuantityMeasurementService _service;
    private readonly ILogger<MeasurementController> _logger;

  public MeasurementController(
    IQuantityMeasurementService service,
    ILogger<MeasurementController> logger)
{
    _service = service;
    _logger = logger;
}

    // Length endpoints
    [HttpPost("convert-length")]
    public async Task<IActionResult> ConvertLengthAsync([FromBody] MeasurementRequestDto request)
    {
        try 
        {
            _logger.LogInformation("Length conversion request started: {Value} {SourceUnit} -> {TargetUnit}", request.Value, request.SourceUnit, request.TargetUnit);
            var sourceUnit = ParseLengthUnit(request.SourceUnit);
            var targetUnit = ParseLengthUnit(request.TargetUnit);
            var quantity = new Quantity<LengthUnit>(request.Value, sourceUnit);
            var result = await _service.ConvertLengthAsync(quantity, targetUnit);
            
            _logger.LogInformation("Length conversion completed with OperationType=Convert - history saved");
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Length conversion failed: {Value} {SourceUnit} -> {TargetUnit}", request.Value, request.SourceUnit, request.TargetUnit);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("divide-length")]
    public async Task<IActionResult> DivideLengthAsync([FromBody] AddMeasurementRequestDto request)
    {
        if (request == null)
        {
            _logger.LogWarning("Divide length request invalid: null body");
            return BadRequest("Invalid request");
        }

        try 
        {
            _logger.LogInformation("Length division request: {Value1} {Unit1} / {Value2} {Unit2} -> {TargetUnit}", request.Value1, request.Unit1, request.Value2, request.Unit2, request.TargetUnit);

            if (request.Value2 == 0)
            {
                var errorRecord = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, 0, DateTime.Now, ModelLayer.Enum.MeasurementType.Length, ModelLayer.Enum.OperationType.Divide, isError: true, "Cannot divide by zero");
                await _repository.SaveMeasurementAsync(errorRecord);
                _logger.LogWarning("Length division by zero prevented");
                return BadRequest("Cannot divide by zero");
            }

            if (request.Value2 < 0)
            {
                var errorRecord = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, 0, DateTime.Now, ModelLayer.Enum.MeasurementType.Length, ModelLayer.Enum.OperationType.Divide, isError: true, "Divisor cannot be negative");
                await _repository.SaveMeasurementAsync(errorRecord);
                _logger.LogWarning("Length division with negative divisor prevented");
                return BadRequest("Divisor cannot be negative");
            }

            var unit1 = ParseLengthUnit(request.Unit1);
            var targetUnit = ParseLengthUnit(request.TargetUnit);
            var q1 = new Quantity<LengthUnit>(request.Value1, unit1);
            var divisor = request.Value2;
            var resultValue = request.Value1 / divisor;
            var result = new Quantity<LengthUnit>(resultValue, targetUnit); // Direct division then unit, since no Divide method
            
            var record = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, result.Value, DateTime.Now, ModelLayer.Enum.MeasurementType.Length, ModelLayer.Enum.OperationType.Divide);
            await _repository.SaveMeasurementAsync(record);
            
            _logger.LogInformation("Length division completed with OperationType=Divide - history saved");
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (ArgumentException ex)
        {
            var errorRecord = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, 0, DateTime.Now, ModelLayer.Enum.MeasurementType.Length, ModelLayer.Enum.OperationType.Divide, isError: true, "Unknown unit");
            await _repository.SaveMeasurementAsync(errorRecord);
            _logger.LogError(ex, "Length division unknown unit");
            return BadRequest("Unknown unit");
        }
        catch (Exception ex)
        {
            var errorRecord = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, 0, DateTime.Now, ModelLayer.Enum.MeasurementType.Length, ModelLayer.Enum.OperationType.Divide, isError: true, ex.Message);
            await _repository.SaveMeasurementAsync(errorRecord);
            _logger.LogError(ex, "Length division unexpected error");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("add-lengths")]
    public async Task<IActionResult> AddLengthsAsync([FromBody] AddMeasurementRequestDto request)
    {
        try 
        {
            var unit1 = ParseLengthUnit(request.Unit1);
            var unit2 = ParseLengthUnit(request.Unit2);
            var targetUnit = ParseLengthUnit(request.TargetUnit);
            var q1 = new Quantity<LengthUnit>(request.Value1, unit1);
            var q2 = new Quantity<LengthUnit>(request.Value2, unit2);
            var result = await _service.AddLengthsAsync(q1, q2, targetUnit);
            
            _logger.LogInformation("Length addition completed with OperationType=Add - history saved");
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Length addition failed");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("subtract-lengths")]
    public async Task<IActionResult> SubtractLengthsAsync([FromBody] AddMeasurementRequestDto request)
    {
        try 
        {
            _logger.LogInformation("Length subtraction request: {Value1} {Unit1} - {Value2} {Unit2} -> {TargetUnit}", request.Value1, request.Unit1, request.Value2, request.Unit2, request.TargetUnit);
            var unit1 = ParseLengthUnit(request.Unit1);
            var unit2 = ParseLengthUnit(request.Unit2);
            var targetUnit = ParseLengthUnit(request.TargetUnit);
            var q1 = new Quantity<LengthUnit>(request.Value1, unit1);
            var q2 = new Quantity<LengthUnit>(request.Value2, unit2);
            var result = q1.Subtract(q2, targetUnit);
            var record = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, result.Value, DateTime.Now, ModelLayer.Enum.MeasurementType.Length, ModelLayer.Enum.OperationType.Subtract);
            await _repository.SaveMeasurementAsync(record);
            
            _logger.LogInformation("Length subtraction completed with OperationType=Subtract - history saved");
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Length subtraction failed");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("compare-length")]
    public async Task<IActionResult> CompareLengthAsync([FromBody] CompareRequestDto request)
    {
        try 
        {
            _logger.LogInformation("Length compare request: {Value1} {Unit1} vs {Value2} {Unit2}", request.Value1, request.Unit1, request.Value2, request.Unit2);
            var unit1 = ParseLengthUnit(request.Unit1);
            var unit2 = ParseLengthUnit(request.Unit2);
            var q1 = new Quantity<LengthUnit>(request.Value1, unit1);
            var q2 = new Quantity<LengthUnit>(request.Value2, unit2);
            var isEqual = q1.Equals(q2);
            var record = new MeasurementRecord(0, request.Value1, request.Unit1, request.Unit2, isEqual ? 1.0 : 0.0, DateTime.Now, ModelLayer.Enum.MeasurementType.Length, ModelLayer.Enum.OperationType.Compare);
            await _repository.SaveMeasurementAsync(record);
            var message = isEqual ? "Equal" : (request.Value1 > request.Value2 ? "Value 1 > Value 2" : "Value 1 < Value 2");
            
            _logger.LogInformation("Length compare completed with OperationType=Compare - history saved");
            return Ok(new CompareResultDto 
            { 
                IsEqual = isEqual,
                Message = message,
                Value1Base = request.Value1,
                Value2Base = request.Value2
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Length compare failed");
            return BadRequest(ex.Message);
        }
    }

    // Volume endpoints
    [HttpPost("divide-volume")]
    public async Task<IActionResult> DivideVolumeAsync([FromBody] AddMeasurementRequestDto request)
    {
        if (request == null)
        {
            _logger.LogWarning("Divide volume request invalid: null body");
            return BadRequest("Invalid request");
        }

        try 
        {
            _logger.LogInformation("Volume division request: {Value1} {Unit1} / {Value2} {Unit2} -> {TargetUnit}", request.Value1, request.Unit1, request.Value2, request.Unit2, request.TargetUnit);

            if (request.Value2 == 0)
            {
                var errorRecord = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, 0, DateTime.Now, ModelLayer.Enum.MeasurementType.Volume, ModelLayer.Enum.OperationType.Divide, isError: true, "Cannot divide by zero");
                await _repository.SaveMeasurementAsync(errorRecord);
                _logger.LogWarning("Volume division by zero prevented");
                return BadRequest("Cannot divide by zero");
            }

            if (request.Value2 < 0)
            {
                var errorRecord = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, 0, DateTime.Now, ModelLayer.Enum.MeasurementType.Volume, ModelLayer.Enum.OperationType.Divide, isError: true, "Divisor cannot be negative");
                await _repository.SaveMeasurementAsync(errorRecord);
                _logger.LogWarning("Volume division with negative divisor prevented");
                return BadRequest("Divisor cannot be negative");
            }

            var unit1 = ParseVolumeUnit(request.Unit1);
            var targetUnit = ParseVolumeUnit(request.TargetUnit);
            var q1 = new Quantity<VolumeUnit>(request.Value1, unit1);
            var divisor = request.Value2;
            var resultValue = request.Value1 / divisor;
            var result = new Quantity<VolumeUnit>(resultValue, targetUnit);
            
            var record = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, result.Value, DateTime.Now, ModelLayer.Enum.MeasurementType.Volume, ModelLayer.Enum.OperationType.Divide);
            await _repository.SaveMeasurementAsync(record);
            
            _logger.LogInformation("Volume division completed with OperationType=Divide - history saved");
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (ArgumentException ex)
        {
            var errorRecord = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, 0, DateTime.Now, ModelLayer.Enum.MeasurementType.Volume, ModelLayer.Enum.OperationType.Divide, isError: true, "Unknown unit");
            await _repository.SaveMeasurementAsync(errorRecord);
            _logger.LogError(ex, "Volume division unknown unit");
            return BadRequest("Unknown unit");
        }
        catch (Exception ex)
        {
            var errorRecord = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, 0, DateTime.Now, ModelLayer.Enum.MeasurementType.Volume, ModelLayer.Enum.OperationType.Divide, isError: true, ex.Message);
            await _repository.SaveMeasurementAsync(errorRecord);
            _logger.LogError(ex, "Volume division unexpected error");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("convert-volume")]
    public async Task<IActionResult> ConvertVolumeAsync([FromBody] MeasurementRequestDto request)
    {
        try 
        {
            var sourceUnit = ParseVolumeUnit(request.SourceUnit);
            var targetUnit = ParseVolumeUnit(request.TargetUnit);
            var quantity = new Quantity<VolumeUnit>(request.Value, sourceUnit);
            var result = await _service.ConvertVolumeAsync(quantity, targetUnit);
            
            _logger.LogInformation("Volume conversion completed with OperationType=Convert - history saved");
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Volume conversion failed");
            return BadRequest(ex.Message);
        }
    }

    // ... (rest of existing endpoints unchanged: add-volumes, subtract-volumes, compare-volume, all weight, all temperature)
    [HttpPost("add-volumes")]
    public async Task<IActionResult> AddVolumesAsync([FromBody] AddMeasurementRequestDto request)
    {
        try 
        {
            var unit1 = ParseVolumeUnit(request.Unit1);
            var unit2 = ParseVolumeUnit(request.Unit2);
            var targetUnit = ParseVolumeUnit(request.TargetUnit);
            var q1 = new Quantity<VolumeUnit>(request.Value1, unit1);
            var q2 = new Quantity<VolumeUnit>(request.Value2, unit2);
            var result = await _service.AddVolumesAsync(q1, q2, targetUnit);
            
            _logger.LogInformation("Volume addition completed with OperationType=Add - history saved");
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Volume addition failed");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("subtract-volumes")]
    public async Task<IActionResult> SubtractVolumesAsync([FromBody] AddMeasurementRequestDto request)
    {
        try 
        {
            _logger.LogInformation("Volume subtraction request: {Value1} {Unit1} - {Value2} {Unit2} -> {TargetUnit}", request.Value1, request.Unit1, request.Value2, request.Unit2, request.TargetUnit);
            var unit1 = ParseVolumeUnit(request.Unit1);
            var unit2 = ParseVolumeUnit(request.Unit2);
            var targetUnit = ParseVolumeUnit(request.TargetUnit);
            var q1 = new Quantity<VolumeUnit>(request.Value1, unit1);
            var q2 = new Quantity<VolumeUnit>(request.Value2, unit2);
            var result = q1.Subtract(q2, targetUnit);
            var record = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, result.Value, DateTime.Now, ModelLayer.Enum.MeasurementType.Volume, ModelLayer.Enum.OperationType.Subtract);
            await _repository.SaveMeasurementAsync(record);
            
            _logger.LogInformation("Volume subtraction completed with OperationType=Subtract - history saved");
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Volume subtraction failed");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("compare-volume")]
    public async Task<IActionResult> CompareVolumeAsync([FromBody] CompareRequestDto request)
    {
        try 
        {
            var unit1 = ParseVolumeUnit(request.Unit1);
            var unit2 = ParseVolumeUnit(request.Unit2);
            var q1 = new Quantity<VolumeUnit>(request.Value1, unit1);
            var q2 = new Quantity<VolumeUnit>(request.Value2, unit2);
            var isEqual = q1.Equals(q2);
            var record = new MeasurementRecord(0, request.Value1, request.Unit1, request.Unit2, isEqual ? 1.0 : 0.0, DateTime.Now, ModelLayer.Enum.MeasurementType.Volume, ModelLayer.Enum.OperationType.Compare);
            await _repository.SaveMeasurementAsync(record);
            var message = isEqual ? "Equal" : (request.Value1 > request.Value2 ? "Value 1 > Value 2" : "Value 1 < Value 2");
            
            _logger.LogInformation("Volume compare completed with OperationType=Compare - history saved");
            return Ok(new CompareResultDto 
            { 
                IsEqual = isEqual,
                Message = message,
                Value1Base = request.Value1,
                Value2Base = request.Value2
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Volume compare failed");
            return BadRequest(ex.Message);
        }
    }

    // Weight divide, convert, add, subtract, compare (similar to length/volume)
    [HttpPost("divide-weight")]
    public async Task<IActionResult> DivideWeightAsync([FromBody] AddMeasurementRequestDto request)
    {
        if (request == null)
        {
            _logger.LogWarning("Divide weight request invalid: null body");
            return BadRequest("Invalid request");
        }

        try 
        {
            _logger.LogInformation("Weight division request: {Value1} {Unit1} / {Value2} {Unit2} -> {TargetUnit}", request.Value1, request.Unit1, request.Value2, request.Unit2, request.TargetUnit);

            if (request.Value2 == 0)
            {
                var errorRecord = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, 0, DateTime.Now, ModelLayer.Enum.MeasurementType.Weight, ModelLayer.Enum.OperationType.Divide, isError: true, "Cannot divide by zero");
                await _repository.SaveMeasurementAsync(errorRecord);
                _logger.LogWarning("Weight division by zero prevented");
                return BadRequest("Cannot divide by zero");
            }

            if (request.Value2 < 0)
            {
                var errorRecord = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, 0, DateTime.Now, ModelLayer.Enum.MeasurementType.Weight, ModelLayer.Enum.OperationType.Divide, isError: true, "Divisor cannot be negative");
                await _repository.SaveMeasurementAsync(errorRecord);
                _logger.LogWarning("Weight division with negative divisor prevented");
                return BadRequest("Divisor cannot be negative");
            }

            var unit1 = ParseWeightUnit(request.Unit1);
            var targetUnit = ParseWeightUnit(request.TargetUnit);
            var q1 = new Quantity<WeightUnit>(request.Value1, unit1);
            var divisor = request.Value2;
            var resultValue = request.Value1 / divisor;
            var result = new Quantity<WeightUnit>(resultValue, targetUnit);
            
            var record = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, result.Value, DateTime.Now, ModelLayer.Enum.MeasurementType.Weight, ModelLayer.Enum.OperationType.Divide);
            await _repository.SaveMeasurementAsync(record);
            
            _logger.LogInformation("Weight division completed with OperationType=Divide - history saved");
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (ArgumentException ex)
        {
            var errorRecord = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, 0, DateTime.Now, ModelLayer.Enum.MeasurementType.Weight, ModelLayer.Enum.OperationType.Divide, isError: true, "Unknown unit");
            await _repository.SaveMeasurementAsync(errorRecord);
            _logger.LogError(ex, "Weight division unknown unit");
            return BadRequest("Unknown unit");
        }
        catch (Exception ex)
        {
            var errorRecord = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, 0, DateTime.Now, ModelLayer.Enum.MeasurementType.Weight, ModelLayer.Enum.OperationType.Divide, isError: true, ex.Message);
            await _repository.SaveMeasurementAsync(errorRecord);
            _logger.LogError(ex, "Weight division unexpected error");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("convert-weight")]
    public async Task<IActionResult> ConvertWeightAsync([FromBody] MeasurementRequestDto request)
    {
        try 
        {
            var sourceUnit = ParseWeightUnit(request.SourceUnit);
            var targetUnit = ParseWeightUnit(request.TargetUnit);
            var quantity = new Quantity<WeightUnit>(request.Value, sourceUnit);
            var result = await _service.ConvertWeightAsync(quantity, targetUnit);
            
            _logger.LogInformation("Weight conversion completed with OperationType=Convert - history saved");
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Weight conversion failed");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("add-weights")]
    public async Task<IActionResult> AddWeightsAsync([FromBody] AddMeasurementRequestDto request)
    {
        try 
        {
            var unit1 = ParseWeightUnit(request.Unit1);
            var unit2 = ParseWeightUnit(request.Unit2);
            var targetUnit = ParseWeightUnit(request.TargetUnit);
            var q1 = new Quantity<WeightUnit>(request.Value1, unit1);
            var q2 = new Quantity<WeightUnit>(request.Value2, unit2);
            var result = await _service.AddWeightsAsync(q1, q2, targetUnit);
            
            _logger.LogInformation("Weight addition completed with OperationType=Add - history saved");
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Weight addition failed");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("subtract-weights")]
    public async Task<IActionResult> SubtractWeightsAsync([FromBody] AddMeasurementRequestDto request)
    {
        try 
        {
            _logger.LogInformation("Weight subtraction request: {Value1} {Unit1} - {Value2} {Unit2} -> {TargetUnit}", request.Value1, request.Unit1, request.Value2, request.Unit2, request.TargetUnit);
            var unit1 = ParseWeightUnit(request.Unit1);
            var unit2 = ParseWeightUnit(request.Unit2);
            var targetUnit = ParseWeightUnit(request.TargetUnit);
            var q1 = new Quantity<WeightUnit>(request.Value1, unit1);
            var q2 = new Quantity<WeightUnit>(request.Value2, unit2);
            var result = q1.Subtract(q2, targetUnit);
            var record = new MeasurementRecord(0, request.Value1, request.Unit1, request.TargetUnit, result.Value, DateTime.Now, ModelLayer.Enum.MeasurementType.Weight, ModelLayer.Enum.OperationType.Subtract);
            await _repository.SaveMeasurementAsync(record);
            
            _logger.LogInformation("Weight subtraction completed with OperationType=Subtract - history saved");
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Weight subtraction failed");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("compare-weight")]
    public async Task<IActionResult> CompareWeightAsync([FromBody] CompareRequestDto request)
    {
        try 
        {
            var unit1 = ParseWeightUnit(request.Unit1);
            var unit2 = ParseWeightUnit(request.Unit2);
            var q1 = new Quantity<WeightUnit>(request.Value1, unit1);
            var q2 = new Quantity<WeightUnit>(request.Value2, unit2);
            var isEqual = q1.Equals(q2);
            var record = new MeasurementRecord(0, request.Value1, request.Unit1, request.Unit2, isEqual ? 1.0 : 0.0, DateTime.Now, ModelLayer.Enum.MeasurementType.Weight, ModelLayer.Enum.OperationType.Compare);
            await _repository.SaveMeasurementAsync(record);
            var message = isEqual ? "Equal" : (request.Value1 > request.Value2 ? "Value 1 > Value 2" : "Value 1 < Value 2");
            
            _logger.LogInformation("Weight compare completed with OperationType=Compare - history saved");
            return Ok(new CompareResultDto 
            { 
                IsEqual = isEqual,
                Message = message,
                Value1Base = request.Value1,
                Value2Base = request.Value2
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Weight compare failed");
            return BadRequest(ex.Message);
        }
    }

    private static WeightUnit ParseWeightUnit(string unitStr)
    {
        return unitStr.ToUpper() switch
        {
            "KILOGRAM" => WeightUnit.Kilogram,
            "GRAM" => WeightUnit.Gram,
            _ => throw new ArgumentException($"Unknown weight unit: {unitStr}")
        };
    }

    // Temperature endpoints (unchanged - no divide)
    [HttpPost("convert-temperature")]
    public async Task<IActionResult> ConvertTemperatureAsync([FromBody] MeasurementRequestDto request)
    {
        try 
        {
            var sourceUnit = ParseTemperatureUnit(request.SourceUnit);
            var targetUnit = ParseTemperatureUnit(request.TargetUnit);
            var quantity = new Quantity<TemperatureUnit>(request.Value, sourceUnit);
            var result = await _service.ConvertTemperatureAsync(quantity, targetUnit);
            
            _logger.LogInformation("Temperature conversion completed with OperationType=Convert - history saved");
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Temperature conversion failed");
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("compare-temperature")]
    public async Task<IActionResult> CompareTemperatureAsync([FromBody] CompareRequestDto request)
    {
        try 
        {
            var unit1 = ParseTemperatureUnit(request.Unit1);
            var unit2 = ParseTemperatureUnit(request.Unit2);
            var q1 = new Quantity<TemperatureUnit>(request.Value1, unit1);
            var q2 = new Quantity<TemperatureUnit>(request.Value2, unit2);
            var isEqual = q1.Equals(q2);
            var record = new MeasurementRecord(0, request.Value1, request.Unit1, request.Unit2, isEqual ? 1.0 : 0.0, DateTime.Now, ModelLayer.Enum.MeasurementType.Temperature, ModelLayer.Enum.OperationType.Compare);
            await _repository.SaveMeasurementAsync(record);
            var message = isEqual ? "Equal" : (request.Value1 > request.Value2 ? "Value 1 > Value 2" : "Value 1 < Value 2");
            
            _logger.LogInformation("Temperature compare completed with OperationType=Compare - history saved");
            return Ok(new CompareResultDto 
            { 
                IsEqual = isEqual,
                Message = message,
                Value1Base = request.Value1,
                Value2Base = request.Value2
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Temperature compare failed");
            return BadRequest(ex.Message);
        }
    }

    private static TemperatureUnit ParseTemperatureUnit(string unitStr)
    {
        return unitStr.ToUpper() switch
        {
            "CELSIUS" => TemperatureUnit.CELSIUS,
            "FAHRENHEIT" => TemperatureUnit.FAHRENHEIT,
            "KELVIN" => TemperatureUnit.KELVIN,
            _ => throw new ArgumentException($"Unknown temperature unit: {unitStr}")
        };
    }

    private static LengthUnit ParseLengthUnit(string unitStr)
    {
        return unitStr.ToUpper() switch
        {
            "FEET" => LengthUnit.Feet,
            "INCHES" => LengthUnit.Inches,
            "YARDS" => LengthUnit.Yards,
            "CENTIMETERS" => LengthUnit.Centimeters,
            _ => throw new ArgumentException($"Unknown length unit: {unitStr}")
        };
    }

    private static VolumeUnit ParseVolumeUnit(string unitStr)
    {
        return unitStr.ToUpper() switch
        {
            "LITRE" => VolumeUnit.Litre,
            "MILLILITRE" => VolumeUnit.Millilitre,
            "GALLON" => VolumeUnit.Gallon,
            _ => throw new ArgumentException($"Unknown volume unit: {unitStr}")
        };
    }
}
