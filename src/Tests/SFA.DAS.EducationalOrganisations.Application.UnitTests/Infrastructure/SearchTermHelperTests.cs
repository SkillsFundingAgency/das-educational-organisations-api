using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Application.Infrastructure;

namespace SFA.DAS.EducationalOrganisations.Application.UnitTests.Infrastructure
{
    [TestFixture]

    public class SearchTermHelperTests
    {
        private const string ValidReference = "400000";
        [Test]
        public void IsSearchTermAReference_ValidSearchTerm_ShouldReturnTrue()
        {
            // Arrange
            var searchTerm = ValidReference;
            var timeout = TimeSpan.FromSeconds(5);

            // Act
            var result = SearchTermHelper.IsSearchTermAReference(searchTerm, timeout);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void IsSearchTermAReference_InvalidSearchTerm_ShouldReturnFalse()
        {
            // Arrange
            var searchTerm = "invalid";
            var timeout = TimeSpan.FromSeconds(5);

            // Act
            var result = SearchTermHelper.IsSearchTermAReference(searchTerm, timeout);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsSearchTermAReference_RegexTimeout_ShouldReturnFalse()
        {
            // Arrange
            var searchTerm = ValidReference;
            var timeout = TimeSpan.FromTicks(1);

            // Act
            var result = SearchTermHelper.IsSearchTermAReference(searchTerm, timeout);

            // Assert
            result.Should().BeFalse();
        }
    }
}
