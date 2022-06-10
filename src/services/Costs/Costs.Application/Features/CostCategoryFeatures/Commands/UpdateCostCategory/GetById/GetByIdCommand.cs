using Costs.Application.Common.Models;
using MediatR;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.UpdateCostCategory.GetById
{
    public class GetByIdCommand : BaseDto, IRequest<GetByIdResponse>
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string? Description { get; set; }
    }
}
