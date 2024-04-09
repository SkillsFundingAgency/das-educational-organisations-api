using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Data.Repository;
using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Data.UnitTests.Repository
{
    public class EducationalOrganisationEntityRepositoryTests
    {
        private Data.Repository.EducationalOrganisationEntityRepository _repository;
        private EducationalOrganisationDataContext _DB;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _DB = new EducationalOrganisationDataContext(new DbContextOptionsBuilder<EducationalOrganisationDataContext>()
                                                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                                    .Options);

            _repository = new EducationalOrganisationEntityRepository(_DB);
            _fixture = new Fixture();
        }

        [Test]
        public async Task InsertMany_ShouldInsertEntitiesAndSaveChangesAsync()
        {
            // Arrange
            var entities = _fixture.CreateMany<EducationalOrganisationEntity>();

            await _repository.DeleteAll();

            // Act
            await _repository.InsertMany(entities);

            // Assert
            _DB.EducationalOrganisationEntities.Should().HaveCount(entities.Count());
        }

        [Test]
        public async Task GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var entities = _fixture.CreateMany<EducationalOrganisationEntity>();

            await _repository.InsertMany(entities);
            // Act
            var result = await _repository.GetAll();

            // Assert
            result.Should().BeEquivalentTo(entities);
        }

        [Test]
        public async Task GetById_ShouldReturnEntityById()
        {
            // Arrange
            var entityId = Guid.NewGuid();
            var entity = _fixture.Build<EducationalOrganisationEntity>().With(e => e.Id, entityId).Create();

            var entities = new List<EducationalOrganisationEntity>()
            {
                entity
            };

            await _repository.InsertMany(entities);
            // Act
            var result = await _repository.GetById(entityId);

            // Assert
            result.Should().BeEquivalentTo(entity);
        }

        [Test]
        public async Task SearchByName_ShouldReturnMatchingEntities()
        {
            // Arrange
            var searchTerm = "Test";
            var maximumResults = 5;
            var entities = _fixture.CreateMany<EducationalOrganisationEntity>();

            await _repository.InsertMany(entities);

            // Act
            var result = await _repository.SearchByName(searchTerm, maximumResults);

            // Assert
            result.Should().BeEquivalentTo(entities.Where(x => x.Name.Contains(searchTerm)).Take(maximumResults));
        }

        [Test]
        public async Task SearchByUrn_ShouldReturnMatchingEntities()
        {
            // Arrange
            var urn = "TestUrn";
            var entities = _fixture.CreateMany<EducationalOrganisationEntity>();

            await _repository.InsertMany(entities);

            // Act
            var result = await _repository.SearchByUrn(urn);

            // Assert
            result.Should().BeEquivalentTo(entities.Where(x => x.URN.Contains(urn)));
        }

        [Test]
        public async Task DeleteAll_ShouldRemoveAllEntitiesAndSaveChanges()
        {
            // Act
            await _repository.DeleteAll();

            _DB.EducationalOrganisationEntities.Should().HaveCount(0);
        }
    }
}
