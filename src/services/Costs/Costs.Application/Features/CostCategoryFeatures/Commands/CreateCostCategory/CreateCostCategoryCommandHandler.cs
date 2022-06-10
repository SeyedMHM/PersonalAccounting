using Costs.Application.Services.CostCategoryAppServices.Commands;
using MediatR;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.CreateCostCategory
{
    public class CreateCostCategoryCommandHandler :
        IRequestHandler<CreateCostCategoryCommand, CreateCostCategoryResponse>
    {
        private readonly ICostCategoryCommandService _costCategoryCommandService;

        public CreateCostCategoryCommandHandler(ICostCategoryCommandService costCategoryCommandService)
        {
            _costCategoryCommandService = costCategoryCommandService;
        }

        public async Task<CreateCostCategoryResponse> Handle(CreateCostCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _costCategoryCommandService.Create<CreateCostCategoryCommand, CreateCostCategoryResponse>(request, cancellationToken);
        }
    }
}