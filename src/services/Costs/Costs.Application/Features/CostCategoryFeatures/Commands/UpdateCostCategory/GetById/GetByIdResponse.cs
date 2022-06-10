using Costs.Application.Common.Models;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.UpdateCostCategory.GetById
{
    public class GetByIdResponse : BaseDto
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string? Description { get; set; }
    }
}