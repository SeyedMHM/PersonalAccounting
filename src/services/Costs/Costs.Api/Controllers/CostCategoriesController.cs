using Costs.Api.Filters;
using Costs.Application.Common.Models;
using Costs.Application.CostCategoriesApplication.Commands.CreateCostCategory;
using Costs.Application.CostCategoriesApplication.Queries.Common;
using Costs.Application.CostCategoriesApplication.Queries.GetAllCostCategories;
using Costs.Application.CostCategoriesApplication.Queries.GetCostCategoryById;
using Costs.Application.CostCategoriesApplication.Queries.GetPagedCostCategories;
using DeleteCostCategory = Costs.Application.CostCategoriesApplication.Commands.DeleteCostCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Costs.Application.CostCategoriesApplication.Commands.DeleteCostCategory;
using Costs.Application.CostCategoriesApplication.Commands.UpdateCostCategory;

namespace Costs.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ApiResultFilter]
    public class CostCategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CostCategoriesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var categories = await _mediator.Send(new GetAllCostCategoriesQuery(), cancellationToken);

            if (categories.Any())
            {
                return Ok(categories);
            }

            return NotFound();
        }


        [HttpGet]
        public async Task<IActionResult> GetPagedList([FromQuery] GetPagedCostCategoriesQuery getPagedCostCategoriesQuery, CancellationToken cancellationToken)
        {
            PagedList<GetCostCategoryResponse> categories = await _mediator.Send(getPagedCostCategoriesQuery, cancellationToken);

            if (categories.TotalCount == 0)
            {
                return NotFound();
            }

            return Ok(categories);
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCostCategoryByIdQuery input, CancellationToken cancellationToken)
        {
            var category = await _mediator.Send(input, cancellationToken);

            if (category is null)
            {
                return NotFound();
            }

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

            if (costCategory is null)
            {
                return NotFound();
            }

            return Ok(costCategory);
        }
       

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteCostCategory.GetById.GetByIdCommand input, CancellationToken cancellationToken)
        {
            var costCategory = await _mediator.Send(input, cancellationToken);
            
            if (costCategory is null)
            {
                return NotFound();
            }

            await _mediator.Send(new DeleteCostCategoryCommand(costCategory.Id), cancellationToken);
            
            return Ok();
        }
    }
}
