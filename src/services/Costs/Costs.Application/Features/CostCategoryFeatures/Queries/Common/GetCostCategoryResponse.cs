namespace Costs.Application.Features.CostCategoryFeatures.Queries.Common
{
    public class GetCostCategoryResponse 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string ParentTitle { get; set; }
        public string Description { get; set; }

        public GetCostCategoryResponse? Parent { get; set; }
        public ICollection<GetCostCategoryResponse>? Children { get; set; }
    }
}