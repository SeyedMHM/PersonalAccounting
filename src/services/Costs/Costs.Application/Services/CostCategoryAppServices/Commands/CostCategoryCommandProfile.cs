using AutoMapper;
using Costs.Application.Features.CostCategoryFeatures.Commands.CreateCostCategory;
using Costs.Application.Features.CostCategoryFeatures.Commands.DeleteCostCategory;
using Costs.Application.Features.CostCategoryFeatures.Commands.DeleteCostCategory.GetById;
using Costs.Application.Features.CostCategoryFeatures.Commands.UpdateCostCategory;
using Costs.Domain.Entities;

namespace Costs.Application.Services.CostCategoryAppServices.Commands
{
    public class CostCategoryCommandProfile : Profile
    {
        public CostCategoryCommandProfile()
        {
            CreateMap<CreateCostCategoryCommand, CostCategory>();

            CreateMap<CostCategory, GetByIdResponse>();
            CreateMap<GetByIdResponse, DeleteCostCategoryCommand>();

            CreateMap<GetByIdResponse, UpdateCostCategoryCommand>();

            CreateMap<UpdateCostCategoryCommand, CostCategory>();
            CreateMap<CostCategory, UpdateCostCategoryResponse>();
        }
    }
}