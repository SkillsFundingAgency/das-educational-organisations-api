using MediatR;

namespace SFA.DAS.EducationalOrganisations.Application.Commands.GetEducationalOrganisationById
{
    public class GetEducationalOrganisationByIdQuery :IRequest<GetEducationalOrganisationByIdResult>
    {
        public Guid Id { get; set; }
    }
}
