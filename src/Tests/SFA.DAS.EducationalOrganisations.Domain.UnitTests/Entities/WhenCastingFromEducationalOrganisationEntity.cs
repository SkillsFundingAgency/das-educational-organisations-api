using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EducationalOrganisations.Domain.UnitTests.Entities;
public class WhenCastingFromEducationalOrganisationEntity
{
    [Test, MoqAutoData]
    public void Then_The_Fields_Are_Correctly_Mapped(EducationalOrganisationEntity frameworkImport)
    {
        EducationalOrganisationImport actual = frameworkImport;

        actual.Should().BeEquivalentTo(frameworkImport);
    }
}