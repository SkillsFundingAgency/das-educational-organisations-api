using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Application.Queries.SearchEducationalOrganisations
{
    public class SearchEducationalOrganisationsResult
    {
        public IEnumerable<EducationalOrganisationEntity> EducationalOrganisations { get; set; }
    }
}
