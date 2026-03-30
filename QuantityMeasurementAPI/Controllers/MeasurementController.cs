using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services;
using ModelLayer.Entity;
// Units are directly in ModelLayer namespace
// using ModelLayer.Enum.LengthUnit; etc. not needed
using ModelLayer;
using QuantityMeasurementAPI.DTOs;

namespace QuantityMeasurementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Microsoft.AspNetCore.Authorization.Authorize]
public class MeasurementController : ControllerBase
{
    private readonly IQuantityMeasurementService _service;
    private readonly ILogger<MeasurementController> _logger;

    public MeasurementController(IQuantityMeasurementService service, ILogger<MeasurementController> logger)
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
            
            _logger.LogInformation("Length conversion request completed successfully");
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Length conversion request failed: {Value} {SourceUnit} -> {TargetUnit}", request.Value, request.SourceUnit, request.TargetUnit);
            return BadRequest(ex.Message);
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
            
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Volume endpoints
[HttpPost("convert-volume")]
    public async Task<IActionResult> ConvertVolumeAsync([FromBody] MeasurementRequestDto request)
    {
        try 
        {
            var sourceUnit = ParseVolumeUnit(request.SourceUnit);
            var targetUnit = ParseVolumeUnit(request.TargetUnit);
            var quantity = new Quantity<VolumeUnit>(request.Value, sourceUnit);
            var result = await _service.ConvertVolumeAsync(quantity, targetUnit);
            
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

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
            
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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

    // Weight endpoints
[HttpPost("convert-weight")]
    public async Task<IActionResult> ConvertWeightAsync([FromBody] MeasurementRequestDto request)
    {
        try 
        {
            var sourceUnit = ParseWeightUnit(request.SourceUnit);
            var targetUnit = ParseWeightUnit(request.TargetUnit);
            var quantity = new Quantity<WeightUnit>(request.Value, sourceUnit);
            var result = await _service.ConvertWeightAsync(quantity, targetUnit);
            
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
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
            
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
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

    // Subtract endpoints (reuse add logic with negative value2)
[HttpPost("subtract-lengths")]
    public async Task<IActionResult> SubtractLengthsAsync([FromBody] AddMeasurementRequestDto request)
    {
        request.Value2 *= -1;
        return await AddLengthsAsync(request);
    }

[HttpPost("subtract-volumes")]
    public async Task<IActionResult> SubtractVolumesAsync([FromBody] AddMeasurementRequestDto request)
    {
        request.Value2 *= -1;
        return await AddVolumesAsync(request);
    }

[HttpPost("subtract-weights")]
    public async Task<IActionResult> SubtractWeightsAsync([FromBody] AddMeasurementRequestDto request)
    {
        request.Value2 *= -1;
        return await AddWeightsAsync(request);
    }

    // Compare endpoints
    [HttpPost("compare-length")]
    public IActionResult CompareLength([FromBody] CompareRequestDto request)
    {
        try 
        {
            var unit1 = ParseLengthUnit(request.Unit1);
            var unit2 = ParseLengthUnit(request.Unit2);
            var q1 = new Quantity<LengthUnit>(request.Value1, unit1);
            var q2 = new Quantity<LengthUnit>(request.Value2, unit2);
            bool isEqual = q1.Equals(q2);
            string message = isEqual ? "Equal" : (q1.Value > q2.Value ? "Value 1 > Value 2" : "Value 1 < Value 2");
            
            return Ok(new CompareResultDto 
            { 
                IsEqual = isEqual,
                Message = message 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("compare-volume")]
    public IActionResult CompareVolume([FromBody] CompareRequestDto request)
    {
        try 
        {
            var unit1 = ParseVolumeUnit(request.Unit1);
            var unit2 = ParseVolumeUnit(request.Unit2);
            var q1 = new Quantity<VolumeUnit>(request.Value1, unit1);
            var q2 = new Quantity<VolumeUnit>(request.Value2, unit2);
            bool isEqual = q1.Equals(q2);
            string message = isEqual ? "Equal" : (q1.Value > q2.Value ? "Value 1 > Value 2" : "Value 1 < Value 2");
            
            return Ok(new CompareResultDto 
            { 
                IsEqual = isEqual,
                Message = message 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("compare-weight")]
    public IActionResult CompareWeight([FromBody] CompareRequestDto request)
    {
        try 
        {
            var unit1 = ParseWeightUnit(request.Unit1);
            var unit2 = ParseWeightUnit(request.Unit2);
            var q1 = new Quantity<WeightUnit>(request.Value1, unit1);
            var q2 = new Quantity<WeightUnit>(request.Value2, unit2);
            bool isEqual = q1.Equals(q2);
            string message = isEqual ? "Equal" : (q1.Value > q2.Value ? "Value 1 > Value 2" : "Value 1 < Value 2");
            
            return Ok(new CompareResultDto 
            { 
                IsEqual = isEqual,
                Message = message 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("compare-temperature")]
    public IActionResult CompareTemperature([FromBody] CompareRequestDto request)
    {
        try 
        {
            var unit1 = ParseTemperatureUnit(request.Unit1);
            var unit2 = ParseTemperatureUnit(request.Unit2);
            var q1 = new Quantity<TemperatureUnit>(request.Value1, unit1);
            var q2 = new Quantity<TemperatureUnit>(request.Value2, unit2);
            bool isEqual = q1.Equals(q2);
            string message = isEqual ? "Equal" : (q1.Value > q2.Value ? "Value 1 > Value 2" : "Value 1 < Value 2");
            
            return Ok(new CompareResultDto 
            { 
                IsEqual = isEqual,
                Message = message 
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Temperature endpoints
[HttpPost("convert-temperature")]
    public async Task<IActionResult> ConvertTemperatureAsync([FromBody] MeasurementRequestDto request)
    {
        try 
        {
            var sourceUnit = ParseTemperatureUnit(request.SourceUnit);
            var targetUnit = ParseTemperatureUnit(request.TargetUnit);
            var quantity = new Quantity<TemperatureUnit>(request.Value, sourceUnit);
            var result = await _service.ConvertTemperatureAsync(quantity, targetUnit);
            
            return Ok(new MeasurementResultDto 
            { 
                Value = result.Value, 
                Unit = result.Unit.ToString() 
            });
        }
        catch (Exception ex)
        {
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
}
