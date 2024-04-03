using Castle.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Data.Repository
{
    public class EducationalOrganisationEntityRepository : IEducationalOrganisationEntityRepository
    {
        private readonly EducationalOrganisationDataContext _dataContext;
        private readonly ILogger<EducationalOrganisationEntityRepository> _logger;

        public EducationalOrganisationEntityRepository(EducationalOrganisationDataContext dataContext, ILogger<EducationalOrganisationEntityRepository> logger)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public async Task InsertMany(IEnumerable<EducationalOrganisationEntity> educationalOrganisationEntities)
        {
            await _dataContext.EducationalOrganisationEntities.AddRangeAsync(educationalOrganisationEntities);

            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<EducationalOrganisationEntity>> GetAll()
        {
            var results = await _dataContext.EducationalOrganisationEntities.ToListAsync();
            return results;
        }

        public async Task<EducationalOrganisationEntity?> GetById(Guid id)
        {
            return await _dataContext.EducationalOrganisationEntities.FindAsync(id);
        }

        public async Task<IEnumerable<EducationalOrganisationEntity>> SearchByName(string searchTerm, int maximumResults)
        {
            var connString = _dataContext.Database.GetConnectionString();
            _logger.LogInformation("ConnectionString in repo: {conn}", connString);

            var organisations = await _dataContext.EducationalOrganisationEntities
                              .Where(x => x.Name.Contains(searchTerm))
                              .ToListAsync();

            return organisations.Take(maximumResults);
        }

        public async Task<IEnumerable<EducationalOrganisationEntity>> SearchByUrn(string urn)
        {
            return await _dataContext.EducationalOrganisationEntities
                              .Where(x => x.URN.Contains(urn))
                              .ToListAsync();
        }

        public async Task<EducationalOrganisationEntity?> FindByUrn(string urn)
        {
            return await _dataContext.EducationalOrganisationEntities
                              .Where(x => x.URN.Contains(urn)).FirstOrDefaultAsync();
        }

        public async Task DeleteAll()
        {
            _dataContext.EducationalOrganisationEntities.RemoveRange(_dataContext.EducationalOrganisationEntities);
            await _dataContext.SaveChangesAsync();
        }
    }
}