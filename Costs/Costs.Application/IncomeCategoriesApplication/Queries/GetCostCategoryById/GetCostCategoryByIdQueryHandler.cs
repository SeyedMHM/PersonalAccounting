using AutoMapper;
using Costs.Application.CostCategoriesApplication.Queries.Common;
using Costs.Domain.Entities;
using Costs.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Costs.Application.CostCategoriesApplication.Queries.GetCostCategoryById
{
    public class GetCostCategoryByIdQueryHandler : IRequestHandler<GetCostCategoryByIdQuery, GetCostCategoryResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetCostCategoryByIdQueryHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<GetCostCategoryResponse> Handle(GetCostCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            CostCategory costCategory = await _applicationDbContext.CostCategories
                .Include(q => q.Parent)
                .Include(q => q.Children)
                .AsNoTracking()
                .Where(q => q.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var result = _mapper.Map<GetCostCategoryResponse>(costCategory);

            return result;
        }
    }
}
