using SFA.DAS.EducationalOrganisations.Application.Queries.GetAllEducationalOrganisations;
using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Api.Responses
{
    public class GetAllEducationalOrganisationsResponse
    {
        public required IEnumerable<EducationalOrganisationEntity> EducationalOrganisations { get; set; }

        public static explicit operator GetAllEducationalOrganisationsResponse(GetAllEducationalOrganisationsResult source)
        {
            return new GetAllEducationalOrganisationsResponse
            {
                EducationalOrganisations = source.EducationalOrganisations
            };
        }
    }
}
