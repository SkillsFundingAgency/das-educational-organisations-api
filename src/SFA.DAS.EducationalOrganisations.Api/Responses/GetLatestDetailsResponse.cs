using SFA.DAS.EducationalOrganisations.Application.Queries.GetLatestDetails;
using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Api.Responses
{
    public class GetLatestDetailsResponse
    {
        public EducationalOrganisationEntity EducationalOrganisation { get; set; }

        public static explicit operator GetLatestDetailsResponse(GetLatestDetailsResult source)
        {
            return new GetLatestDetailsResponse
            {
                EducationalOrganisation = source.EducationalOrganisation
            };
        }
    }
}
