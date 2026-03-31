using FluentValidation;
using QuantityMeasurementAPI.DTOs;

namespace QuantityMeasurementAPI.Validators
{
    public class GoogleLoginDtoValidator : AbstractValidator<GoogleLoginDto>
    {
        public GoogleLoginDtoValidator()
        {
            RuleFor(x => x.IdToken)
                .NotEmpty().WithMessage("ID token is required")
                .Length(100, 2000).WithMessage("ID token length must be between 100 and 2000 characters");
        }
    }
}
