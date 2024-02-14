using System.Reflection;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Application.Queries.SearchEducationalOrganisations;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;


namespace SFA.DAS.EducationalOrganisations.Application.UnitTests.Queries.SearchEducationalOrganisations
{
    [TestFixture]
    public class WhenICallHandle
    {
        [Test, MoqAutoData]
        public async Task And_IsNot_SearchTermAReference_Then_SearchByName(
           [Frozen] Mock<IEducationalOrganisationEntityRepository> edOrgRepo,
           IEnumerable<EducationalOrganisationEntity> repoResponse,
           SearchEducationalOrganisationsQueryHandler handler)
        {
            var query = new SearchEducationalOrganisationsQuery
            {
                SearchTerm = "10000",
                MaximumResults = 500
            };

            edOrgRepo
                .Setup(m => m.SearchByName(query.SearchTerm, query.MaximumResults))
                .ReturnsAsync(repoResponse);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            result.EducationalOrganisations.Should().BeEquivalentTo(repoResponse);
        }

        [Test, MoqAutoData]
        public async Task And_IsSearchTermAReference_Then_SearchByUrn(
           [Frozen] Mock<IEducationalOrganisationEntityRepository> edOrgRepo,
           IEnumerable<EducationalOrganisationEntity> repoResponse,
           SearchEducationalOrganisationsQueryHandler handler)
        {
            var query = new SearchEducationalOrganisationsQuery
            {
                SearchTerm = "400000",
                MaximumResults = 500
            };

            edOrgRepo
                .Setup(m => m.SearchByUrn(query.SearchTerm))
                .ReturnsAsync(repoResponse);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            result.EducationalOrganisations.Should().BeEquivalentTo(repoResponse);
        }

        [Test, MoqAutoData]
        public void IsSearchTermAReference_ShouldReturnFalse_WhenRegexTimeoutOccurs(
           SearchEducationalOrganisationsQueryHandler handler)
        {
            // Arrange
            var timeoutSearchTerm = "400000";
            var isSearchTermAReferenceMethod = GetIsSearchTermAReferenceMethod();
            var timeout = TimeSpan.FromMilliseconds(0.01);

            // Act
            var result = (bool)isSearchTermAReferenceMethod.Invoke(handler, new object[] { timeoutSearchTerm, timeout });

            // Assert
            result.Should().BeFalse();
        }

        private static MethodInfo GetIsSearchTermAReferenceMethod()
        {
            return typeof(SearchEducationalOrganisationsQueryHandler)
                .GetMethod("IsSearchTermAReference", BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}
