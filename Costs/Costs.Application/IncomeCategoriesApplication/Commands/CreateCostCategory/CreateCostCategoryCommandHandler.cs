using AutoMapper;
using Costs.Domain.Entities;
using Costs.Infrastructure.Data;
using MediatR;

namespace Costs.Application.CostCategoriesApplication.Commands.CreateCostCategory
{
    public class CreateCostCategoryCommandHandler :
        IRequestHandler<CreateCostCategoryCommand, CreateCostCategoryResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public CreateCostCategoryCommandHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper
            )
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<CreateCostCategoryResponse> Handle(CreateCostCategoryCommand request, CancellationToken cancellationToken)
        {
            CostCategory costCategory = _mapper.Map<CostCategory>(request);

            await _applicationDbContext.CostCategories.AddAsync(costCategory, cancellationToken);

            await _applicationDbContext.SaveChangesAsync();

            CreateCostCategoryResponse result = _mapper.Map<CreateCostCategoryResponse>(costCategory);

            return result;
        }
    }
}