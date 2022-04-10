using Costs.Domain.Entities;
using Costs.Infrastructure.Data;
using MediatR;

namespace Costs.Application.CostCategoriesApplication.Commands.DeleteCostCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCostCategoryCommand, Unit>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeleteCategoryCommandHandler(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(DeleteCostCategoryCommand request, CancellationToken cancellationToken)
        {
            CostCategory costCategory = await _applicationDbContext.CostCategories
                .FindAsync(new object[] { request.Id }, cancellationToken);

            _applicationDbContext.CostCategories.Remove(costCategory);

            await _applicationDbContext.SaveChangesAsync();

            return new Unit();
        }
    }
}
