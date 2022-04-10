using FluentValidation;
using Costs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Costs.Application.CostCategoriesApplication.Commands.UpdateCostCategory
{
    public class UpdateCostCategoryCommandValidator : AbstractValidator<UpdateCostCategoryCommand>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UpdateCostCategoryCommandValidator(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

            RuleFor(category => category.Title).NotEmpty();
            RuleFor(category => category.Description).MaximumLength(500);
            RuleFor(category => category.ParentId)
                .GreaterThan(0)
                .NotEqual(q => q.Id).WithMessage("'{PropertyName}' must not be equal to 'Id'.");

            RuleFor(category => category)
                .MustAsync(async (category, cancellationToken) =>
                !await TitleIsExist(category.Id, category.Title, cancellationToken))
                .WithMessage("'Title' is exist. You have to change category Title");

            RuleFor(category => category)
                .MustAsync(async (category, cancellationToken) =>
                    await ParentIsExist(category.ParentId, cancellationToken))
                .WithMessage("'ParentId' isn't exist. You have to change 'ParentId'");
        }


        private async Task<bool> TitleIsExist(int id, string title, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.CostCategories
                .Where(q => q.Id != id && q.Title == title)
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
