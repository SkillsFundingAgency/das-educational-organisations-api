using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Api.Responses;
using SFA.DAS.EducationalOrganisations.Application.Queries.SearchEducationalOrganisations;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EducationalOrganisations.Api.UnitTests.Responses
{
    public class WhenCastingFromSearchEducationalOrganisationsResult
    {
        [Test, MoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(SearchEducationalOrganisationsResult source)
        {
            var actual = new SearchEducationalOrganisationsResponse { EducationalOrganisations = source.EducationalOrganisations };

            actual.Should().BeEquivalentTo(source);
        }
    }
}
