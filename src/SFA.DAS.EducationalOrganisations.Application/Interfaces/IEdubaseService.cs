using SFA.DAS.EducationalOrganisations.Domain.EducationalOrganisation;

namespace SFA.DAS.EducationalOrganisations.Application.Interfaces
{
    public interface IEdubaseService
    {
        Task<ICollection<EducationalOrganisationEntity>> GetOrganisations();
    }
}
