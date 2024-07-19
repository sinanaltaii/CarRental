using CarRental.DataAccess;
using CarRental.Domains;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly CarRentalContext _context;

        public CreateCategoryCommandValidator(CarRentalContext context)
        {
            _context = context;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.BasePrice)
                .GreaterThan(0m).WithMessage("{PropertyName} is required");

            RuleFor(e => e)
                .MustAsync(CategoryExists).WithMessage("An category with same name already exists");
        }

        private async Task<bool> CategoryExists(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var categoryName = Enum.Parse<CategoryNameAndDiscriminator>(command.Name);
            var exists = !(await _context.Categories.AnyAsync(x => x.Name.Equals(categoryName), CancellationToken.None));
            return exists;
        }
    }
}
