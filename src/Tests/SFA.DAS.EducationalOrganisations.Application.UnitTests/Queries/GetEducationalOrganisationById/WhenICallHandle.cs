using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetEducationalOrganisationById;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;


namespace SFA.DAS.EducationalOrganisations.Application.UnitTests.Queries.GetEducationalOrganisationById
{
    [TestFixture]
    public class WhenICallHandle
    {
        [Test, MoqAutoData]
        public async Task Then_Returns_Entity(
           [Frozen] Mock<IEducationalOrganisationEntityRepository> edOrgRepo,
           EducationalOrganisationEntity repoResponse,
           GetEducationalOrganisationByIdQuery request,
           GetEducationalOrganisationByIdQueryHandler handler)
        {
            edOrgRepo
                .Setup(m => m.GetById(request.Id))
                .ReturnsAsync(repoResponse);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            result.EducationalOrganisation.Should().BeEquivalentTo(repoResponse);
        }
    }
}
