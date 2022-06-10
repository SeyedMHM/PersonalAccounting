using Costs.Application.Features.CostCategoryFeatures.Queries.Common;
using Costs.Application.Services.CostCategoryAppServices.Queries;
using MediatR;

namespace Costs.Application.Features.CostCategoryFeatures.Queries.GetCostCategoryById
{
    public class GetCostCategoryByIdQueryHandler : IRequestHandler<GetCostCategoryByIdQuery, GetCostCategoryResponse>
    {
        private readonly ICostCategoryQueryService _costCategoryQueryService;

        public GetCostCategoryByIdQueryHandler(ICostCategoryQueryService costCategoryQueryService)
        {
            _costCategoryQueryService = costCategoryQueryService;
        }

        public async Task<GetCostCategoryResponse> Handle(GetCostCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _costCategoryQueryService
                .GetByIdWithIncludeProperties<GetCostCategoryResponse>(request.Id, "Parent,Children", cancellationToken);
        }
    }
}
