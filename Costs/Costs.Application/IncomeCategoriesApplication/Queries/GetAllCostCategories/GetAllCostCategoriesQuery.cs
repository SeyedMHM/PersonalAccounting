using Costs.Application.CostCategoriesApplication.Queries.Common;
using MediatR;

namespace Costs.Application.CostCategoriesApplication.Queries.GetAllCostCategories
{
    public class GetAllCostCategoriesQuery : IRequest<List<GetCostCategoryResponse>>
    {
    }
}
