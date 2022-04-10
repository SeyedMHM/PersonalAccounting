namespace Costs.Application.CostCategoriesApplication.Commands.UpdateCostCategory
{
    public class UpdateCostCategoryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string? ParentTitle { get; set; }
        public string? Description { get; set; }

        public UpdateCostCategoryResponse? Parent { get; set; }
        public ICollection<UpdateCostCategoryResponse>? Children { get; set; }
    }
}
