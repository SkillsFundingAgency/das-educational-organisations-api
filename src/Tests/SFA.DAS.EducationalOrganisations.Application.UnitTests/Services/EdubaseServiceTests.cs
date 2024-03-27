using System.Reflection;
using AutoFixture;
using EdubaseSoap;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Application.Services;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.UnitTests.Services
{
    [TestFixture]
    public class EdubaseServiceTests
    {
        private Mock<ILogger<EdubaseService>> _loggerMock;
        private Mock<IEdubaseSoapService> _edubaseSoapServiceMock;
        private Mock<IEducationalOrganisationImportService> _educationalOrganisationImportService;
        private EdubaseService _edubaseService;
        private Fixture _fixture;
        private FindEstablishmentsResponse _response;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<EdubaseService>>();
            _edubaseSoapServiceMock = new Mock<IEdubaseSoapService>();
            _educationalOrganisationImportService = new Mock<IEducationalOrganisationImportService>();

            _fixture = new Fixture();

            var establishments = _fixture.CreateMany<Establishment>().ToList();
            _response = _fixture.Build<FindEstablishmentsResponse>()
                .With(x => x.PageCount, 1)
                .With(x => x.Establishments, [.. establishments])
                .Create();

            _edubaseService = new EdubaseService(_loggerMock.Object, _edubaseSoapServiceMock.Object, _educationalOrganisationImportService.Object);
        }

        [Test]
        public async Task PopulateStagingEducationalOrganisations_ShouldReturnFalse_WhenNoEstablishments()
        {
            // Arrange
            _edubaseSoapServiceMock.Setup(client => client.FindEstablishmentsAsync(It.IsAny<EstablishmentFilter>()))
                .ReturnsAsync(new FindEstablishmentsResponse());

            // Act
            var result = await _edubaseService.PopulateStagingEducationalOrganisations();

            // Assert
            result.Should().Be(false);
        }

        [Test]
        public async Task PopulateStagingEducationalOrganisations_ShouldReturnFalse_WhenNullResponse()
        {
            // Arrange
            _edubaseSoapServiceMock.Setup(client => client.FindEstablishmentsAsync(It.IsAny<EstablishmentFilter>()))
                .ReturnsAsync((FindEstablishmentsResponse)null);

            // Act
            var result = await _edubaseService.PopulateStagingEducationalOrganisations();

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public async Task PopulateStagingEducationalOrganisations_ShouldReturnTrue_WhenEstablishmentsFound()
        {
            // Arrange
            _edubaseSoapServiceMock.Setup(client => client.FindEstablishmentsAsync(It.IsAny<EstablishmentFilter>()))
                .ReturnsAsync(_response);

            // Act
            var result = await _edubaseService.PopulateStagingEducationalOrganisations();

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task FindEstablishmentsAndInsertIntoStaging_ShouldInsertDataIntoStaging()
        {
            // Arrange
            var filter = new EstablishmentFilter();
            _edubaseSoapServiceMock.Setup(client => client.FindEstablishmentsAsync(It.IsAny<EstablishmentFilter>()))
                .ReturnsAsync(_response);

            // Act
            await _edubaseService.PopulateStagingEducationalOrganisations();

            // Assert
            _educationalOrganisationImportService.Verify(service => service.InsertDataIntoStaging(It.IsAny<List<EducationalOrganisationImport>>()), Times.Once);
        }

        [Test]
        public async Task InsertIntoDatabaseAsync_ShouldCallInsertDataIntoStagingMethod()
        {
            // Arrange
            var establishments = _fixture.CreateMany<Establishment>().ToList();

            var insertIntoDatabaseAsyncMethod = typeof(EdubaseService)
                .GetMethod("InsertIntoDatabaseAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            // Act
            await (Task)insertIntoDatabaseAsyncMethod.Invoke(_edubaseService, new object[] { establishments });

            // Assert
            _educationalOrganisationImportService.Verify(service => service.InsertDataIntoStaging(It.IsAny<List<EducationalOrganisationImport>>()), Times.Once);
        }
    }
}
