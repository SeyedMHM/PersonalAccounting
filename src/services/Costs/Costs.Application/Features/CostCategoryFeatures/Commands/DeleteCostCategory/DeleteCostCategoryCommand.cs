using Costs.Application.Common.Models;
using MediatR;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.DeleteCostCategory
{
    public class DeleteCostCategoryCommand : BaseDto, IRequest<Unit>
    {
        public DeleteCostCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
