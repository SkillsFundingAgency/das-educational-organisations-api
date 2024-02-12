using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.Queries.GetEducationalOrganisationById
{
    public class GetEducationalOrganisationByIdQueryHandler : IRequestHandler<GetEducationalOrganisationByIdQuery, GetEducationalOrganisationByIdResult>
    {
        private readonly ILogger<GetEducationalOrganisationByIdQueryHandler> _logger;
        private readonly IEducationalOrganisationEntityRepository _educationalOrganisationEntityRepository;

        public GetEducationalOrganisationByIdQueryHandler(ILogger<GetEducationalOrganisationByIdQueryHandler> logger
            , IEducationalOrganisationEntityRepository educationalOrganisationEntityRepository)
        {
            _logger = logger;
            _educationalOrganisationEntityRepository = educationalOrganisationEntityRepository;
        }

        public async Task<GetEducationalOrganisationByIdResult> Handle(GetEducationalOrganisationByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetEducationalOrganisationByIdQuery");

            var result = await _educationalOrganisationEntityRepository.GetById(request.Id);

            return new GetEducationalOrganisationByIdResult
            {
                EducationalOrganisation = result
            };
        }

    }
}
