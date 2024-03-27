using AutoFixture;
using EdubaseSoap;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Application.Infrastructure;
using SFA.DAS.EducationalOrganisations.Domain.Configuration;

namespace SFA.DAS.EducationalOrganisations.Application.UnitTests.Infrastructure
{
    [TestFixture]
    public class EdubaseClientFactoryTests
    {
        private readonly Fixture _fixture = new Fixture();

        [Test]
        public void Create_ShouldReturnNonNullEdubaseClient()
        {
            // Arrange
            var configuration = _fixture.Create<EducationalOrganisationsConfiguration>();
            var factory = new EdubaseClientFactory(configuration);

            // Act
            var result = factory.Create();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<EdubaseClient>();
        }

        [Test]
        public void Create_ShouldSetClientCredentialsFromConfiguration()
        {
            // Arrange
            var configuration = _fixture.Create<EducationalOrganisationsConfiguration>();
            var factory = new EdubaseClientFactory(configuration);

            // Act
            EdubaseClient result = (EdubaseClient)factory.Create();

            // Assert
            result.ClientCredentials.UserName.UserName.Should().Be(configuration.EdubaseUsername);
            result.ClientCredentials.UserName.Password.Should().Be(configuration.EdubasePassword);
        }
    }
}
