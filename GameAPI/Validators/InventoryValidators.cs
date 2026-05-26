using FluentValidation;
using GameAPI.DTOs;

namespace GameAPI.Validators
{
    public class AddItemRequestValidator : AbstractValidator<AddItemRequest>
    {
        public AddItemRequestValidator()
        {
            RuleFor(x => x.ItemKey)
                .NotEmpty().WithMessage("ItemKey is required.")
                .MaximumLength(100).WithMessage("An ItemKey can be up to 100 characters long.")
                .Matches("^[a-zA-Z0-9_]+$").WithMessage("ItemKey can only contain letters, numbers, and _");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("The quantity must be greater than 0.")
                .LessThanOrEqualTo(9999).WithMessage("The quantity exceeds the maximum limit.");
        }
    }

    public class UpdateItemRequestValidator : AbstractValidator<UpdateItemRequest>
    {
        public UpdateItemRequestValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("The quantity must be greater than 0.")
                .LessThanOrEqualTo(9999).WithMessage("The quantity exceeds the maximum limit.");
        }
    }
}