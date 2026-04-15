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
    private readonly IQuantityMeasurementRepository _repository;
    private readonly ILogger<MeasurementController> _logger;

    public MeasurementController(
        IQuantityMeasurementService service,
        IQuantityMeasurementRepository repository,
        ILogger<MeasurementController> logger)
    {
        _service = service;
        _repository = repository;
        _logger = logger;
    }

    // ─── LENGTH ───────────────────────────────────────────────

    [HttpPost("convert-length")]
    public async Task<IActionResult> ConvertLengthAsync([FromBody] MeasurementRequestDto request)
    {
        try
        {
            var quantity = new Quantity<LengthUnit>(request.Value, ParseLengthUnit(request.SourceUnit));
            var result = await _service.ConvertLengthAsync(quantity, ParseLengthUnit(request.TargetUnit));
            return Ok(new MeasurementResultDto { Value = result.Value, Unit = result.Unit.ToString() });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("add-lengths")]
    public async Task<IActionResult> AddLengthsAsync([FromBody] AddMeasurementRequestDto request)
    {
        try
        {
            var q1 = new Quantity<LengthUnit>(request.Value1, ParseLengthUnit(request.Unit1));
            var q2 = new Quantity<LengthUnit>(request.Value2, ParseLengthUnit(request.Unit2));
            var result = await _service.AddLengthsAsync(q1, q2, ParseLengthUnit(request.TargetUnit));
            return Ok(new MeasurementResultDto { Value = result.Value, Unit = result.Unit.ToString() });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("subtract-lengths")]
    public async Task<IActionResult> SubtractLengthsAsync([FromBody] AddMeasurementRequestDto request)
    {
        try
        {
            var q1 = new Quantity<LengthUnit>(request.Value1, ParseLengthUnit(request.Unit1));
            var q2 = new Quantity<LengthUnit>(request.Value2, ParseLengthUnit(request.Unit2));
            var result = await _service.SubtractLengthsAsync(q1, q2, ParseLengthUnit(request.TargetUnit));
            return Ok(new MeasurementResultDto { Value = result.Value, Unit = result.Unit.ToString() });
        }
        catch (Exception ex)
{
    return BadRequest(ex.ToString());
}
    }
    // redeploy compare length endpoint
    [HttpPost("compare-length")]
public async Task<IActionResult> CompareLengthAsync([FromBody] CompareRequestDto request)
{
    try
    {
        var q1 = new Quantity<LengthUnit>(
            request.Value1,
            ParseLengthUnit(request.Unit1));

        var q2 = new Quantity<LengthUnit>(
            request.Value2,
            ParseLengthUnit(request.Unit2));

        var result = await _service.AreLengthsEqualAsync(q1, q2);

        return Ok(new CompareResultDto
        {
            IsEqual = result,
            Message = result ? "Both values are equal" : "Values are not equal",
            Value1Base = q1.ConvertTo(LengthUnit.Feet).Value,
            Value2Base = q2.ConvertTo(LengthUnit.Feet).Value
        });
    }
    catch (Exception ex)
    {
        return BadRequest(ex.ToString());
    }
}
[HttpPost("divide-length")]
public async Task<IActionResult> DivideLengthAsync([FromBody] CompareRequestDto request)
{
    try
    {
        var q1 = new Quantity<LengthUnit>(
            request.Value1,
            ParseLengthUnit(request.Unit1));

        var q2 = new Quantity<LengthUnit>(
            request.Value2,
            ParseLengthUnit(request.Unit2));

        var result = await _service.DivideLengthsAsync(q1, q2);

        return Ok(new
        {
            value = result,
            unit = "ratio"
        });
    }
    catch (Exception ex)
    {
        return BadRequest(ex.ToString());
    }
}

    // ─── VOLUME ───────────────────────────────────────────────

    [HttpPost("convert-volume")]
    public async Task<IActionResult> ConvertVolumeAsync([FromBody] MeasurementRequestDto request)
    {
        try
        {
            var quantity = new Quantity<VolumeUnit>(request.Value, ParseVolumeUnit(request.SourceUnit));
            var result = await _service.ConvertVolumeAsync(quantity, ParseVolumeUnit(request.TargetUnit));
            return Ok(new MeasurementResultDto { Value = result.Value, Unit = result.Unit.ToString() });
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
            var q1 = new Quantity<VolumeUnit>(request.Value1, ParseVolumeUnit(request.Unit1));
            var q2 = new Quantity<VolumeUnit>(request.Value2, ParseVolumeUnit(request.Unit2));
            var result = await _service.AddVolumesAsync(q1, q2, ParseVolumeUnit(request.TargetUnit));
            return Ok(new MeasurementResultDto { Value = result.Value, Unit = result.Unit.ToString() });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("subtract-volumes")]
    public async Task<IActionResult> SubtractVolumesAsync([FromBody] AddMeasurementRequestDto request)
    {
        try
        {
            var q1 = new Quantity<VolumeUnit>(request.Value1, ParseVolumeUnit(request.Unit1));
            var q2 = new Quantity<VolumeUnit>(request.Value2, ParseVolumeUnit(request.Unit2));
            var result = await _service.SubtractVolumesAsync(q1, q2, ParseVolumeUnit(request.TargetUnit));
            return Ok(new MeasurementResultDto { Value = result.Value, Unit = result.Unit.ToString() });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // ─── WEIGHT ───────────────────────────────────────────────

    [HttpPost("convert-weight")]
    public async Task<IActionResult> ConvertWeightAsync([FromBody] MeasurementRequestDto request)
    {
        try
        {
            var quantity = new Quantity<WeightUnit>(request.Value, ParseWeightUnit(request.SourceUnit));
            var result = await _service.ConvertWeightAsync(quantity, ParseWeightUnit(request.TargetUnit));
            return Ok(new MeasurementResultDto { Value = result.Value, Unit = result.Unit.ToString() });
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
            var q1 = new Quantity<WeightUnit>(request.Value1, ParseWeightUnit(request.Unit1));
            var q2 = new Quantity<WeightUnit>(request.Value2, ParseWeightUnit(request.Unit2));
            var result = await _service.AddWeightsAsync(q1, q2, ParseWeightUnit(request.TargetUnit));
            return Ok(new MeasurementResultDto { Value = result.Value, Unit = result.Unit.ToString() });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("subtract-weights")]
    public async Task<IActionResult> SubtractWeightsAsync([FromBody] AddMeasurementRequestDto request)
    {
        try
        {
            var q1 = new Quantity<WeightUnit>(request.Value1, ParseWeightUnit(request.Unit1));
            var q2 = new Quantity<WeightUnit>(request.Value2, ParseWeightUnit(request.Unit2));
            var result = await _service.SubtractWeightsAsync(q1, q2, ParseWeightUnit(request.TargetUnit));
            return Ok(new MeasurementResultDto { Value = result.Value, Unit = result.Unit.ToString() });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("divide-volume")]
public async Task<IActionResult> DivideVolumeAsync([FromBody] CompareRequestDto request)
{
    try
    {
        var q1 = new Quantity<VolumeUnit>(
            request.Value1,
            ParseVolumeUnit(request.Unit1));

        var q2 = new Quantity<VolumeUnit>(
            request.Value2,
            ParseVolumeUnit(request.Unit2));

        var result = await _service.DivideVolumesAsync(q1, q2);

        return Ok(new
        {
            value = result,
            unit = "ratio"
        });
    }
    catch (Exception ex)
    {
        return BadRequest(ex.ToString());
    }
}
    [HttpPost("compare-weight")]
public async Task<IActionResult> CompareWeightAsync([FromBody] CompareRequestDto request)
{
    try
    {
        var q1 = new Quantity<WeightUnit>(
            request.Value1,
            ParseWeightUnit(request.Unit1));

        var q2 = new Quantity<WeightUnit>(
            request.Value2,
            ParseWeightUnit(request.Unit2));

        var result = await _service.AreWeightsEqualAsync(q1, q2);

        return Ok(new
        {
            isEqual = result,
            message = result
                ? "Both weights are equal"
                : "Weights are not equal"
        });
    }
    catch (Exception ex)
    {
        return BadRequest(ex.ToString());
    }
}
    [HttpPost("divide-weight")]
public async Task<IActionResult> DivideWeightAsync([FromBody] CompareRequestDto request)
{
    try
    {
        var q1 = new Quantity<WeightUnit>(
            request.Value1,
            ParseWeightUnit(request.Unit1));

        var q2 = new Quantity<WeightUnit>(
            request.Value2,
            ParseWeightUnit(request.Unit2));

        var result = await _service.DivideWeightsAsync(q1, q2);

        return Ok(new
        {
            value = result,
            unit = "ratio"
        });
    }
    catch (Exception ex)
    {
        return BadRequest(ex.ToString());
    }
}
[HttpPost("compare-volume")]
public async Task<IActionResult> CompareVolumeAsync([FromBody] CompareRequestDto request)
{
    try
    {
        var q1 = new Quantity<VolumeUnit>(
            request.Value1,
            ParseVolumeUnit(request.Unit1));

        var q2 = new Quantity<VolumeUnit>(
            request.Value2,
            ParseVolumeUnit(request.Unit2));

        var result = await _service.AreVolumesEqualAsync(q1, q2);

        return Ok(new
        {
            isEqual = result,
            message = result
                ? "Both volumes are equal"
                : "Volumes are not equal"
        });
    }
    catch (Exception ex)
    {
        return BadRequest(ex.ToString());
    }
}
    // ─── TEMPERATURE ──────────────────────────────────────────

    [HttpPost("convert-temperature")]
    public async Task<IActionResult> ConvertTemperatureAsync([FromBody] MeasurementRequestDto request)
    {
        try
        {
            var quantity = new Quantity<TemperatureUnit>(request.Value, ParseTemperatureUnit(request.SourceUnit));
            var result = await _service.ConvertTemperatureAsync(quantity, ParseTemperatureUnit(request.TargetUnit));
            return Ok(new MeasurementResultDto { Value = result.Value, Unit = result.Unit.ToString() });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // ─── PARSERS ──────────────────────────────────────────────

    private static LengthUnit ParseLengthUnit(string unitStr) => unitStr.ToUpper() switch
    {
        "FEET" => LengthUnit.Feet,
        "INCHES" => LengthUnit.Inches,
        "YARDS" => LengthUnit.Yards,
        "CENTIMETERS" => LengthUnit.Centimeters,
        _ => throw new ArgumentException($"Unknown length unit: {unitStr}")
    };

    private static VolumeUnit ParseVolumeUnit(string unitStr) => unitStr.ToUpper() switch
    {
        "LITRE" => VolumeUnit.Litre,
        "MILLILITRE" => VolumeUnit.Millilitre,
        "GALLON" => VolumeUnit.Gallon,
        _ => throw new ArgumentException($"Unknown volume unit: {unitStr}")
    };

    private static WeightUnit ParseWeightUnit(string unitStr) => unitStr.ToUpper() switch
    {
        "KILOGRAM" => WeightUnit.Kilogram,
        "GRAM" => WeightUnit.Gram,
        _ => throw new ArgumentException($"Unknown weight unit: {unitStr}")
    };

    private static TemperatureUnit ParseTemperatureUnit(string unitStr) => unitStr.ToUpper() switch
    {
        "CELSIUS" => TemperatureUnit.CELSIUS,
        "FAHRENHEIT" => TemperatureUnit.FAHRENHEIT,
        "KELVIN" => TemperatureUnit.KELVIN,
        _ => throw new ArgumentException($"Unknown temperature unit: {unitStr}")
    };
}