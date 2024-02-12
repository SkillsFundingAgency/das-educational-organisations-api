using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Domain.Interfaces
{
    public interface IEducationalOrganisationImportRepository
    {
        void DeleteAll();
        Task<IEnumerable<EducationalOrganisationImport>> GetAll();
        Task InsertMany(IEnumerable<EducationalOrganisationImport> educationalOrganisationImports);
    }
}
