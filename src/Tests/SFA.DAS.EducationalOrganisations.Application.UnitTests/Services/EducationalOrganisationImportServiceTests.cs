using AutoFixture;
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
    public class EducationalOrganisationImportServiceTests
    {
        private Mock<ILogger<EducationalOrganisationImportService>> _loggerMock;
        private Mock<IEducationalOrganisationImportRepository> _educationalOrganisationImportRepositoryMock;
        private EducationalOrganisationImportService _educationalOrganisationImportService;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<EducationalOrganisationImportService>>();
            _educationalOrganisationImportRepositoryMock = new Mock<IEducationalOrganisationImportRepository>();
            _fixture = new Fixture();

            _educationalOrganisationImportService = new EducationalOrganisationImportService(
                _loggerMock.Object,
                _educationalOrganisationImportRepositoryMock.Object);
        }

        [Test]
        public async Task GetAll_ShouldReturnAllEntitiesFromRepository()
        {
            // Arrange
            var expectedEntities = _fixture.CreateMany<EducationalOrganisationImport>().ToList();
            _educationalOrganisationImportRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(expectedEntities);

            // Act
            var result = await _educationalOrganisationImportService.GetAll();

            // Assert
            result.Should().BeEquivalentTo(expectedEntities);
        }

        [Test]
        public async Task ClearStagingData_ShouldCallDeleteAllMethodInRepository()
        {
            // Act
            await _educationalOrganisationImportService.ClearStagingData();

            // Assert
            _educationalOrganisationImportRepositoryMock.Verify(repo => repo.DeleteAll(), Times.Once);
        }

        [Test]
        public async Task InsertDataIntoStaging_ShouldCallInsertManyMethodInRepository()
        {
            // Arrange
            var organisations = _fixture.CreateMany<EducationalOrganisationImport>().ToList();

            // Act
            var result = await _educationalOrganisationImportService.InsertDataIntoStaging(organisations);

            // Assert
            _educationalOrganisationImportRepositoryMock.Verify(repo => repo.InsertMany(organisations), Times.Once);
            result.Should().BeTrue();
        }

        [Test]
        public async Task InsertDataIntoStaging_ShouldReturnFalse_WhenRepositoryThrowsException()
        {
            // Arrange
            var organisations = _fixture.CreateMany<EducationalOrganisationImport>().ToList();
            var exception = new Exception("Simulated repository exception");
            _educationalOrganisationImportRepositoryMock.Setup(repo => repo.InsertMany(organisations)).Throws(exception);

            // Act
            var result = await _educationalOrganisationImportService.InsertDataIntoStaging(organisations);

            // Assert
            result.Should().BeFalse();
        }
    }
}
