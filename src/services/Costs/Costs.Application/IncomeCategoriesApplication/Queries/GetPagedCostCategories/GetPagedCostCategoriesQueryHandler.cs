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
            var categories = await _applicationDbContext.CostCategories
                .Include(q => q.Parent)
                .Include(q => q.Children)
                .AsNoTracking()
                .ToPagedResult<CostCategory, GetCostCategoryResponse>(request, _mapper, cancellationToken);

            return categories;
        }
    }
}