using FluentValidation;

namespace CarRental.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(p => p.CategoryId)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

            RuleFor(p => p.BasePrice)
                .GreaterThan(0m).WithMessage("{PropertyName} is required");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        }
    }
}
