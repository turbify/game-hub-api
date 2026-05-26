using FluentValidation;
using GameAPI.DTOs;

namespace GameAPI.Validators
{
    public class SaveGameRequestValidator : AbstractValidator<SaveGameRequest>
    {
        public SaveGameRequestValidator()
        {
            RuleFor(x => x.SaveData)
                .NotEmpty().WithMessage("This information is required.")
                .MaximumLength(100000).WithMessage("The log data is too large.");

            RuleFor(x => x.CurrentLevel)
                .GreaterThanOrEqualTo(1).WithMessage("The level must be greater than 0.")
                .LessThanOrEqualTo(9999).WithMessage("The level exceeds the maximum value.");

            RuleFor(x => x.CurrentScore)
                .GreaterThanOrEqualTo(0).WithMessage("The score cannot be negative.");
        }
    }
}