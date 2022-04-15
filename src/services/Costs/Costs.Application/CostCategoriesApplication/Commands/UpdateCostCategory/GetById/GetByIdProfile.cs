using AutoMapper;
using Costs.Domain.Entities;

namespace Costs.Application.CostCategoriesApplication.Commands.UpdateCostCategory.GetById
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