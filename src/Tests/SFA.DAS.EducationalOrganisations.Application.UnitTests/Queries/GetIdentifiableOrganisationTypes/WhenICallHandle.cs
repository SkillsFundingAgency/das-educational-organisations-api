using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetIdentifiableOrganisationTypes;
using SFA.DAS.EducationalOrganisations.Domain.DTO;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EducationalOrganisations.Application.UnitTests.Queries.GetIdentifiableOrganisationTypes
{
    [TestFixture]
    public class WhenICallHandle
    {
        [Test, MoqAutoData]
        public async Task Then_Returns_AllEntities(
           [Frozen] Mock<IOrganisationTypeHelper> orgTypeHelper,
           OrganisationType[] types,
           GetIdentifiableOrganisationTypesQuery request,
           GetIdentifiableOrganisationTypesHandler handler)
        {
            orgTypeHelper
                .Setup(m => m.GetOrganisationTypesArray())
                .Returns(types);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            result.OrganisationTypes.Should().BeEquivalentTo(types);
        }
    }
}
