using Dfe.Edubase2.SoapApi.Client;
using SFA.DAS.EducationOrganisations.Domain.Configuration;

namespace SFA.DAS.EducationOrganisations.Application.Factories
{
    public class EdubaseClientFactory : IEdubaseClientFactory
    {
        private readonly EducationOrganisationsConfiguration _configuration;

        public EdubaseClientFactory(EducationOrganisationsConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEstablishmentClient Create()
        {
            return new EstablishmentClient(_configuration.EdubaseUsername, _configuration.EdubasePassword);
        }
    }
}
