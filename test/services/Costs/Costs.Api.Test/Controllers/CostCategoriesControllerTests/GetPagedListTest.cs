using Costs.Api.Controllers;
using Costs.Api.Test.Fixtures;
using Costs.Application.Common.Models;
using Costs.Application.CostCategoriesApplication.Queries.Common;
using Costs.Application.CostCategoriesApplication.Queries.GetPagedCostCategories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Costs.Api.Test.Controllers.CostCategoriesControllerTests
{
    public class GetPagedListTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly CostCategoriesController _costCategoriesController;
        private readonly GetPagedCostCategoriesQuery _getPagedCostCategoriesQuery;

        public GetPagedListTest()
        {
            _mediator = new Mock<IMediator>();
            _costCategoriesController = new CostCategoriesController(_mediator.Object);
            _getPagedCostCategoriesQuery = new GetPagedCostCategoriesQuery();
        }


        [Fact]
        [Trait("CostCategory", "GetPagedList")]
        public async Task When_Call_ActionMethod_Invoke_GetPagedCostCategoriesQueryHandler_ExactlyOnce()
        {
            _mediator
                .Setup(q => q.Send(_getPagedCostCategoriesQuery, CancellationToken.None))
                .ReturnsAsync(new PagedList<GetCostCategoryResponse>());

            IActionResult result = await _costCategoriesController
                .GetPagedList(_getPagedCostCategoriesQuery, CancellationToken.None);

            _mediator.Verify(handler => handler.Send(_getPagedCostCategoriesQuery, CancellationToken.None), Times.Once);

        }


        [Fact]
        [Trait("CostCategory", "GetPagedList")]
        public async Task When_ExistingCostCategories_Return_Status200OK()
        {
            //10 Items
            List<GetCostCategoryResponse> costCategories = await CostCategoryFixture.GetAllCostCategoriesTest();

            PagedList<GetCostCategoryResponse> pagedListResult = new PagedList<GetCostCategoryResponse>()
            {
                Items = costCategories,
                CurrentPage = 1,
                PageSize = 5,
                TotalCount = 10
            };

            _mediator
                .Setup(q => q.Send(_getPagedCostCategoriesQuery, CancellationToken.None))
                .ReturnsAsync(pagedListResult);

            IActionResult result = await _costCategoriesController
                .GetPagedList(_getPagedCostCategoriesQuery, CancellationToken.None);

            OkObjectResult okObjectResult = result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        }


        [Fact]
        [Trait("CostCategory", "GetPagedList")]
        public async Task When_ExistingCostCategories_ReturnsAllExistingCostCategories()
        {
            //10 Items
            List<GetCostCategoryResponse> costCategories = await CostCategoryFixture.GetAllCostCategoriesTest();

            PagedList<GetCostCategoryResponse> pagedListResult = new PagedList<GetCostCategoryResponse>()
            {
                Items = costCategories,
                CurrentPage = 1,
                PageSize = 5,
                TotalCount = 10
            };

            _mediator
                .Setup(q => q.Send(_getPagedCostCategoriesQuery, CancellationToken.None))
                .ReturnsAsync(pagedListResult);

            IActionResult result = await _costCategoriesController
                .GetPagedList(_getPagedCostCategoriesQuery, CancellationToken.None);

            OkObjectResult okObjectResult = result as OkObjectResult;

            Assert.NotNull(result);
            Assert.IsType(typeof(PagedList<GetCostCategoryResponse>), okObjectResult.Value);
        }


        [Fact]
        [Trait("CostCategory", "GetPagedList")]
        public async Task When_IsEmptyCostCategories_Return_Status404NotFound()
        {
            //10 Items
            List<GetCostCategoryResponse> costCategories = new List<GetCostCategoryResponse>();

            PagedList<GetCostCategoryResponse> pagedListResult = new PagedList<GetCostCategoryResponse>()
            {
                Items = costCategories,
                CurrentPage = 1,
                PageSize = 5,
                TotalCount = 0
            };

            _mediator
                .Setup(q => q.Send(_getPagedCostCategoriesQuery, CancellationToken.None))
                .ReturnsAsync(pagedListResult);

            IActionResult result = await _costCategoriesController
                .GetPagedList(_getPagedCostCategoriesQuery, CancellationToken.None);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }


        [Fact]
        [Trait("CostCategory", "GetPagedList")]
        public async Task When_IsNullCostCategories_Return_Status404NotFound()
        {
            //10 Items
            List<GetCostCategoryResponse> costCategories = new List<GetCostCategoryResponse>();

            PagedList<GetCostCategoryResponse> pagedListResult = null;

            _mediator
                .Setup(q => q.Send(_getPagedCostCategoriesQuery, CancellationToken.None))
                .ReturnsAsync(pagedListResult);

            IActionResult result = await _costCategoriesController
                .GetPagedList(_getPagedCostCategoriesQuery, CancellationToken.None);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
    
    }
}
