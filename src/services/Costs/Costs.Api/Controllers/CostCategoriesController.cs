using Costs.Api.Filters;
using Costs.Application.Common.Models;
using Costs.Application.CostCategoriesApplication.Commands.CreateCostCategory;
using Costs.Application.CostCategoriesApplication.Commands.DeleteCostCategory;
using Costs.Application.CostCategoriesApplication.Commands.UpdateCostCategory;
using Costs.Application.CostCategoriesApplication.Queries.Common;
using Costs.Application.CostCategoriesApplication.Queries.GetAllCostCategories;
using Costs.Application.CostCategoriesApplication.Queries.GetCostCategoryById;
using Costs.Application.CostCategoriesApplication.Queries.GetPagedCostCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Costs.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ApiResultFilter]
    public class CostCategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CostCategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            List<GetCostCategoryResponse> category = await _mediator.Send(new GetAllCostCategoriesQuery(), cancellationToken);

            return Ok(category);
        }


        [HttpGet]
        public async Task<IActionResult> GetPagedList([FromQuery] GetPagedCostCategoriesQuery getPagedCostCategoriesQuery, CancellationToken cancellationToken)
        {
            PagedList<GetCostCategoryResponse> category = await _mediator.Send(getPagedCostCategoriesQuery, cancellationToken);

            return Ok(category);
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCostCategoryByIdQuery input, CancellationToken cancellationToken)
        {
            GetCostCategoryResponse category = await _mediator.Send(input, cancellationToken);

            return Ok(category);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateCostCategoryCommand input, CancellationToken cancellationToken)
        {
            CreateCostCategoryResponse costCategory = await _mediator.Send(input, cancellationToken);

            return Ok(costCategory);
        }


        [HttpPut]
        public async Task<IActionResult> Update(UpdateCostCategoryCommand input, CancellationToken cancellationToken)
        {
            UpdateCostCategoryResponse costCategory = await _mediator.Send(input, cancellationToken);

            return Ok(costCategory);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteCostCategoryCommand input, CancellationToken cancellationToken)
        {
            await _mediator.Send(input, cancellationToken);

            return Ok();
        }
    }
}
