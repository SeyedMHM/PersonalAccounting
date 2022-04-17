using Costs.Api.Controllers;
using Costs.Api.Test.Fixtures;
using Costs.Application.CostCategoriesApplication.Queries.Common;
using Costs.Application.CostCategoriesApplication.Queries.GetAllCostCategories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Costs.Api.Test.Controllers.CostCategoriesControllerTests
{
    public class GetAllTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly CostCategoriesController _costCategoriesController;
        private readonly GetAllCostCategoriesQuery _getAllCostCategoriesQuery;

        public GetAllTest()
        {
            _mediator = new Mock<IMediator>();
            _costCategoriesController = new CostCategoriesController(_mediator.Object);
            _getAllCostCategoriesQuery = new GetAllCostCategoriesQuery();
        }


        [Fact]
        [Trait("CostCategory", "GetAll")]
        public async Task When_Call_ActionMethod_Invoke_GetAllCostCategoriesQueryHandler_ExactlyOnce()
        {
            _mediator
                .Setup(q => q.Send(_getAllCostCategoriesQuery, CancellationToken.None))
                .ReturnsAsync(new List<GetCostCategoryResponse>());

            IActionResult result = await _costCategoriesController
                .GetAll(_getAllCostCategoriesQuery, CancellationToken.None);

            _mediator.Verify(handler => handler.Send(_getAllCostCategoriesQuery, CancellationToken.None), Times.Once);
        }


        [Fact]
        [Trait("CostCategory", "GetAll")]
        public async Task When_ExistingCostCategories_Return_Status200OK()
        {
            _mediator
                .Setup(q => q.Send(_getAllCostCategoriesQuery, CancellationToken.None))
                .ReturnsAsync(await CostCategoryFixture.GetAllCostCategoriesTest());

            IActionResult result = await _costCategoriesController.GetAll(_getAllCostCategoriesQuery, CancellationToken.None);

            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        }


        [Fact]
        [Trait("CostCategory", "GetAll")]
        public async Task When_ExistingCostCategories_ReturnsAllExistingCostCategories()
        {
            _mediator
                .Setup(q => q.Send(_getAllCostCategoriesQuery, CancellationToken.None))
                .ReturnsAsync(await CostCategoryFixture.GetAllCostCategoriesTest());

            IActionResult result = await _costCategoriesController.GetAll(_getAllCostCategoriesQuery, CancellationToken.None);

            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            Assert.IsType(typeof(List<GetCostCategoryResponse>), okObjectResult.Value);
        }


        [Fact]
        [Trait("CostCategory", "GetAll")]
        public async Task When_IsNullCostCategories_Return_Status404NotFound()
        {
            _mediator
                .Setup(q => q.Send(_getAllCostCategoriesQuery, CancellationToken.None))
                .ReturnsAsync((List<GetCostCategoryResponse>)null);

            IActionResult result = await _costCategoriesController.GetAll(_getAllCostCategoriesQuery, CancellationToken.None);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }


        [Fact]
        [Trait("CostCategory", "GetAll")]
        public async Task When_IsEmptyListOfCostCategories_Return_Status404NotFound()
        {
            _mediator
                .Setup(q => q.Send(_getAllCostCategoriesQuery, CancellationToken.None))
                .ReturnsAsync(new List<GetCostCategoryResponse>());

            IActionResult result = await _costCategoriesController.GetAll(_getAllCostCategoriesQuery, CancellationToken.None);

            var notFoundResult = result as NotFoundResult;
            
            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

    }
}
