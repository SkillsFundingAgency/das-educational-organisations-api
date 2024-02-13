using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EducationalOrganisations.Domain.UnitTests.Entities;
public class WhenCastingFromEducationalOrganisationImport
{
    [Test, MoqAutoData]
    public void Then_The_Fields_Are_Correctly_Mapped(EducationalOrganisationImport frameworkImport)
    {
        var actual = (EducationalOrganisationEntity)frameworkImport;

        actual.Should().BeEquivalentTo(frameworkImport);
    }
}