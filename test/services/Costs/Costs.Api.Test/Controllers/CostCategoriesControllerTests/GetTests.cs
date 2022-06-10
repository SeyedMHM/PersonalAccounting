using Costs.Api.Controllers;
using Costs.Application.Features.CostCategoryFeatures.Queries.Common;
using Costs.Application.Features.CostCategoryFeatures.Queries.GetCostCategoryById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Costs.Api.Test.Controllers.CostCategoriesControllerTests
{
    public class GetTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly CostCategoriesController _costCategoriesController;
        private readonly GetCostCategoryByIdQuery _getCostCategoryByIdQuery;
        public GetTests()
        {
            _mediator = new Mock<IMediator>();
            _costCategoriesController = new CostCategoriesController(_mediator.Object);
            _getCostCategoryByIdQuery = new GetCostCategoryByIdQuery()
            {
                Id = It.IsAny<int>()
            };
        }


        [Fact]
        [Trait("CostCategory", "CostCategoriesController")]
        public async Task When_Call_ActionMethod_Invoke_GetCostCategoryByIdQueryHandler_ExactlyOnce()
        {
            _mediator
                .Setup(q => q.Send(_getCostCategoryByIdQuery, CancellationToken.None))
                .ReturnsAsync((GetCostCategoryResponse)null);

            IActionResult result = await _costCategoriesController.Get(_getCostCategoryByIdQuery, CancellationToken.None);

            _mediator.Verify(handler => handler.Send(_getCostCategoryByIdQuery, CancellationToken.None), Times.Once);
        }


        [Fact]
        [Trait("CostCategory", "CostCategoriesController")]
        public async Task When_UnExsitingCostCategory_Return_Status404NotFound()
        {
            _mediator
                .Setup(q => q.Send(_getCostCategoryByIdQuery, CancellationToken.None))
                .ReturnsAsync((GetCostCategoryResponse)null);

            IActionResult result = await _costCategoriesController.Get(_getCostCategoryByIdQuery, CancellationToken.None);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }


        [Fact]
        [Trait("CostCategory", "CostCategoriesController")]
        public async Task When_ExsitingCostCategory_Return_Status200OK()
        {
            _mediator
                .Setup(q => q.Send(_getCostCategoryByIdQuery, CancellationToken.None))
                .ReturnsAsync(new GetCostCategoryResponse()
                {
                    Id = 1
                });

            IActionResult result = await _costCategoriesController.Get(_getCostCategoryByIdQuery, CancellationToken.None);

            var okObjectResult = result as OkObjectResult;

            Assert.NotNull(okObjectResult);
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        }
    }
}
