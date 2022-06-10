using Costs.Api.Controllers;
using Costs.Application.Features.CostCategoryFeatures.Commands.DeleteCostCategory.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Costs.Api.Test.Controllers.CostCategoriesControllerTests
{
    public class DeleteTests
    {
        private readonly CostCategoriesController _costCategoriesController;
        private readonly Mock<IMediator> _mediator;
        private readonly GetByIdCommand _getByIdCommand;
        public DeleteTests()
        {
            _mediator = new Mock<IMediator>();
            _costCategoriesController = new CostCategoriesController(_mediator.Object);
            _getByIdCommand = new GetByIdCommand()
            {
                Id = It.IsAny<int>(),
            };
        }


        [Fact]
        [Trait("CostCategory", "CostCategoriesController")]
        public async Task When_Call_ActionMethod_Invoke_GetByIdCommandHandler_ExactlyOnce()
        {
            _mediator.Setup(q => q.Send(_getByIdCommand, CancellationToken.None))
                .ReturnsAsync(new GetByIdResponse());

            IActionResult result = await _costCategoriesController.Delete(_getByIdCommand, CancellationToken.None);

            _mediator.Verify(handler => handler.Send(_getByIdCommand, CancellationToken.None));
        }


        [Fact]
        [Trait("CostCategory", "CostCategoriesController")]
        public async Task When_Call_ActionMethod_If_ExistingItem_Invoke_DeleteCostCategoryCommandHandler_ExactlyOnce()
        {
            _mediator.Setup(q => q.Send(_getByIdCommand, CancellationToken.None))
                .ReturnsAsync(new GetByIdResponse());

            IActionResult result = await _costCategoriesController.Delete(_getByIdCommand, CancellationToken.None);

            var okObjectResult = result as OkObjectResult;
            //DeleteCostCategoryCommand

            _mediator.Verify(handler => handler.Send(_getByIdCommand, CancellationToken.None));
        }


        [Fact]
        [Trait("CostCategory", "CostCategoriesController")]
        public async Task When_UnExsitingCostCategory_Return_Status404NotFound()
        {
            _mediator.Setup(q => q.Send(It.IsAny<int>(), CancellationToken.None))
                .ReturnsAsync(new GetByIdResponse());

            IActionResult result = await _costCategoriesController.Delete(_getByIdCommand, CancellationToken.None);

            NotFoundResult notFoundResult = result as NotFoundResult;

            Assert.NotNull(notFoundResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
    }
}
