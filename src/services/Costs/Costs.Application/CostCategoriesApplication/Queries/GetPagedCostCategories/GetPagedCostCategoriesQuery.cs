using Costs.Application.Common.Models;
using Costs.Application.CostCategoriesApplication.Queries.Common;
using MediatR;

namespace Costs.Application.CostCategoriesApplication.Queries.GetPagedCostCategories
{
    public class GetPagedCostCategoriesQuery : PagedListMetadata, IRequest<PagedList<GetCostCategoryResponse>>
    {
        public string? Title { get; set; }
        public int? ParentId { get; set; }
    }
}