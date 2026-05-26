using FluentValidation;
using GameAPI.DTOs;

namespace GameAPI.Validators
{
    public class AddScoreRequestValidator : AbstractValidator<AddScoreRequest>
    {
        public AddScoreRequestValidator()
        {
            RuleFor(x => x.Score)
                .GreaterThanOrEqualTo(0).WithMessage("Score nie może być ujemny.")
                .LessThanOrEqualTo(9999999).WithMessage("Score przekracza maksymalną wartość.");

            RuleFor(x => x.Level)
                .GreaterThanOrEqualTo(1).WithMessage("Level musi być większy od 0.")
                .LessThanOrEqualTo(9999).WithMessage("Level przekracza maksymalną wartość.");
        }
    }
}