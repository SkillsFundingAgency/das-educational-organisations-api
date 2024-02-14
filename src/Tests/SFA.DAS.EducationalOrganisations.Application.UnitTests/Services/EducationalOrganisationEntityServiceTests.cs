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
    public class EducationalOrganisationEntityServiceTests
    {
        private Mock<ILogger<EducationalOrganisationEntityService>> _loggerMock;
        private Mock<IEducationalOrganisationEntityRepository> _educationalOrganisationEntityRepositoryMock;
        private Mock<IImportAuditRepository> _importAuditRepositoryMock;
        private EducationalOrganisationEntityService _educationalOrganisationEntityService;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<EducationalOrganisationEntityService>>();
            _educationalOrganisationEntityRepositoryMock = new Mock<IEducationalOrganisationEntityRepository>();
            _importAuditRepositoryMock = new Mock<IImportAuditRepository>();
            _fixture = new Fixture();

            _educationalOrganisationEntityService = new EducationalOrganisationEntityService(
                _loggerMock.Object,
                _educationalOrganisationEntityRepositoryMock.Object,
                _importAuditRepositoryMock.Object);
        }

        [Test]
        public async Task PopulateDataFromStaging_ShouldPopulateDataAndCreateAudit_WhenImportOrgsNotEmpty()
        {
            // Arrange
            var importOrgs = _fixture.CreateMany<EducationalOrganisationImport>().ToList();
            var importStartTime = DateTime.UtcNow;

            // Act
            await _educationalOrganisationEntityService.PopulateDataFromStaging(importOrgs, importStartTime);

            // Assert
            _educationalOrganisationEntityRepositoryMock.Verify(repo => repo.DeleteAll(), Times.Once);
            _educationalOrganisationEntityRepositoryMock.Verify(repo => repo.InsertMany(It.IsAny<List<EducationalOrganisationEntity>>()), Times.Once);
            _importAuditRepositoryMock.Verify(repo => repo.Insert(It.IsAny<ImportAudit>()), Times.Once);
        }

        [Test]
        public async Task PopulateDataFromStaging_ShouldNotPerformActions_WhenImportOrgsEmpty()
        {
            // Arrange
            var importOrgs = Enumerable.Empty<EducationalOrganisationImport>();
            var importStartTime = DateTime.UtcNow;

            // Act
            await _educationalOrganisationEntityService.PopulateDataFromStaging(importOrgs, importStartTime);

            // Assert
            _educationalOrganisationEntityRepositoryMock.Verify(repo => repo.DeleteAll(), Times.Never);
            _educationalOrganisationEntityRepositoryMock.Verify(repo => repo.InsertMany(It.IsAny<List<EducationalOrganisationEntity>>()), Times.Never);
            _importAuditRepositoryMock.Verify(repo => repo.Insert(It.IsAny<ImportAudit>()), Times.Never);
        }

        [Test]
        public void PopulateDataFromStaging_ShouldLogErrorAndRethrowException_WhenExceptionOccurs()
        {
            // Arrange
            var importOrgs = _fixture.CreateMany<EducationalOrganisationImport>().ToList();
            var importStartTime = DateTime.UtcNow;
            var expectedException = new InvalidOperationException("Simulated error");

            _educationalOrganisationEntityRepositoryMock.Setup(repo => repo.DeleteAll()).Throws(expectedException);

            // Act & Assert
            Func<Task> act = async () => await _educationalOrganisationEntityService.PopulateDataFromStaging(importOrgs, importStartTime);

            // Assert
            act.Should().ThrowAsync<InvalidOperationException>().WithMessage("Simulated error");
        }
    }
}
