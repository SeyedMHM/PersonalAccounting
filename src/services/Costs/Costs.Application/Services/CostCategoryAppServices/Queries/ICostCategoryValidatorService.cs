namespace Costs.Application.Services.CostCategoryAppServices.Queries
{
    public interface ICostCategoryValidatorService
    {
        Task<bool> IsExistTitle(string title, CancellationToken cancellationToken);

        Task<bool> IsExistTitle(int id, string title, CancellationToken cancellationToken);

        Task<bool> IsValidParentId(int? parentId, CancellationToken cancellationToken);
    }
}