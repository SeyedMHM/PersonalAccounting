using Costs.Application.Common.Models;
using Costs.Application.Features.CostCategoryFeatures.Queries.Common;
using MediatR;

namespace Costs.Application.Features.CostCategoryFeatures.Queries.GetPagedCostCategories
{
    public class GetPagedCostCategoriesQuery : PagedListMetadata, IRequest<PagedList<GetCostCategoryResponse>>
    {
        public string? Title { get; set; }
        public int? ParentId { get; set; }
    }
}