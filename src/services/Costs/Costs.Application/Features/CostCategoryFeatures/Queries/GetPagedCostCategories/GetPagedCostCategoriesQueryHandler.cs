using Costs.Application.Common.Models;
using Costs.Application.Features.CostCategoryFeatures.Queries.Common;
using Costs.Application.Services.CostCategoryAppServices.Queries;
using MediatR;

namespace Costs.Application.Features.CostCategoryFeatures.Queries.GetPagedCostCategories
{
    public class GetPagedCostCategoriesQueryHandler
        : IRequestHandler<GetPagedCostCategoriesQuery, PagedList<GetCostCategoryResponse>>
    {
        private readonly ICostCategoryQueryService _costCategoryQueryService;

        public GetPagedCostCategoriesQueryHandler(ICostCategoryQueryService costCategoryQueryService)
        {
            _costCategoryQueryService = costCategoryQueryService;
        }

        public async Task<PagedList<GetCostCategoryResponse>> Handle(GetPagedCostCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _costCategoryQueryService.Search<GetCostCategoryResponse>(request, cancellationToken);
        }
    }
}