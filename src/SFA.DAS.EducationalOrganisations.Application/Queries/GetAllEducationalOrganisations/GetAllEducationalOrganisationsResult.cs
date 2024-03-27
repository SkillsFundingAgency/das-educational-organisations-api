using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Application.Queries.GetAllEducationalOrganisations
{
    public class GetAllEducationalOrganisationsResult
    {
        public required IEnumerable<EducationalOrganisationEntity> EducationalOrganisations { get; set; }
    }
}
