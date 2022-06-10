using Costs.Application.Services.CostCategoryAppServices.Commands;
using MediatR;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.DeleteCostCategory.GetById
{
    public class GetByIdCommandHandler : IRequestHandler<GetByIdCommand,GetByIdResponse>
    {
        private readonly ICostCategoryCommandService _costCategoryCommandService;

        public GetByIdCommandHandler(ICostCategoryCommandService costCategoryCommandService)
        {
            _costCategoryCommandService = costCategoryCommandService;
        }

        public async Task<GetByIdResponse> Handle(GetByIdCommand request, CancellationToken cancellationToken)
        {
            return await _costCategoryCommandService.GetByIdAsNoTracking<GetByIdResponse>(request.Id, cancellationToken);
        }
    }
}
