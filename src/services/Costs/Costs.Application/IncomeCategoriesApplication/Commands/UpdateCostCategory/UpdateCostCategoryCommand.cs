using MediatR;

namespace Costs.Application.CostCategoriesApplication.Commands.UpdateCostCategory
{
    public class UpdateCostCategoryCommand : IRequest<UpdateCostCategoryResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string? Description { get; set; }
    }
}
