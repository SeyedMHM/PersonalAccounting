using Costs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Costs.Application.Services.CostCategoryAppServices.Queries
{
    public class CostCategoryValidatorService : ICostCategoryValidatorService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CostCategoryValidatorService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        public async Task<bool> IsExistTitle(string title, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.CostCategories
                .Where(q => q.Title == title)
                .AnyAsync(cancellationToken);
        }


        public async Task<bool> IsExistTitle(int id, string title, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.CostCategories
                .Where(q => q.Id != id && q.Title == title)
                .AnyAsync(cancellationToken);
        }


        public async Task<bool> IsValidParentId(int? parentId, CancellationToken cancellationToken)
        {
            return parentId is null ? true : await _applicationDbContext.CostCategories
                .Where(q => q.Id == parentId)
                .AnyAsync(cancellationToken);
        }
    }
}
