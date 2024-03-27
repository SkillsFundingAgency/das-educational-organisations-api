using SFA.DAS.EducationalOrganisations.Domain.DTO;

namespace SFA.DAS.EducationalOrganisations.Domain.Exceptions
{
    public class OrganisationNotFoundException : ReferenceDataException
    {
        public OrganisationNotFoundException(string message) : base(message)
        {
            // just call base
        }

        public OrganisationNotFoundException(OrganisationType organisationType, string identifier) :
            base($"Did not find an organisation type {organisationType} with identifier {identifier}")
        {
            OrganisationType = organisationType;
            Identifier = identifier;
        }
        public OrganisationType OrganisationType { get; }
        public string Identifier { get; }
    }
}
