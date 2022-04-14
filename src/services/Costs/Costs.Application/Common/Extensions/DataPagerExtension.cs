using AutoMapper;
using Costs.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Costs.Application.Common.Extensions
{
    public static class DataPagerExtension
    {
        public static async Task<PagedList<TModel>> ToPagedResult<TModel>(
            this IQueryable<TModel> query,
            int page,
            int limit,
            CancellationToken cancellationToken)
            where TModel : class
        {
            var paged = new PagedList<TModel>();

            page = (page < 0) ? 1 : page;

            paged.CurrentPage = page;
            paged.PageSize = limit;
            var startRow = (page - 1) * limit;

            paged.Items = await query
                       .Skip(startRow)
                       .Take(limit)
                       .ToListAsync(cancellationToken);

            paged.TotalCount = await query.CountAsync(cancellationToken);

            return paged;
        }

        public static async Task<PagedList<TDto>> ToPagedResult<TModel, TDto>(
             this IQueryable<TModel> query,
             PagedListMetadata pagedListMetadata,
             IMapper _mapper,
             CancellationToken cancellationToken)
             where TModel : class
        {
            var paged = new PagedList<TDto>();
            paged.CurrentPage = pagedListMetadata.PageId;
            paged.PageSize = pagedListMetadata.PageSize;
            var startRow = pagedListMetadata.PageSize * (pagedListMetadata.PageId - 1);

            var items = await query
                       .Skip(startRow)
                       .Take(pagedListMetadata.PageSize)
                       .ToListAsync(cancellationToken);

            paged.TotalCount = await query.CountAsync(cancellationToken);

            paged.Items = _mapper.Map<List<TDto>>(items);

            return paged;
        }
    }
}

