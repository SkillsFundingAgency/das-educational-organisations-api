using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Domain.Interfaces
{
    public interface IEdubaseService
    {
        Task<ICollection<EducationalOrganisationEntity>> GetOrganisations();
    }
}
