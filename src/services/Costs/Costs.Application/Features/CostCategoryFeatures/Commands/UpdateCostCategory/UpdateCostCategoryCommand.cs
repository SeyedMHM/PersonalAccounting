using Costs.Application.Common.Models;
using MediatR;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.UpdateCostCategory
{
    public class UpdateCostCategoryCommand : BaseDto, IRequest<UpdateCostCategoryResponse>
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string? Description { get; set; }
    }
}
