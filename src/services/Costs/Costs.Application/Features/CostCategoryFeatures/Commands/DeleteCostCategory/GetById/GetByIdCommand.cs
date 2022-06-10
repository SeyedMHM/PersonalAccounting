using Costs.Application.Common.Models;
using MediatR;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.DeleteCostCategory.GetById
{
    public class GetByIdCommand : BaseDto, IRequest<GetByIdResponse>
    {
    }
}
