using AutoMapper;
using Costs.Application.Common.Extensions;
using Costs.Application.Common.Models;
using Costs.Application.Features.CostCategoryFeatures.Queries.GetPagedCostCategories;
using Costs.Domain.Entities;
using Costs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Costs.Application.Services.CostCategoryAppServices.Queries
{
    public class CostCategoryQueryService : ICostCategoryQueryService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public CostCategoryQueryService(
            ApplicationDbContext applicationDbContext,
            IMapper mapper
            )
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<TResponse> GetByIdWithIncludeProperties<TResponse>(int id, string includeProperties, CancellationToken cancellationToken)
        {
            var query = _applicationDbContext.CostCategories
                .Where(q => q.Id == id)
                .AsNoTracking();

            foreach (var includeProperty in includeProperties
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            CostCategory costCategory = await query.FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<TResponse>(costCategory);
        }


        public async Task<List<TResponse>> GetAllWithIncludeProperties<TResponse>(string includeProperties, CancellationToken cancellationToken)
        {
            var query = _applicationDbContext.CostCategories
                .AsNoTracking();

            foreach (var includeProperty in includeProperties
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            List<CostCategory> costCategories = await query.ToListAsync(cancellationToken);

            return _mapper.Map<List<TResponse>>(costCategories);
        }


        public async Task<PagedList<TResponse>> Search<TResponse>(GetPagedCostCategoriesQuery search, CancellationToken cancellationToken)
        {
            var query = _applicationDbContext.CostCategories
                .Include(q => q.Parent)
                .Include(q => q.Children)
                .AsNoTracking();

            if (search.ParentId is not null)
            {
                query = query.Where(q => q.ParentId == search.ParentId);
            }

            if (!string.IsNullOrEmpty(search.Title))
            {
                query = query.Where(q => q.Title.Contains(search.Title));
            }

            var costCategories = await query
                .ToPagedResult<CostCategory, TResponse>(search, _mapper, cancellationToken);

            return costCategories ?? new PagedList<TResponse>();
        }

    }
}
