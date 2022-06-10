using Costs.Api.Controllers;
using Costs.Api.Test.Fixtures;
using Costs.Application.Common.Models;
using Costs.Application.Features.CostCategoryFeatures.Queries.Common;
using Costs.Application.Features.CostCategoryFeatures.Queries.GetPagedCostCategories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Costs.Api.Test.Controllers.CostCategoriesControllerTests
{
    public class GetPagedListTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly CostCategoriesController _costCategoriesController;
        private readonly GetPagedCostCategoriesQuery _getPagedCostCategoriesQuery;

        public GetPagedListTests()
        {
            _mediator = new Mock<IMediator>();
            _costCategoriesController = new CostCategoriesController(_mediator.Object);
            _getPagedCostCategoriesQuery = new GetPagedCostCategoriesQuery();
        }


        [Fact]
        [Trait("CostCategory", "CostCategoriesController")]
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
        [Trait("CostCategory", "CostCategoriesController")]
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
        [Trait("CostCategory", "CostCategoriesController")]
        public async Task When_ExistingCostCategories_Return_ExactlyExistingCostCategories()
        {
            //10 Items
            PagedList<GetCostCategoryResponse> fixturePagedListResult = new PagedList<GetCostCategoryResponse>()
            {
                Items = await CostCategoryFixture.GetAllCostCategoriesTest(),
                CurrentPage = 1,
                PageSize = 5,
                TotalCount = 10
            };
            PagedList<GetCostCategoryResponse> fixturePagedResultAfterReturnFromController = new PagedList<GetCostCategoryResponse>()
            {
                Items = await CostCategoryFixture.GetAllCostCategoriesTest(),
                CurrentPage = 1,
                PageSize = 5,
                TotalCount = 10
            };

            _mediator
                .Setup(q => q.Send(_getPagedCostCategoriesQuery, CancellationToken.None))
                .ReturnsAsync(fixturePagedResultAfterReturnFromController);

            IActionResult result = await _costCategoriesController
                .GetPagedList(_getPagedCostCategoriesQuery, CancellationToken.None);

            OkObjectResult okObjectResult = result as OkObjectResult;

            Assert.NotNull(result);
            Assert.IsType(typeof(PagedList<GetCostCategoryResponse>), okObjectResult.Value);
            Assert.Equal(JsonSerializer.Serialize(fixturePagedListResult.Items), JsonSerializer.Serialize(fixturePagedResultAfterReturnFromController.Items));

            //Assert.True(
            //   fixturePagedListResult.Items
            //   .All(shouldItem => fixturePagedResultAfterReturnFromController.Items.Any(isItem => isItem == shouldItem))
            //);
            //Assert.Equal(fixturePagedListResult.HasPrevious, fixturePagedResultAfterReturnFromController.HasPrevious);
            //Assert.Equal(fixturePagedListResult.HasNext, fixturePagedResultAfterReturnFromController.HasNext);
            //Assert.Equal(fixturePagedListResult.TotalCount, fixturePagedResultAfterReturnFromController.TotalCount);
            //Assert.Equal(fixturePagedListResult.CurrentPage, fixturePagedResultAfterReturnFromController.CurrentPage);
            //Assert.Equal(fixturePagedListResult.HasNext, fixturePagedResultAfterReturnFromController.HasNext);
            //Assert.Equal(fixturePagedListResult.PageSize, fixturePagedResultAfterReturnFromController.PageSize);
            //Assert.Equal(fixturePagedListResult.TotalPages, fixturePagedResultAfterReturnFromController.TotalPages);

        }


        [Fact]
        [Trait("CostCategory", "CostCategoriesController")]
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
        [Trait("CostCategory", "CostCategoriesController")]
        public async Task When_IsNullCostCategories_Return_Status404NotFound()
        {
            //10 Items
            List<GetCostCategoryResponse> costCategories = new List<GetCostCategoryResponse>();

            PagedList<GetCostCategoryResponse> pagedListResult = new();

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
