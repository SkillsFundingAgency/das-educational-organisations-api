using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EducationalOrganisations.Application.Infrastructure;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.Queries.SearchEducationalOrganisations
{
    public partial class SearchEducationalOrganisationsQueryHandler : IRequestHandler<SearchEducationalOrganisationsQuery, SearchEducationalOrganisationsResult>
    {
        private readonly ILogger<SearchEducationalOrganisationsQueryHandler> _logger;
        private readonly IEducationalOrganisationEntityRepository _educationalOrganisationEntityRepository;

        public SearchEducationalOrganisationsQueryHandler(ILogger<SearchEducationalOrganisationsQueryHandler> logger
            , IEducationalOrganisationEntityRepository educationalOrganisationEntityRepository)
        {
            _logger = logger;
            _educationalOrganisationEntityRepository = educationalOrganisationEntityRepository;
        }

        public async Task<SearchEducationalOrganisationsResult> Handle(SearchEducationalOrganisationsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling SearchEducationalOrganisationsQuery");

            if (!SearchTermHelper.IsSearchTermAReference(request.SearchTerm, TimeSpan.FromMilliseconds(1000)))
            {
                return new SearchEducationalOrganisationsResult
                {
                    EducationalOrganisations = await _educationalOrganisationEntityRepository.SearchByName(request.SearchTerm, request.MaximumResults)
                };
            }

            return new SearchEducationalOrganisationsResult
            {
                EducationalOrganisations = await _educationalOrganisationEntityRepository.SearchByUrn(request.SearchTerm)
            };
        }
    }
}