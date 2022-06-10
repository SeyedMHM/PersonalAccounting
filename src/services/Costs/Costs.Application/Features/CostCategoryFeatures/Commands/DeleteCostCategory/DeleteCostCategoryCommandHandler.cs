using AutoMapper;
using Costs.Domain.Entities;
using Costs.Infrastructure.Data;
using MediatR;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.DeleteCostCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCostCategoryCommand, Unit>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCostCategoryCommand request, CancellationToken cancellationToken)
        {
            CostCategory costCategory = _mapper.Map<CostCategory>(request);

            _applicationDbContext.CostCategories.Remove(costCategory);

            await _applicationDbContext.SaveChangesAsync();

            return new Unit();
        }
    }
}
