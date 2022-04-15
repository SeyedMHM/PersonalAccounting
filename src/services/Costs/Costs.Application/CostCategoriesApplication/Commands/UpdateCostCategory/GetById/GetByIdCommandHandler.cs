using AutoMapper;
using Costs.Domain.Entities;
using Costs.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Costs.Application.CostCategoriesApplication.Commands.UpdateCostCategory.GetById
{
    public class GetByIdCommandHandler : IRequestHandler<GetByIdCommand, GetByIdResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetByIdCommandHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<GetByIdResponse> Handle(GetByIdCommand request, CancellationToken cancellationToken)
        {
            CostCategory costCategory = await _applicationDbContext.CostCategories
                .Include(q => q.Parent)
                .Include(q => q.Children)
                .Where(q => q.Id == request.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            var result = _mapper.Map<GetByIdResponse>(costCategory);

            return result;
        }
    }
}
