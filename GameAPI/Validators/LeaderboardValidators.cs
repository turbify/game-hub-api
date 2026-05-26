using FluentValidation;
using GameAPI.DTOs;

namespace GameAPI.Validators
{
    public class AddScoreRequestValidator : AbstractValidator<AddScoreRequest>
    {
        public AddScoreRequestValidator()
        {
            RuleFor(x => x.Score)
                .GreaterThanOrEqualTo(0).WithMessage("The score cannot be negative.")
                .LessThanOrEqualTo(9999999).WithMessage("The score exceeds the maximum value.");

            RuleFor(x => x.Level)
                .GreaterThanOrEqualTo(1).WithMessage("The level must be greater than 0.")
                .LessThanOrEqualTo(9999).WithMessage("The level exceeds the maximum value.");
        }
    }
}