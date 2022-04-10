using AutoMapper;
using Costs.Domain.Entities;
using Costs.Infrastructure.Data;
using MediatR;

namespace Costs.Application.CostCategoriesApplication.Commands.UpdateCostCategory
{
    public class UpdateCostCategoryCommandHandler :
        IRequestHandler<UpdateCostCategoryCommand, UpdateCostCategoryResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public UpdateCostCategoryCommandHandler(
            ApplicationDbContext applicationDbContext,
            IMapper mapper
            )
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<UpdateCostCategoryResponse> Handle(UpdateCostCategoryCommand request, CancellationToken cancellationToken)
        {
            CostCategory costCategory = await _applicationDbContext.CostCategories
                .FindAsync(new object[] { request.Id }, cancellationToken);

            _mapper.Map(request, costCategory);

            await _applicationDbContext.SaveChangesAsync();

            UpdateCostCategoryResponse result = _mapper.Map<UpdateCostCategoryResponse>(costCategory);

            return result;
        }
    }
}
