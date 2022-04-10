using AutoMapper;
using Costs.Domain.Entities;

namespace Costs.Application.CostCategoriesApplication.Queries.Common
{
    public class CostCategoryProfile : Profile
    {
        public CostCategoryProfile()
        {
            CreateMap<CostCategory, GetCostCategoryResponse>();
        }
    }
}