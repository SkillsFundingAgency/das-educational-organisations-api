using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Domain.Interfaces
{
    public interface IEducationalOrganisationEntityService
    {
        Task PopulateDataFromStaging(IEnumerable<EducationalOrganisationImport> importOrgs, DateTime importStartTime);
    }
}
