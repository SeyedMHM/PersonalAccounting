﻿namespace Costs.Application.CostCategoriesApplication.Commands.UpdateCostCategory.GetById
{
    public class GetByIdResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public string? Description { get; set; }
    }
}