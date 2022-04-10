using PersonalAccounting.Shared.Common.Entites;

namespace Costs.Domain.Entities
{
    public class CostCategory : BaseEntity
    {
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string? Description { get; set; }

        public CostCategory? Parent { get; set; }
        public ICollection<CostCategory> Children { get; set; }
    }
}