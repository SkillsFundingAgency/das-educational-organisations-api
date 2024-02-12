using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Data.Repository
{
    public class ImportAuditRepository : IImportAuditRepository
    {
        private readonly EducationalOrganisationDataContext _dataContext;

        public ImportAuditRepository(EducationalOrganisationDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Insert(ImportAudit importAudit)
        {
            await _dataContext.ImportAudit.AddAsync(importAudit);
            _dataContext.SaveChanges();
        }
    }
}
