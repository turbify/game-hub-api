using FluentValidation;
using GameAPI.DTOs;

namespace GameAPI.Validators
{
    public class AddItemRequestValidator : AbstractValidator<AddItemRequest>
    {
        public AddItemRequestValidator()
        {
            RuleFor(x => x.ItemKey)
                .NotEmpty().WithMessage("ItemKey jest wymagany.")
                .MaximumLength(100).WithMessage("ItemKey może mieć maksymalnie 100 znaków.")
                .Matches("^[a-zA-Z0-9_]+$").WithMessage("ItemKey może zawierać tylko litery, cyfry i _");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Ilość musi być większa od 0.")
                .LessThanOrEqualTo(9999).WithMessage("Ilość przekracza maksymalną wartość.");
        }
    }

    public class UpdateItemRequestValidator : AbstractValidator<UpdateItemRequest>
    {
        public UpdateItemRequestValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Ilość musi być większa od 0.")
                .LessThanOrEqualTo(9999).WithMessage("Ilość przekracza maksymalną wartość.");
        }
    }
}