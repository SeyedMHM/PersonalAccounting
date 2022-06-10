using FluentValidation;
using Costs.Application.Services.CostCategoryAppServices.Queries;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.CreateCostCategory
{
    public class CreateCostCategoryCommandValidator : AbstractValidator<CreateCostCategoryCommand>
    {
        private readonly ICostCategoryValidatorService _costCategoryValidatorService;

        public CreateCostCategoryCommandValidator(ICostCategoryValidatorService costCategoryValidatorService)
        {
            _costCategoryValidatorService = costCategoryValidatorService;

            RuleFor(category => category.Title).NotEmpty();

            RuleFor(category => category.Description).MaximumLength(500);

            RuleFor(category => category)
                .MustAsync(async (category, cancellationToken) => 
                !await _costCategoryValidatorService.IsExistTitle(category.Title, cancellationToken))
                .WithMessage("'Title' is exist. You have to change category Title");

            RuleFor(category => category)
                .MustAsync(async (category, cancellationToken) =>
                    await _costCategoryValidatorService.IsValidParentId(category.ParentId, cancellationToken))
                .WithMessage("'ParentId' isn't exist. You should to change 'ParentId'");
        }
    }
}
