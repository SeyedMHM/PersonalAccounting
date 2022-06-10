using Costs.Application.Common.Models;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.CreateCostCategory
{
    public class CreateCostCategoryResponse : BaseDto
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string? ParentTitle { get; set; }
        public string? Description { get; set; }

        public CreateCostCategoryResponse? Parent { get; set; }
        public ICollection<CreateCostCategoryResponse>? Children { get; set; }
    }
}
