using Microsoft.EntityFrameworkCore;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Data.Repository
{
    public class EducationalOrganisationImportRepository : IEducationalOrganisationImportRepository
    {
        private readonly EducationalOrganisationDataContext _dataContext;

        public EducationalOrganisationImportRepository(EducationalOrganisationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task InsertMany(IEnumerable<EducationalOrganisationImport> educationalOrganisationImports)
        {
            await _dataContext.EducationalOrganisationImport.AddRangeAsync(educationalOrganisationImports);

            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<EducationalOrganisationImport>> GetAll()
        {
            var results = await _dataContext.EducationalOrganisationImport.ToListAsync();
            return results;
        }

        public async Task DeleteAll()
        {
            _dataContext.EducationalOrganisationImport.RemoveRange(_dataContext.EducationalOrganisationImport);
            await _dataContext.SaveChangesAsync();
        }
    }
}