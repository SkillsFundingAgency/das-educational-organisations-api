using Microsoft.EntityFrameworkCore;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Data.Repository
{
    public class EducationalOrganisationEntityRepository : IEducationalOrganisationEntityRepository
    {
        private readonly EducationalOrganisationDataContext _dataContext;

        public EducationalOrganisationEntityRepository(EducationalOrganisationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InsertMany(IEnumerable<EducationalOrganisationEntity> educationalOrganisationEntitys)
        {
            await _dataContext.EducationalOrganisationEntities.AddRangeAsync(educationalOrganisationEntitys);

            _dataContext.SaveChanges();
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

        public async Task<IEnumerable<EducationalOrganisationEntity>> SearchByName(string searchTerm)
        {
            return await _dataContext.EducationalOrganisationEntities
                              .Where(x => x.Name.Contains(searchTerm))
                              .ToListAsync();
        }
        
        public async Task<IEnumerable<EducationalOrganisationEntity>> SearchByURN(string urn)
        {
            return await _dataContext.EducationalOrganisationEntities
                              .Where(x => x.URN.Contains(urn))
                              .ToListAsync();
        }

        public void DeleteAll()
        {
            _dataContext.EducationalOrganisationEntities.RemoveRange(_dataContext.EducationalOrganisationEntities);
            _dataContext.SaveChanges();
        }
    }
}
