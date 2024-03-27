using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetLatestDetails;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Exceptions;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EducationalOrganisations.Application.UnitTests.Queries.GetLatestDetails
{
    [TestFixture]
    public class WhenICallHandle
    {
        [Test, MoqAutoData]
        public async Task And_IsValidReference_Return_EducationalOrganisation(
         [Frozen] Mock<IEducationalOrganisationEntityRepository> edOrgRepo,
         EducationalOrganisationEntity repoResponse,
         GetLatestDetailsQueryHandler handler)
        {
            var query = new GetLatestDetailsQuery
            {
                Identifier = "400000"
            };

            edOrgRepo
                .Setup(m => m.FindByUrn(query.Identifier))
                .ReturnsAsync(repoResponse);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            result.EducationalOrganisation.Should().BeEquivalentTo(repoResponse);
        }

        [Test, MoqAutoData]
        public async Task And_IsNot_ValidReference_Return_BadOrganisationIdentifierException(
         GetLatestDetailsQueryHandler handler)
        {
            var query = new GetLatestDetailsQuery
            {
                Identifier = "400000000"
            };

            // Act
            Func<Task> act = async () => await handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<BadOrganisationIdentifierException>()
                       .WithMessage($"The supplied identifier is not in a format recognised by the reference handler for organisation type (EducationOrganisation). Invalid identifier was \"{query.Identifier}\"");
        }

        [Test, MoqAutoData]
        public async Task And_NoOrganisationFound_Returns_OrganisationNotFoundException(
         [Frozen] Mock<IEducationalOrganisationEntityRepository> edOrgRepo,
         GetLatestDetailsQueryHandler handler)
        {
            var query = new GetLatestDetailsQuery
            {
                Identifier = "400000"
            };

            edOrgRepo
                .Setup(m => m.FindByUrn(query.Identifier))
                .ReturnsAsync((EducationalOrganisationEntity)null);

            // Act
            Func<Task> act = async () => await handler.Handle(query, CancellationToken.None);

            await act.Should().ThrowAsync<OrganisationNotFoundException>()
                       .WithMessage($"Did not find an organisation type EducationOrganisation with identifier {query.Identifier}");
        }
    }
}
