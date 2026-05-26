using FluentValidation;
using GameAPI.DTOs;

namespace GameAPI.Validators
{
    public class SaveGameRequestValidator : AbstractValidator<SaveGameRequest>
    {
        public SaveGameRequestValidator()
        {
            RuleFor(x => x.SaveData)
                .NotEmpty().WithMessage("Dane zapisu są wymagane.")
                .MaximumLength(100000).WithMessage("Dane zapisu są zbyt duże.");

            RuleFor(x => x.CurrentLevel)
                .GreaterThanOrEqualTo(1).WithMessage("Level musi być większy od 0.")
                .LessThanOrEqualTo(9999).WithMessage("Level przekracza maksymalną wartość.");

            RuleFor(x => x.CurrentScore)
                .GreaterThanOrEqualTo(0).WithMessage("Score nie może być ujemny.");
        }
    }
}