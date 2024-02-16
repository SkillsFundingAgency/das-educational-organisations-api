using MediatR;

namespace SFA.DAS.EducationalOrganisations.Application.Queries.GetLatestDetails
{
    public class GetLatestDetailsQuery : IRequest<GetLatestDetailsResult>
    {
        public string Identifier { get; set; }
    }
}
