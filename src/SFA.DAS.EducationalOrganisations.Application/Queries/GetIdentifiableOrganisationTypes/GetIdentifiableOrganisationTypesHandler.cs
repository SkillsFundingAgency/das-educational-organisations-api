using MediatR;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.Queries.GetIdentifiableOrganisationTypes
{
    public class GetIdentifiableOrganisationTypesHandler : IRequestHandler<GetIdentifiableOrganisationTypesQuery, GetIdentifiableOrganisationTypesResult>
    {
        private IOrganisationTypeHelper _organisationTypeHelper;

        public GetIdentifiableOrganisationTypesHandler(IOrganisationTypeHelper organisationTypeHelper)
        {
            _organisationTypeHelper = organisationTypeHelper;
        }

        public Task<GetIdentifiableOrganisationTypesResult> Handle(GetIdentifiableOrganisationTypesQuery query, CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetIdentifiableOrganisationTypesResult
            {
                OrganisationTypes = _organisationTypeHelper.GetOrganisationTypesArray()
            });
        }
    }
}
