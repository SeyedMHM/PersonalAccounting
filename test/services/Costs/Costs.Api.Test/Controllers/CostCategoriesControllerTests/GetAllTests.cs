using Costs.Api.Controllers;
using Costs.Api.Test.Fixtures;
using Costs.Application.Features.CostCategoryFeatures.Queries.Common;
using Costs.Application.Features.CostCategoryFeatures.Queries.GetAllCostCategories;
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
    public class GetAllTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly CostCategoriesController _costCategoriesController;
        private readonly GetAllCostCategoriesQuery _getAllCostCategoriesQuery;

        public GetAllTests()
        {
            _mediator = new Mock<IMediator>();
            _costCategoriesController = new CostCategoriesController(_mediator.Object);
            _getAllCostCategoriesQuery = new GetAllCostCategoriesQuery();
        }


        [Fact]
        [Trait("CostCategory", "CostCategoriesController")]
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
        [Trait("CostCategory", "CostCategoriesController")]
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
        [Trait("CostCategory", "CostCategoriesController")]
        public async Task When_ExistingCostCategories_Return_ExactlyExistingCostCategories()
        {
            //به این علت دوبار مقادیر دسته بندی ها از فایل فیکسچر خوانده شده که وقتی نتیجه 
            //داخل متد کانفیگ مدیاتور اضافه می شود، مقدار مورد نظر چون رفرنرس تایپ هست، تغییر می کند
            //پس باید مقدار پایه با مقداری که به داخل کانفیگ ارسال می شود متفاوت باشد
            List<GetCostCategoryResponse> fixtureResult = await CostCategoryFixture.GetAllCostCategoriesTest();
            List<GetCostCategoryResponse> fixtureResultAfterReturnFromController = await CostCategoryFixture.GetAllCostCategoriesTest();
            _mediator
                .Setup(q => q.Send(_getAllCostCategoriesQuery, CancellationToken.None))
                .ReturnsAsync(fixtureResultAfterReturnFromController);

            IActionResult result = await _costCategoriesController.GetAll(_getAllCostCategoriesQuery, CancellationToken.None);

            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            Assert.IsType(typeof(List<GetCostCategoryResponse>), okObjectResult.Value);
            Assert.Equal(JsonSerializer.Serialize(fixtureResult), JsonSerializer.Serialize(fixtureResultAfterReturnFromController));
        }


        [Fact]
        [Trait("CostCategory", "CostCategoriesController")]
        public async Task When_NotExistingCostCategories_Return_Status404NotFound()
        {
            _mediator
                .Setup(q => q.Send(_getAllCostCategoriesQuery, CancellationToken.None))
                .ReturnsAsync(new List<GetCostCategoryResponse>());

            IActionResult result = await _costCategoriesController.GetAll(_getAllCostCategoriesQuery, CancellationToken.None);

            var notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }


        [Fact]
        [Trait("CostCategory", "CostCategoriesController")]
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
