using Costs.Application.CostCategoriesApplication.Queries.Common;
using MediatR;

namespace Costs.Application.CostCategoriesApplication.Queries.GetCostCategoryById
{
    public class GetCostCategoryByIdQuery : IRequest<GetCostCategoryResponse>
    {
        public int Id { get; set; }
    }
}
