using MediatR;

namespace SFA.DAS.EducationalOrganisations.Application.Queries.SearchEducationalOrganisations
{
    public class SearchEducationalOrganisationsQuery : IRequest<SearchEducationalOrganisationsResult>
    {
        public string SearchTerm { get; set; } = "";
        public int MaximumResults { get; set; }
    }
}