using AutoMapper;
using Costs.Application.Features.CostCategoryFeatures.Commands.UpdateCostCategory;
using Costs.Application.Services.CostCategoryAppServices.Queries;
using Costs.Domain.Entities;
using Costs.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Costs.Application.Services.CostCategoryAppServices.Commands
{
    public class CostCategoryCommandService : ICostCategoryCommandService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly ICostCategoryValidatorService _costCategoryValidatorService;

        public CostCategoryCommandService(
            ApplicationDbContext applicationDbContext,
            IMapper mapper,
            ICostCategoryValidatorService costCategoryValidatorService
            )
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _costCategoryValidatorService = costCategoryValidatorService;
        }


        public async Task<TResponse> Create<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        {
            CostCategory costCategory = _mapper.Map<CostCategory>(request);

            bool isExistTitle = await _costCategoryValidatorService.IsExistTitle(costCategory.Title, cancellationToken);
            if (isExistTitle)
            {
                throw new Exception("'Title' is exist. You have to change category Title");
            }

            bool isValidParentId = await _costCategoryValidatorService.IsValidParentId(costCategory.ParentId, cancellationToken);
            if (!isValidParentId)
            {
                throw new Exception("'ParentId' isn't exist. You should to change 'ParentId'");
            }

            await _applicationDbContext.CostCategories.AddAsync(costCategory, cancellationToken);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TResponse>(costCategory);
        }


        public async Task<TResponse> Update<TResponse>(UpdateCostCategoryCommand request, CancellationToken cancellationToken)
        {
            bool isExistTitle = await _costCategoryValidatorService.IsExistTitle(request.Id, request.Title, cancellationToken);
            if (isExistTitle)
            {
                throw new Exception("'Title' is exist. You have to change category Title");
            }

            bool isValidParentId = await _costCategoryValidatorService.IsValidParentId(request.ParentId, cancellationToken);
            if (!isValidParentId)
            {
                throw new Exception("'ParentId' isn't exist. You should to change 'ParentId'");
            }

            CostCategory costCategory = await GetByIdAsNoTracking(request.Id, cancellationToken);

            if (costCategory is null)
            {
                throw new Exception("'CostCategory' isn't exist.");
            }

            _mapper.Map(request, costCategory);

            await _applicationDbContext.SaveChangesAsync();

            TResponse result = _mapper.Map<TResponse>(costCategory);

            return result;
        }


        public async Task<CostCategory> GetById(int id, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.CostCategories
              .FindAsync(new object[] { id }, cancellationToken);
        }


        public async Task<TResponse> GetById<TResponse>(int id, CancellationToken cancellationToken)
        {
            CostCategory costCategory = await _applicationDbContext.CostCategories
              .FindAsync(new object[] { id }, cancellationToken);

            return _mapper.Map<TResponse>(costCategory);

        }

        public async Task<TResponse> GetByIdAsNoTracking<TResponse>(int id, CancellationToken cancellationToken)
        {
            CostCategory costCategory = await _applicationDbContext.CostCategories
                .Where(q => q.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<TResponse>(costCategory);
        }
    }
}
