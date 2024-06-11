using EdubaseSoap;
using Polly;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.Services
{
    public class EdubaseSoapService : IEdubaseSoapService
    {
        private readonly IEdubaseClientFactory _factory;
        private readonly IEdubaseClient _edubaseClient;

        public EdubaseSoapService(IEdubaseClientFactory factory)
        {
                _factory = factory;
                _edubaseClient = _factory.Create();
        }

        public async Task<FindEstablishmentsResponse> FindEstablishmentsAsync(EstablishmentFilter filter)
        {
            var retryPolicy = Policy
                .Handle<System.Net.Sockets.SocketException>()
                .Or<TimeoutException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            return await retryPolicy.ExecuteAsync(async () =>
            {
                return await _edubaseClient.FindEstablishmentsAsync(new FindEstablishmentsRequest(filter));
            });
        }
    }
}
