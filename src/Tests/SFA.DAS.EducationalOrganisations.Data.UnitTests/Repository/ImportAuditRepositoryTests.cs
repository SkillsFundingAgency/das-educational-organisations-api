using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Data.UnitTests.Repository
{
    public class ImportAuditRepositoryTests
    {
        private Data.Repository.ImportAuditRepository _repository;
        private EducationalOrganisationDataContext _DB;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _DB = new EducationalOrganisationDataContext(new DbContextOptionsBuilder<EducationalOrganisationDataContext>()
                                                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                                    .Options);

            _repository = new Data.Repository.ImportAuditRepository(_DB);
            _fixture = new Fixture();
        }

        [Test]
        public async Task Insert_ShouldInsertAndSaveChangesAsync()
        {
            // Arrange
            var audit = _fixture.Create<ImportAudit>();

            // Act
            await _repository.Insert(audit);

            // Assert
            _DB.ImportAudit.Should().HaveCount(1);

            var entity = _DB.ImportAudit.FirstOrDefault();
            entity.Should().NotBeNull();
            entity.Id.Should().Be(audit.Id);
        }
    }
}
