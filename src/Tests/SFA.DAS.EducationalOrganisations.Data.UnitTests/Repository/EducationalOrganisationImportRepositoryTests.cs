using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Data.UnitTests.Repository
{
    public class EducationalOrganisationImportRepositoryTests
    {
        private Data.Repository.EducationalOrganisationImportRepository _repository;
        private EducationalOrganisationDataContext _DB;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _DB = new EducationalOrganisationDataContext(new DbContextOptionsBuilder<EducationalOrganisationDataContext>()
                                                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                                    .Options);

            _repository = new Data.Repository.EducationalOrganisationImportRepository(_DB);
            _fixture = new Fixture();
        }

        [Test]
        public async Task InsertMany_ShouldInsertEntitiesAndSaveChangesAsync()
        {
            // Arrange
            var entities = _fixture.CreateMany<EducationalOrganisationImport>();

            await _repository.DeleteAll();

            // Act
            await _repository.InsertMany(entities);

            // Assert
            _DB.EducationalOrganisationImport.Should().HaveCount(entities.Count());
        }

        [Test]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var entities = _fixture.CreateMany<EducationalOrganisationImport>();

            await _repository.InsertMany(entities);
            // Act
            var result = await _repository.GetAll();

            // Assert
            result.Should().BeEquivalentTo(entities);
        }

        [Test]
        public async Task DeleteAll_ShouldRemoveAllEntitiesAndSaveChanges()
        {
            // Act
            await _repository.DeleteAll();

            _DB.EducationalOrganisationImport.Should().HaveCount(0);
        }
    }
}
