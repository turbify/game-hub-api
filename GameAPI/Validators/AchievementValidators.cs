using FluentValidation;
using GameAPI.DTOs;

namespace GameAPI.Validators
{
    public class CreateAchievementRequestValidator : AbstractValidator<CreateAchievementRequest>
    {
        public CreateAchievementRequestValidator()
        {
            RuleFor(x => x.Key)
                .NotEmpty().WithMessage("A key is required.")
                .MaximumLength(100).WithMessage("The key can be up to 100 characters long.")
                .Matches("^[a-zA-Z0-9_]+$").WithMessage("The key can only contain letters, numbers, and _");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("A name is required.")
                .MaximumLength(200).WithMessage("The name can be up to 200 characters long.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A description is required.")
                .MaximumLength(500).WithMessage("The description can be up to 500 characters long.");

            RuleFor(x => x.Points)
                .GreaterThanOrEqualTo(0).WithMessage("Scores cannot be negative.")
                .LessThanOrEqualTo(9999).WithMessage("The scores exceed the maximum value.");
        }
    }
}