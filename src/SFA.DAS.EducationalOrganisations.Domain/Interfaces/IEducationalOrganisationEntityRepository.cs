using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Domain.Interfaces
{
    public interface IEducationalOrganisationEntityRepository
    {
        Task InsertMany(IEnumerable<EducationalOrganisationEntity> educationalOrganisationEntitys);
        Task<IEnumerable<EducationalOrganisationEntity>> GetAll();
        void DeleteAll();
    }
}
