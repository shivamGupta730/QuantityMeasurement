using FluentValidation;
using QuantityMeasurementAPI.DTOs;

namespace QuantityMeasurementAPI.Validators
{
    public class AddMeasurementRequestDtoValidator : AbstractValidator<AddMeasurementRequestDto>
    {
        public AddMeasurementRequestDtoValidator()
        {
            RuleFor(x => x.Value1)
                .GreaterThan(0).WithMessage("Value1 must be greater than 0");

            RuleFor(x => x.Value2)
                .GreaterThan(0).WithMessage("Value2 must be greater than 0");

            RuleFor(x => x.Unit1)
                .NotEmpty().WithMessage("Unit1 is required")
                .MaximumLength(50).WithMessage("Unit1 cannot exceed 50 characters");

            RuleFor(x => x.Unit2)
                .NotEmpty().WithMessage("Unit2 is required")
                .MaximumLength(50).WithMessage("Unit2 cannot exceed 50 characters");

            RuleFor(x => x.TargetUnit)
                .NotEmpty().WithMessage("TargetUnit is required")
                .MaximumLength(50).WithMessage("TargetUnit cannot exceed 50 characters");
        }
    }
}
