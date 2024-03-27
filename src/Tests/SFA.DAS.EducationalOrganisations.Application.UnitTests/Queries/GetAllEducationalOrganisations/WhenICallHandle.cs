using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetAllEducationalOrganisations;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EducationalOrganisations.Application.UnitTests.Queries.GetAllEducationalOrganisations
{
    [TestFixture]
    public class WhenICallHandle
    {
        [Test, MoqAutoData]
        public async Task Then_Returns_AllEntities(
           [Frozen] Mock<IEducationalOrganisationEntityRepository> edOrgRepo,
           IEnumerable<EducationalOrganisationEntity> repoResponse,
           GetAllEducationalOrganisationsQuery request,
           GetAllEducationalOrganisationsQueryHandler handler)
        {
            edOrgRepo
                .Setup(m => m.GetAll())
                .ReturnsAsync(repoResponse);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            result.EducationalOrganisations.Should().BeEquivalentTo(repoResponse);
        }
    }
}
