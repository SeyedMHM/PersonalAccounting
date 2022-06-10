using FluentValidation;
using Costs.Application.Services.CostCategoryAppServices.Queries;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.UpdateCostCategory
{
    public class UpdateCostCategoryCommandValidator : AbstractValidator<UpdateCostCategoryCommand>
    {
        private readonly ICostCategoryValidatorService _costCategoryValidatorService;

        public UpdateCostCategoryCommandValidator(ICostCategoryValidatorService costCategoryValidatorService)
        {
            _costCategoryValidatorService = costCategoryValidatorService;

            RuleFor(category => category.Title).NotEmpty();

            RuleFor(category => category.Description).MaximumLength(500);

            RuleFor(category => category.ParentId)
                .GreaterThan(0)
                .NotEqual(q => q.Id).WithMessage("'{PropertyName}' must not be equal to 'Id'.");

            RuleFor(category => category)
                .MustAsync(async (category, cancellationToken) =>
                !await _costCategoryValidatorService.IsExistTitle(category.Id, category.Title, cancellationToken))
                .WithMessage("'Title' is exist. You have to change category Title");

            RuleFor(category => category)
                .MustAsync(async (category, cancellationToken) =>
                    await _costCategoryValidatorService.IsValidParentId(category.ParentId, cancellationToken))
                .WithMessage("'ParentId' isn't exist. You should to change 'ParentId'");
        }
    }
}
