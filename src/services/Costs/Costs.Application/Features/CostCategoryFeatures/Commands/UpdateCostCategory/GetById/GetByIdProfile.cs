using AutoMapper;
using Costs.Domain.Entities;

namespace Costs.Application.Features.CostCategoryFeatures.Commands.UpdateCostCategory.GetById
{
    public class GetByIdProfile : Profile
    {
        public GetByIdProfile()
        {
            CreateMap<CostCategory, GetByIdResponse>();
            CreateMap<GetByIdResponse, UpdateCostCategoryCommand>();
        }
    }
}