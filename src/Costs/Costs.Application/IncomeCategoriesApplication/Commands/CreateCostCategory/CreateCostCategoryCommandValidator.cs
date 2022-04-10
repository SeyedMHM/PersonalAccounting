using FluentValidation;
using Costs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Costs.Application.CostCategoriesApplication.Commands.CreateCostCategory
{
    public class CreateCostCategoryCommandValidator : AbstractValidator<CreateCostCategoryCommand>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CreateCostCategoryCommandValidator(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

            RuleFor(category => category.Title).NotEmpty();

            RuleFor(category => category.Description).MaximumLength(500);

            RuleFor(category => category)
                .MustAsync(async (category, cancellationToken) => 
                !await TitleIsExist(category.Title, cancellationToken))
                .WithMessage("'Title' is exist. You have to change category Title");

            RuleFor(category => category)
                .MustAsync(async (category, cancellationToken) =>
                    await ParentIsExist(category.ParentId, cancellationToken))
                .WithMessage("'ParentId' isn't exist. You have to change 'ParentId'");
        }

        private async Task<bool> TitleIsExist(string title, CancellationToken cancellationToken)
        {
            return 
                await _applicationDbContext.CostCategories
                .Where(q => q.Title == title)
                .AnyAsync(cancellationToken);
        }

        private async Task<bool> ParentIsExist(int? parentId, CancellationToken cancellationToken)
        {
            return parentId is null ? true : await _applicationDbContext.CostCategories
                .Where(q => q.Id == parentId)
                .AnyAsync(cancellationToken);
        }
    }
}
