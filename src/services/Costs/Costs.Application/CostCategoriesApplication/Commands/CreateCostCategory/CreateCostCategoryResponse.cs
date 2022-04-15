namespace Costs.Application.CostCategoriesApplication.Commands.CreateCostCategory
{
    public class CreateCostCategoryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string? ParentTitle { get; set; }
        public string? Description { get; set; }

        public CreateCostCategoryResponse? Parent { get; set; }
        public ICollection<CreateCostCategoryResponse>? Children { get; set; }
    }
}
