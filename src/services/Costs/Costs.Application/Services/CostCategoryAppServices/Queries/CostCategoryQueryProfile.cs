using AutoMapper;
using Costs.Application.Features.CostCategoryFeatures.Commands.CreateCostCategory;
using Costs.Application.Features.CostCategoryFeatures.Queries.Common;
using Costs.Domain.Entities;

namespace Costs.Application.Services.CostCategoryAppServices.Queries
{
    public class CostCategoryQueryProfile : Profile
    {
        public CostCategoryQueryProfile()
        {
            CreateMap<CostCategory, CreateCostCategoryResponse>();
            CreateMap<CostCategory, GetCostCategoryResponse>();
        }
    }
}