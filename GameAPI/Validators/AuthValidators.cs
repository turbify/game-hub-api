using FluentValidation;
using GameAPI.DTOs;

namespace GameAPI.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("A username is required.")
                .MinimumLength(3).WithMessage("The username must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("A username can be up to 50 characters long.")
                .Matches("^[a-zA-Z0-9_]+$").WithMessage("A username can only contain letters, numbers, and _");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("An email address is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A password is required.")
                .MinimumLength(8).WithMessage("The password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("The password must contain at least one uppercase letter.")
                .Matches("[0-9]").WithMessage("The password must contain at least one number.");
        }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("A username is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A password is required.");
        }
    }
}