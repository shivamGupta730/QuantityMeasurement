using FluentValidation;
using QuantityMeasurementAPI.DTOs;

namespace QuantityMeasurementAPI.Validators
{
    public class CompareRequestDtoValidator : AbstractValidator<CompareRequestDto>
    {
        public CompareRequestDtoValidator()
        {
            RuleFor(x => x.Value1)
                .GreaterThanOrEqualTo(0).WithMessage("Value1 must be 0 or greater");

            RuleFor(x => x.Value2)
                .GreaterThanOrEqualTo(0).WithMessage("Value2 must be 0 or greater");

            RuleFor(x => x.Unit1)
                .NotEmpty().WithMessage("Unit1 is required")
                .MaximumLength(50).WithMessage("Unit1 cannot exceed 50 characters");

            RuleFor(x => x.Unit2)
                .NotEmpty().WithMessage("Unit2 is required")
                .MaximumLength(50).WithMessage("Unit2 cannot exceed 50 characters");
        }
    }
}
