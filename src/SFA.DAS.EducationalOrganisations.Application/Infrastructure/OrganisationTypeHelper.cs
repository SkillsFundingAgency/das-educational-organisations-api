using SFA.DAS.EducationalOrganisations.Domain.DTO;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.Infrastructure
{
    public class OrganisationTypeHelper : IOrganisationTypeHelper
    {
        public OrganisationType[] GetOrganisationTypesArray()
        {
            return (OrganisationType[])Enum.GetValues(typeof(OrganisationType));
        }
    }
}
