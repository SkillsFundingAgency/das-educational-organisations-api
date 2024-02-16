using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Domain.Interfaces
{
    public interface IEducationalOrganisationEntityRepository
    {
        Task InsertMany(IEnumerable<EducationalOrganisationEntity> educationalOrganisationEntities);
        Task<IEnumerable<EducationalOrganisationEntity>> GetAll();
        Task<EducationalOrganisationEntity?> GetById(Guid id);
        Task<IEnumerable<EducationalOrganisationEntity>> SearchByName(string searchTerm, int maximumResults);
        Task<IEnumerable<EducationalOrganisationEntity>> SearchByUrn(string urn);
        Task<EducationalOrganisationEntity?> FindByUrn(string urn);
        void DeleteAll();
    }
}
