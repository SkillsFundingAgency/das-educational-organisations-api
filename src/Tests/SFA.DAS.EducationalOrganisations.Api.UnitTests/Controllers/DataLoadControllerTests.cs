using System.Net;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Api.Controllers;
using SFA.DAS.EducationalOrganisations.Application.Commands.ImportEducationalOrganisations;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EducationalOrganisations.Api.UnitTests.Controllers
{
    public class DataLoadControllerTests
    {
        [Test, MoqAutoData]
        public async Task Then_Calls_Import(
         [Frozen] Mock<IMediator> mockMediator,
         [Greedy] DataLoadController controller)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<ImportEducationalOrganisationsCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(Unit.Value);

            var controllerResult = await controller.Import() as NoContentResult;

            controllerResult.Should().NotBeNull();

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

        [Test, MoqAutoData]
        public async Task Then_Throws_On_Import(
         [Frozen] Mock<IMediator> mockMediator,
         [Greedy] DataLoadController controller,
          Exception simulatedException)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<ImportEducationalOrganisationsCommand>(),
                    It.IsAny<CancellationToken>()))
               .Throws(simulatedException);

            var controllerResult = await controller.Import() as ObjectResult;

            controllerResult.Should().NotBeNull();

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}
