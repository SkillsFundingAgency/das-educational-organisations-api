namespace SFA.DAS.EducationalOrganisations.Domain.Exceptions
{
    public class ReferenceDataException : Exception
    {
        public ReferenceDataException()
        {
            // just call base    
        }

        public ReferenceDataException(string message) : base(message)
        {
            // just call base        
        }
    }
}
