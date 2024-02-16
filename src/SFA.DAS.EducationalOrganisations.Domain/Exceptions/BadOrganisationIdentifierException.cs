using SFA.DAS.EducationalOrganisations.Domain.DTO;

namespace SFA.DAS.EducationalOrganisations.Domain.Exceptions
{
    public class BadOrganisationIdentifierException : ReferenceDataException
    {
        public BadOrganisationIdentifierException()
        {
            // just call base            
        }

        public BadOrganisationIdentifierException(string message) : base(message)
        {
            // just call base
        }

        public BadOrganisationIdentifierException(OrganisationType organisationType, string identifier) :
            base($"The supplied identifier is not in a format recognised by the reference handler for organisation type ({organisationType}). Invalid identifier was \"{identifier}\"")
        {
            OrganisationType = organisationType;
            Identifier = identifier;
        }
        public OrganisationType OrganisationType { get; }
        public string Identifier { get; }
    }
}
