using MediatR;

namespace Costs.Application.CostCategoriesApplication.Commands.DeleteCostCategory
{
    public class DeleteCostCategoryCommand : IRequest<Unit>
    {
        public DeleteCostCategoryCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
