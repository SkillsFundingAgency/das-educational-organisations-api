using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Domain.Interfaces
{
    public interface IEducationalOrganisationImportService
    {
        Task<bool> ImportDataIntoStaging(IEnumerable<EducationalOrganisationEntity> organisations);
        Task<IEnumerable<EducationalOrganisationImport>> GetAll();
    }
}