using MediatR;
using SFA.DAS.EducationalOrganisations.Application.Infrastructure;
using SFA.DAS.EducationalOrganisations.Domain.DTO;
using SFA.DAS.EducationalOrganisations.Domain.Exceptions;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.Queries.GetLatestDetails
{

    public class GetLatestDetailsQueryHandler : IRequestHandler<GetLatestDetailsQuery, GetLatestDetailsResult>
    {
        private readonly IEducationalOrganisationEntityRepository _educationalOrganisationEntityRepository;

        public GetLatestDetailsQueryHandler(IEducationalOrganisationEntityRepository educationalOrganisationEntityRepository)
        {
            _educationalOrganisationEntityRepository = educationalOrganisationEntityRepository;
        }

        public async Task<GetLatestDetailsResult> Handle(GetLatestDetailsQuery query, CancellationToken cancellationToken)
        {
            if (!SearchTermHelper.IsSearchTermAReference(query.Identifier, TimeSpan.FromSeconds(1)))
            {
                throw new BadOrganisationIdentifierException(OrganisationType.EducationOrganisation, query.Identifier);
            }

            var educationOrganisation = await _educationalOrganisationEntityRepository.FindByUrn(query.Identifier);

            if (educationOrganisation == null)
            {
                throw new OrganisationNotFoundException(OrganisationType.EducationOrganisation, query.Identifier);
            }

            return new GetLatestDetailsResult
            {
                EducationalOrganisation = educationOrganisation
            };
        }
    }
}
