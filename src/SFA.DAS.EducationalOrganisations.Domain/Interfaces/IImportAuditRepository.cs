using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Domain.Interfaces
{
    public interface IImportAuditRepository
    {
        Task Insert(ImportAudit importAudit);
    }
}
