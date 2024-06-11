using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Domain.Interfaces
{
    public interface IEducationalOrganisationImportService
    {
        Task<bool> InsertDataIntoStaging(List<EducationalOrganisationImport> organisations);
        Task<IEnumerable<EducationalOrganisationImport>> GetAll();
        Task ClearStagingData();
    }
}