using MediatR;

namespace SFA.DAS.EducationalOrganisations.Application.Queries.GetEducationalOrganisationById
{
    public class GetEducationalOrganisationByIdQuery : IRequest<GetEducationalOrganisationByIdResult>
    {
        public Guid Id { get; set; }
    }
}
