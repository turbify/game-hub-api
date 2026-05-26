using FluentValidation;
using GameAPI.DTOs;

namespace GameAPI.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username jest wymagany.")
                .MinimumLength(3).WithMessage("Username musi mieć minimum 3 znaki.")
                .MaximumLength(50).WithMessage("Username może mieć maksymalnie 50 znaków.")
                .Matches("^[a-zA-Z0-9_]+$").WithMessage("Username może zawierać tylko litery, cyfry i _");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email jest wymagany.")
                .EmailAddress().WithMessage("Nieprawidłowy format email.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Hasło jest wymagane.")
                .MinimumLength(8).WithMessage("Hasło musi mieć minimum 8 znaków.")
                .Matches("[A-Z]").WithMessage("Hasło musi zawierać przynajmniej jedną wielką literę.")
                .Matches("[0-9]").WithMessage("Hasło musi zawierać przynajmniej jedną cyfrę.");
        }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username jest wymagany.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Hasło jest wymagane.");
        }
    }
}