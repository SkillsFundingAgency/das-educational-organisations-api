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
        public async Task ImportDataIntoStaging_ShouldImportDataAndReturnTrue()
        {
            // Arrange
            var organisations = _fixture.CreateMany<EducationalOrganisationEntity>().ToList();

            // Act
            var result = await _educationalOrganisationImportService.ImportDataIntoStaging(organisations);

            // Assert
            result.Should().BeTrue();
            _educationalOrganisationImportRepositoryMock.Verify(repo => repo.DeleteAll(), Times.Once);
            _educationalOrganisationImportRepositoryMock.Verify(repo => repo.InsertMany(It.IsAny<List<EducationalOrganisationImport>>()), Times.Once);
        }

        [Test]
        public void ImportDataIntoStaging_ShouldLogErrorAndRethrowException_WhenExceptionOccurs()
        {
            // Arrange
            var organisations = _fixture.CreateMany<EducationalOrganisationEntity>().ToList();
            var expectedException = new InvalidOperationException("Simulated error");

            _educationalOrganisationImportRepositoryMock.Setup(repo => repo.DeleteAll()).Throws(expectedException);

            // Act & Assert
            Func<Task> act = async () => await _educationalOrganisationImportService.ImportDataIntoStaging(organisations);

            // Assert
            act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Simulated error");
        }
    }
}
