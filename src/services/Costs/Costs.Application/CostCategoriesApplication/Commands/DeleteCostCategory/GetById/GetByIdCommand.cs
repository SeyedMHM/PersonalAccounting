using MediatR;

namespace Costs.Application.CostCategoriesApplication.Commands.DeleteCostCategory.GetById
{
    public class GetByIdCommand : IRequest<GetByIdResponse>
    {
        public int Id { get; set; }
    }
}
