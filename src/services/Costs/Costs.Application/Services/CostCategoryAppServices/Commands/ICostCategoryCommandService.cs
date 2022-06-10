using Costs.Application.Features.CostCategoryFeatures.Commands.UpdateCostCategory;
using Costs.Domain.Entities;

namespace Costs.Application.Services.CostCategoryAppServices.Commands
{
    public interface ICostCategoryCommandService
    {
        Task<TResponse> Create<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken);

        Task<CostCategory> GetById(int id, CancellationToken cancellationToken);

        Task<TResponse> GetById<TResponse>(int id, CancellationToken cancellationToken);

        Task<TResponse> GetByIdAsNoTracking<TResponse>(int id, CancellationToken cancellationToken);

        Task<TResponse> Update<TResponse>(UpdateCostCategoryCommand request, CancellationToken cancellationToken);
    }
}