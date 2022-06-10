using Costs.Application.Services.CostCategoryAppServices.Commands;
using MediatR;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.UpdateCostCategory
{
    public class UpdateCostCategoryCommandHandler :
        IRequestHandler<UpdateCostCategoryCommand, UpdateCostCategoryResponse>
    {
        private readonly ICostCategoryCommandService _costCategoryCommandService;

        public UpdateCostCategoryCommandHandler(ICostCategoryCommandService costCategoryCommandService)
        {
            _costCategoryCommandService = costCategoryCommandService;
        }

        public async Task<UpdateCostCategoryResponse> Handle(UpdateCostCategoryCommand request, CancellationToken cancellationToken)
        {
            return await _costCategoryCommandService.Update<UpdateCostCategoryResponse>(request, cancellationToken);
        }
    }
}
