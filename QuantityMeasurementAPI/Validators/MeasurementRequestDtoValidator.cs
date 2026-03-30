using FluentValidation;
using QuantityMeasurementAPI.DTOs;

namespace QuantityMeasurementAPI.Validators
{
    public class MeasurementRequestDtoValidator : AbstractValidator<MeasurementRequestDto>
    {
        public MeasurementRequestDtoValidator()
        {
            RuleFor(x => x.Value)
                .GreaterThan(0).WithMessage("Value must be greater than 0");

            RuleFor(x => x.SourceUnit)
                .NotEmpty().WithMessage("SourceUnit is required")
                .MaximumLength(50).WithMessage("SourceUnit cannot exceed 50 characters");

            RuleFor(x => x.TargetUnit)
                .NotEmpty().WithMessage("TargetUnit is required")
                .MaximumLength(50).WithMessage("TargetUnit cannot exceed 50 characters")
                .NotEqual(x => x.SourceUnit).WithMessage("SourceUnit and TargetUnit must be different");
        }
    }
}
