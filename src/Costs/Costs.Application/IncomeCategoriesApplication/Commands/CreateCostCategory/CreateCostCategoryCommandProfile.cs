using AutoMapper;
using Costs.Domain.Entities;

namespace Costs.Application.CostCategoriesApplication.Commands.CreateCostCategory
{
    public class CreateCostCategoryCommandProfile : Profile
    {
        public CreateCostCategoryCommandProfile()
        {
            CreateMap<CreateCostCategoryCommand, CostCategory>();
            CreateMap<CostCategory, CreateCostCategoryResponse>();
        }
    }
}