using AutoMapper;
using Costs.Application.Services.CostCategoryAppServices;
using Costs.Application.Services.CostCategoryAppServices.Commands;
using Costs.Domain.Entities;
using Costs.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.UpdateCostCategory.GetById
{
    public class GetByIdCommandHandler : IRequestHandler<GetByIdCommand, GetByIdResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICostCategoryCommandService _costCategoryCommandService;

        public GetByIdCommandHandler(IMapper mapper, ICostCategoryCommandService costCategoryCommandService)
        {
            _mapper = mapper;
            _costCategoryCommandService = costCategoryCommandService;
        }

        public async Task<GetByIdResponse> Handle(GetByIdCommand request, CancellationToken cancellationToken)
        {
            CostCategory costCategory = await _costCategoryCommandService.GetById(request.Id, cancellationToken);

            return _mapper.Map<GetByIdResponse>(costCategory);
        }
    }
}
