using AutoMapper;
using Costs.Domain.Entities;

namespace Costs.Application.CostCategoriesApplication.Commands.UpdateCostCategory
{
    public class UpdateCostCategoryCommandProfile : Profile
    {
        public UpdateCostCategoryCommandProfile()
        {
            CreateMap<UpdateCostCategoryCommand, CostCategory>();
            CreateMap<CostCategory, UpdateCostCategoryResponse>();
        }
    }
}