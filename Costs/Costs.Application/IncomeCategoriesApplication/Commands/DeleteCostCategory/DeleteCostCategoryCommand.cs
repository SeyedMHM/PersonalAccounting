using MediatR;

namespace Costs.Application.CostCategoriesApplication.Commands.DeleteCostCategory
{
    public class DeleteCostCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}