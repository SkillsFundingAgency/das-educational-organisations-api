using SFA.DAS.EducationalOrganisations.Application.Queries.SearchEducationalOrganisations;
using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Api.Responses
{
    public class SearchEducationalOrganisationsResponse
    {
        public required IEnumerable<EducationalOrganisationEntity> EducationalOrganisations { get; set; }

        public static explicit operator SearchEducationalOrganisationsResponse(SearchEducationalOrganisationsResult source)
        {
            return new SearchEducationalOrganisationsResponse
            {
                EducationalOrganisations = source.EducationalOrganisations
            };
        }
    }
}
