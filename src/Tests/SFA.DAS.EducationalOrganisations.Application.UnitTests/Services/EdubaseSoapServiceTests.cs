using System.Net.Sockets;
using AutoFixture;
using AutoFixture.NUnit3;
using EdubaseSoap;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Application.Services;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EducationalOrganisations.Application.UnitTests.Services
{
    [TestFixture]
    public class EdubaseSoapServiceTests
    {     
        [Test, MoqAutoData]
        public async Task FindEstablishmentsAsync_ShouldReturnResponse(
            [Frozen] Mock<IEdubaseClient> edubaseClientMock,
            [Greedy] EdubaseSoapService edubaseSoapService,
            EstablishmentFilter filter,
            FindEstablishmentsResponse expectedResponse           
            )
        {
            // Arrange
            edubaseClientMock.Setup(client => client.FindEstablishmentsAsync(It.IsAny<FindEstablishmentsRequest>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await edubaseSoapService.FindEstablishmentsAsync(filter);

            // Assert
            result.Should().BeEquivalentTo(expectedResponse);
            edubaseClientMock.Verify(client => client.FindEstablishmentsAsync(It.IsAny<FindEstablishmentsRequest>()), Times.Once);
        }

        [Test, MoqAutoData]
        public void FindEstablishmentsAsync_ShouldNotRetryOnNonSocketException(
             [Frozen] Mock<IEdubaseClient> edubaseClientMock,
            [Greedy] EdubaseSoapService edubaseSoapService,
            EstablishmentFilter filter,
            FindEstablishmentsResponse expectedResponse)
        {
            // Arrange
            edubaseClientMock.Setup(client => client.FindEstablishmentsAsync(It.IsAny<FindEstablishmentsRequest>()))
                .ThrowsAsync(new InvalidOperationException());

            // Act
            Func<Task> act = async () => await edubaseSoapService.FindEstablishmentsAsync(filter);

            // Assert
            act.Should().ThrowAsync<InvalidOperationException>();
            edubaseClientMock.Verify(client => client.FindEstablishmentsAsync(It.IsAny<FindEstablishmentsRequest>()), Times.Once);
        }
    }
}
