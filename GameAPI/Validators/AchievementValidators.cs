using FluentValidation;
using GameAPI.DTOs;

namespace GameAPI.Validators
{
    public class CreateAchievementRequestValidator : AbstractValidator<CreateAchievementRequest>
    {
        public CreateAchievementRequestValidator()
        {
            RuleFor(x => x.Key)
                .NotEmpty().WithMessage("Klucz jest wymagany.")
                .MaximumLength(100).WithMessage("Klucz może mieć maksymalnie 100 znaków.")
                .Matches("^[a-zA-Z0-9_]+$").WithMessage("Klucz może zawierać tylko litery, cyfry i _");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa jest wymagana.")
                .MaximumLength(200).WithMessage("Nazwa może mieć maksymalnie 200 znaków.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Opis jest wymagany.")
                .MaximumLength(500).WithMessage("Opis może mieć maksymalnie 500 znaków.");

            RuleFor(x => x.Points)
                .GreaterThanOrEqualTo(0).WithMessage("Punkty nie mogą być ujemne.")
                .LessThanOrEqualTo(9999).WithMessage("Punkty przekraczają maksymalną wartość.");
        }
    }
}