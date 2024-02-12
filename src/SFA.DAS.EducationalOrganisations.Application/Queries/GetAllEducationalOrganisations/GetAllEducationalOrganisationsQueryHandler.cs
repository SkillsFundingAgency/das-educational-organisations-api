using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.Queries.GetAllEducationalOrganisations
{
    public class GetAllEducationalOrganisationsQueryHandler : IRequestHandler<GetAllEducationalOrganisationsQuery, GetAllEducationalOrganisationsResult>
    {
        private readonly ILogger<GetAllEducationalOrganisationsQueryHandler> _logger;
        private readonly IEducationalOrganisationEntityRepository _educationalOrganisationEntityRepository;

        public GetAllEducationalOrganisationsQueryHandler(ILogger<GetAllEducationalOrganisationsQueryHandler> logger
            , IEducationalOrganisationEntityRepository educationalOrganisationEntityRepository)
        {
            _logger = logger;
            _educationalOrganisationEntityRepository = educationalOrganisationEntityRepository;
        }

        public async Task<GetAllEducationalOrganisationsResult> Handle(GetAllEducationalOrganisationsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllEducationalOrganisationsQuery");

            return new GetAllEducationalOrganisationsResult
            {
                EducationalOrganisations = await _educationalOrganisationEntityRepository.GetAll()
            };
        }
    }
}
