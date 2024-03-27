using SFA.DAS.EducationalOrganisations.Domain.DTO;

namespace SFA.DAS.EducationalOrganisations.Application.Queries.GetIdentifiableOrganisationTypes;
public class GetIdentifiableOrganisationTypesResult
{
    public required OrganisationType[] OrganisationTypes { get; set; }
}