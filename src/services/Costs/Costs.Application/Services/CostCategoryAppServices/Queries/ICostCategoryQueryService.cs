using Costs.Application.Common.Models;
using Costs.Application.Features.CostCategoryFeatures.Queries.GetPagedCostCategories;

namespace Costs.Application.Services.CostCategoryAppServices.Queries
{
    public interface ICostCategoryQueryService
    {
        Task<TResponse> GetByIdWithIncludeProperties<TResponse>(int id, string includeProperties, CancellationToken cancellationToken);

        Task<List<TResponse>> GetAllWithIncludeProperties<TResponse>(string includeProperties, CancellationToken cancellationToken);

        Task<PagedList<TResponse>> Search<TResponse>(GetPagedCostCategoriesQuery search, CancellationToken cancellationToken);
    }
}