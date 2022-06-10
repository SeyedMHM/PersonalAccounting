using Costs.Application.Features.CostCategoryFeatures.Queries.Common;
using MediatR;

namespace Costs.Application.Features.CostCategoryFeatures.Queries.GetAllCostCategories
{
    public class GetAllCostCategoriesQuery : IRequest<List<GetCostCategoryResponse>>
    {
    }
}
