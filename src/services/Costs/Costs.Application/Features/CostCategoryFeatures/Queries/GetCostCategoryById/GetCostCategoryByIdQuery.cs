using Costs.Application.Features.CostCategoryFeatures.Queries.Common;
using MediatR;

namespace Costs.Application.Features.CostCategoryFeatures.Queries.GetCostCategoryById
{
    public class GetCostCategoryByIdQuery : IRequest<GetCostCategoryResponse>
    {
        public int Id { get; set; }
    }
}
