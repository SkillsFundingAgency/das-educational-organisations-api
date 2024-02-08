using SFA.DAS.EducationOrganisations.Domain.EducationalOrganisation;

namespace SFA.DAS.EducationOrganisations.Application.Interfaces
{
    public interface IEdubaseService
    {
        Task<ICollection<EducationalOrganisationEntity>> GetOrganisations();
    }
}
