using Costs.Application.Features.CostCategoryFeatures.Queries.Common;
using Costs.Application.Services.CostCategoryAppServices.Queries;
using MediatR;

namespace Costs.Application.Features.CostCategoryFeatures.Queries.GetAllCostCategories
{
    public class GetAllCostCategoriesQueryHandler : IRequestHandler<GetAllCostCategoriesQuery, List<GetCostCategoryResponse>>
    {
        private readonly ICostCategoryQueryService _costCategoryQueryService;

        public GetAllCostCategoriesQueryHandler(ICostCategoryQueryService costCategoryQueryService)
        {
            _costCategoryQueryService = costCategoryQueryService;
        }

        public async Task<List<GetCostCategoryResponse>> Handle(GetAllCostCategoriesQuery request, CancellationToken cancellationToken)
        {
            List<GetCostCategoryResponse> result = await _costCategoryQueryService
                .GetAllWithIncludeProperties<GetCostCategoryResponse>("Parent, Children", cancellationToken);

            return result;
        }
    }
}