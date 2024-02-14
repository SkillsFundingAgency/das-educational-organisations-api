using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Api.Responses;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetEducationalOrganisationById;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EducationalOrganisations.Api.UnitTests.Responses
{
    public class WhenCastingFromGetEducationalOrganisationByIdResult
    {
        [Test, MoqAutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(GetEducationalOrganisationByIdResult source)
        {
            var actual = new GetEducationalOrganisationByIdResponse { EducationalOrganisation = source.EducationalOrganisation };

            actual.Should().BeEquivalentTo(source);
        }
    }
}
