using EdubaseSoap;
using SFA.DAS.EducationalOrganisations.Domain.Configuration;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.Infrastructure
{
    public class EdubaseClientFactory : IEdubaseClientFactory
    {
        private readonly EducationalOrganisationsConfiguration _configuration;

        public EdubaseClientFactory(EducationalOrganisationsConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEdubaseClient Create()
        {
            var client = new EdubaseClient();
            client.ClientCredentials.UserName.UserName = _configuration.EdubaseUsername;
            client.ClientCredentials.UserName.Password = _configuration.EdubasePassword;

            return client;
        }
    }
}
