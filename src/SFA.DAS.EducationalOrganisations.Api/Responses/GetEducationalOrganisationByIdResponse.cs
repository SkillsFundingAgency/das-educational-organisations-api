using SFA.DAS.EducationalOrganisations.Application.Queries.GetEducationalOrganisationById;
using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Api.Responses
{
    public class GetEducationalOrganisationByIdResponse
    {
        public required EducationalOrganisationEntity EducationalOrganisation { get; set; }

        public static explicit operator GetEducationalOrganisationByIdResponse(GetEducationalOrganisationByIdResult source)
        {
            return new GetEducationalOrganisationByIdResponse
            {
                EducationalOrganisation = source.EducationalOrganisation ?? new EducationalOrganisationEntity()
            };
        }
    }
}
