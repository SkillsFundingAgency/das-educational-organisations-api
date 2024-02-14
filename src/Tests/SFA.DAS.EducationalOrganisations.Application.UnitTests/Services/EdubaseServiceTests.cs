using AutoFixture;
using EdubaseSoap;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Application.Services;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.UnitTests.Services
{
    [TestFixture]
    public class EdubaseServiceTests
    {
        private Mock<ILogger<EdubaseService>> _loggerMock;
        private Mock<IEdubaseClient> _edubaseClientMock;
        private Mock<IEdubaseClientFactory> _edubaseClientFactoryMock;
        private EdubaseService _edubaseService;
        private Fixture _fixture;
        private FindEstablishmentsResponse _response;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<EdubaseService>>();
            _edubaseClientMock = new Mock<IEdubaseClient>();
            _edubaseClientFactoryMock = new Mock<IEdubaseClientFactory>();
            _edubaseClientFactoryMock.Setup(factory => factory.Create()).Returns(_edubaseClientMock.Object);

            _fixture = new Fixture();

            var establishments = _fixture.CreateMany<Establishment>().ToList();
            _response = _fixture.Build<FindEstablishmentsResponse>()
                .With(x => x.PageCount, 1)
                .With(x => x.Establishments, [.. establishments])
                .Create();

            _edubaseService = new EdubaseService(_loggerMock.Object, _edubaseClientFactoryMock.Object);
        }

        [Test]
        public async Task GetOrganisations_ShouldReturnEmptyList_WhenNoEstablishments()
        {
            // Arrange
            _edubaseClientMock.Setup(client => client.FindEstablishmentsAsync(It.IsAny<FindEstablishmentsRequest>()))
                .ReturnsAsync(new FindEstablishmentsResponse());

            // Act
            var result = await _edubaseService.GetOrganisations();

            // Assert
            result.Should().BeEmpty();
        }

        [Test]
        public void GetOrganisations_ShouldThrowException_WhenEdubaseApiFails()
        {
            // Arrange
            _edubaseClientMock.Setup(client => client.FindEstablishmentsAsync(It.IsAny<FindEstablishmentsRequest>()))
                .ThrowsAsync(new Exception("Simulated error"));

            // Act & Assert
            Func<Task> act = async () => await _edubaseService.GetOrganisations();
            act.Should().ThrowAsync<Exception>();
        }

        [Test]
        public async Task GetOrganisations_ShouldReturnMappedOrganisations_WhenEstablishmentsExist()
        {
            // Arrange
            _edubaseClientMock.Setup(client => client.FindEstablishmentsAsync(It.IsAny<FindEstablishmentsRequest>()))
                .ReturnsAsync(_response);

            // Act
            var result = await _edubaseService.GetOrganisations();

            // Assert
            result.Should().HaveCount(_response.Establishments.Count);
        }
    }
}
