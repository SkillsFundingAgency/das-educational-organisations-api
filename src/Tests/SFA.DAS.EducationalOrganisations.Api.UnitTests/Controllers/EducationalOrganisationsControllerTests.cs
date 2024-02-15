using System.Net;
using AutoFixture.NUnit3;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Api.Controllers;
using SFA.DAS.EducationalOrganisations.Api.Responses;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetAllEducationalOrganisations;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetEducationalOrganisationById;
using SFA.DAS.EducationalOrganisations.Application.Queries.SearchEducationalOrganisations;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EducationalOrganisations.Api.UnitTests.Controllers
{
    public class EducationalOrganisationsControllerTests
    {
        [Test, MoqAutoData]
        public async Task Then_Calls_GetAll(
         [Frozen] Mock<IMediator> mockMediator,
         [Greedy] EducationalOrganisationsController controller,
            GetAllEducationalOrganisationsResult result)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetAllEducationalOrganisationsQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            var controllerResult = await controller.GetAll() as ObjectResult;

            controllerResult.Should().NotBeNull();

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var model = controllerResult.Value as GetAllEducationalOrganisationsResponse;
        }

        [Test, MoqAutoData]
        public async Task Then_Calls_GetById_Returns_From_Mediator(
        [Frozen] Mock<IMediator> mockMediator,
        [Greedy] EducationalOrganisationsController controller,
           GetEducationalOrganisationByIdResult result,
           Guid id)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetEducationalOrganisationByIdQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            var controllerResult = await controller.GetById(id) as ObjectResult;

            controllerResult.Should().NotBeNull();

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var model = controllerResult.Value as GetEducationalOrganisationByIdResponse;
        }

        [Test, MoqAutoData]
        public async Task Then_Calls_GetById_Returns_Empty_From_Mediator(
          [Frozen] Mock<IMediator> mockMediator,
          [Greedy] EducationalOrganisationsController controller,
             Guid id)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<GetEducationalOrganisationByIdQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new GetEducationalOrganisationByIdResult { EducationalOrganisation = null });

            var controllerResult = await controller.GetById(id) as ObjectResult;

            controllerResult.Should().NotBeNull();

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var model = controllerResult.Value as GetEducationalOrganisationByIdResponse;
            model.EducationalOrganisation.Should().BeNull();
        }

        [Test, MoqAutoData]
        public async Task Then_Calls_Search_Returns_From_Mediator(
       [Frozen] Mock<IMediator> mockMediator,
       [Greedy] EducationalOrganisationsController controller,
          SearchEducationalOrganisationsResult result,
          SearchEducationalOrganisationsQuery query)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny< SearchEducationalOrganisationsQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            var controllerResult = await controller.Search(query.SearchTerm, query.MaximumResults) as ObjectResult;

            controllerResult.Should().NotBeNull();

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var model = controllerResult.Value as SearchEducationalOrganisationsResponse;
        }

        [Test, MoqAutoData]
        public async Task Then_Calls_Search_Returns_Empty_From_Mediator(
          [Frozen] Mock<IMediator> mockMediator,
          [Greedy] EducationalOrganisationsController controller,
             SearchEducationalOrganisationsQuery query)
        {
            mockMediator
                .Setup(mediator => mediator.Send(
                    It.IsAny<SearchEducationalOrganisationsQuery>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new SearchEducationalOrganisationsResult
                {
                    EducationalOrganisations = new List<EducationalOrganisationEntity>()
                });

            var controllerResult = await controller.Search(query.SearchTerm, query.MaximumResults) as ObjectResult;

            controllerResult.Should().NotBeNull();

            controllerResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            var model = controllerResult.Value as SearchEducationalOrganisationsResponse;
            model.EducationalOrganisations.Count().Should().Be(0);
        }
    }
}
