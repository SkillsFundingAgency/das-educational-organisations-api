using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Api.Responses;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetAllEducationalOrganisations;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EducationalOrganisations.Api.UnitTests.Responses
{
    public class WhenCastingFromGetAllEducationalOrganisationsResult
    {
        [Test, MoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(GetAllEducationalOrganisationsResult source)
        {
            var actual = new GetAllEducationalOrganisationsResponse { EducationalOrganisations = source.EducationalOrganisations };

            actual.Should().BeEquivalentTo(source);
        }
    }
}
