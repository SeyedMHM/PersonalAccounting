using AutoMapper;
using Costs.Api.Controllers;
using Costs.Application.CostCategoriesApplication.Queries.Common;
using Costs.Application.CostCategoriesApplication.Queries.GetCostCategoryById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using Xunit;

namespace Costs.Api.Test.Controllers.CostCategoriesControllerTests
{
    public class GetTests
    {
        [Fact]
        [Trait("CostCategory", "Get")]
        public async void When_Call_ActionMethod_Invoke_GetCostCategoryByIdQueryHandler_ExactlyOnce()
        {
            Mock<IMediator> mockMediator = new Mock<IMediator>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            CostCategoriesController costCategoriesController = new CostCategoriesController(mockMediator.Object, mockMapper.Object);

            GetCostCategoryByIdQuery getCostCategoryByIdQuery = new GetCostCategoryByIdQuery();
            getCostCategoryByIdQuery.Id = It.IsAny<int>();
            mockMediator
                .Setup(q => q.Send(getCostCategoryByIdQuery, CancellationToken.None))
                .ReturnsAsync((GetCostCategoryResponse)null);

            IActionResult result = await costCategoriesController.Get(getCostCategoryByIdQuery, CancellationToken.None);

            mockMediator.Verify(handler => handler.Send(getCostCategoryByIdQuery, CancellationToken.None), Times.Once);
        }


        [Fact]
        [Trait("CostCategory", "Get")]
        public async void When_UnExsitingCostCategory_Return_Status404NotFound()
        {
            Mock<IMediator> mockMediator = new Mock<IMediator>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            CostCategoriesController costCategoriesController = new CostCategoriesController(mockMediator.Object, mockMapper.Object);

            GetCostCategoryByIdQuery getCostCategoryByIdQuery = new GetCostCategoryByIdQuery();
            getCostCategoryByIdQuery.Id = It.IsAny<int>();
            mockMediator
                .Setup(q => q.Send(getCostCategoryByIdQuery, CancellationToken.None))
                .ReturnsAsync((GetCostCategoryResponse)null);

            IActionResult result = await costCategoriesController.Get(getCostCategoryByIdQuery, CancellationToken.None);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }


        [Fact]
        [Trait("CostCategory", "Get")]
        public async void When_ExsitingCostCategory_Return_Status200OK()
        {
            Mock<IMediator> mockMediator = new Mock<IMediator>();
            Mock<IMapper> mockMapper = new Mock<IMapper>();
            CostCategoriesController costCategoriesController = new CostCategoriesController(mockMediator.Object, mockMapper.Object);

            GetCostCategoryByIdQuery getCostCategoryByIdQuery = new GetCostCategoryByIdQuery();
            getCostCategoryByIdQuery.Id = It.IsAny<int>();
            mockMediator
                .Setup(q => q.Send(getCostCategoryByIdQuery, CancellationToken.None))
                .ReturnsAsync(new GetCostCategoryResponse()
                {
                    Id = 1,
                    Title = "category A"
                });

            IActionResult result = await costCategoriesController.Get(getCostCategoryByIdQuery, CancellationToken.None);

            var okObjectResult = result as OkObjectResult;

            // assert
            Assert.NotNull(okObjectResult);
            Assert.Equal(StatusCodes.Status200OK, okObjectResult.StatusCode);
        }
    }
}
