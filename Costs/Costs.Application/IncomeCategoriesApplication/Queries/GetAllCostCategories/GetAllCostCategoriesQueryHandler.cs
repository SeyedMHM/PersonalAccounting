using AutoMapper;
using Costs.Application.CostCategoriesApplication.Queries.Common;
using Costs.Domain.Entities;
using Costs.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Costs.Application.CostCategoriesApplication.Queries.GetAllCostCategories
{
    public class GetAllCostCategoriesQueryHandler : IRequestHandler<GetAllCostCategoriesQuery, List<GetCostCategoryResponse>>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetAllCostCategoriesQueryHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<List<GetCostCategoryResponse>> Handle(GetAllCostCategoriesQuery request, CancellationToken cancellationToken)
        {
            List<CostCategory> categories = await _applicationDbContext.CostCategories
                .Include(q => q.Parent)
                .Include(q => q.Children)
                .AsNoTracking()
                .ToListAsync();

            List<GetCostCategoryResponse> result = _mapper.Map<List<GetCostCategoryResponse>>(categories);

            return result;
        }
    }
}
