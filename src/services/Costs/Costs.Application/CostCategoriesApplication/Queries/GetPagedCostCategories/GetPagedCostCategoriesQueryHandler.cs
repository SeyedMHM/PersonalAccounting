using AutoMapper;
using Costs.Application.Common.Extensions;
using Costs.Application.Common.Models;
using Costs.Application.CostCategoriesApplication.Queries.Common;
using Costs.Domain.Entities;
using Costs.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Costs.Application.CostCategoriesApplication.Queries.GetPagedCostCategories
{
    public class GetPagedCostCategoriesQueryHandler
        : IRequestHandler<GetPagedCostCategoriesQuery, PagedList<GetCostCategoryResponse>>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetPagedCostCategoriesQueryHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PagedList<GetCostCategoryResponse>> Handle(GetPagedCostCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = _applicationDbContext.CostCategories
                .Include(q => q.Parent)
                .Include(q => q.Children)
                .AsNoTracking();

            if (request.ParentId is not null)
            {
                query = query.Where(q => q.ParentId == request.ParentId);
            }

            if (!string.IsNullOrEmpty(request.Title))
            {
                query = query.Where(q => q.Title.Contains(request.Title));
            }

            var categories = await query
                .ToPagedResult<CostCategory, GetCostCategoryResponse>(request, _mapper, cancellationToken);

            return categories;
        }
    }
}