using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Domain.Interfaces
{
    public interface IEducationalOrganisationEntityRepository
    {
        Task InsertMany(IEnumerable<EducationalOrganisationEntity> educationalOrganisationEntitys);
        Task<IEnumerable<EducationalOrganisationEntity>> GetAll();
        Task<EducationalOrganisationEntity?> GetById(Guid id);
        Task<IEnumerable<EducationalOrganisationEntity>> SearchByName(string searchTerm);
        Task<IEnumerable<EducationalOrganisationEntity>> SearchByUrn(string urn);
        void DeleteAll();
    }
}
