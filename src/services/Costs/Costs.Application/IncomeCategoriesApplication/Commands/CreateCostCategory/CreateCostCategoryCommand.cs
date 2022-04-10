using MediatR;

namespace Costs.Application.CostCategoriesApplication.Commands.CreateCostCategory
{
    public class CreateCostCategoryCommand : IRequest<CreateCostCategoryResponse>
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string? Description { get; set; }
    }
}
