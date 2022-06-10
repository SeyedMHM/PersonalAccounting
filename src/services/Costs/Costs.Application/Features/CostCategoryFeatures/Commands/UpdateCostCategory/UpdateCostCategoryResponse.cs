using Costs.Application.Common.Models;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.UpdateCostCategory
{
    public class UpdateCostCategoryResponse : BaseDto
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string? ParentTitle { get; set; }
        public string? Description { get; set; }

        public UpdateCostCategoryResponse? Parent { get; set; }
        public ICollection<UpdateCostCategoryResponse>? Children { get; set; }
    }
}
