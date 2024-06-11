using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Application.Commands.ImportEducationalOrganisations;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.EducationalOrganisations.Application.UnitTests.Commands.ImportEducationalOrganisations
{
    public class WhenICallHandle
    {
        [Test, MoqAutoData]
        public async Task ImportOrganisationsAndPopulateData_WhenOrganisationsExist(
           [Frozen] Mock<IEducationalOrganisationEntityService> edOrgService,
           [Frozen] Mock<IEducationalOrganisationImportService> edOrgImportService,
           [Frozen] Mock<IEdubaseService> edubaseService,
           ICollection<EducationalOrganisationEntity> getAllOrganisationsResponse,
           IEnumerable<EducationalOrganisationImport> getAllOrganisationImportsResponse,
           ImportEducationalOrganisationsCommand command,
           ImportEducationalOrganisationsCommandHandler handler)
        {
            edubaseService
                .Setup(m => m.PopulateStagingEducationalOrganisations())
                .ReturnsAsync(true);
            
            edOrgImportService
             .Setup(m => m.GetAll())
             .ReturnsAsync(getAllOrganisationImportsResponse);

            edOrgService
               .Setup(m => m.PopulateDataFromStaging(getAllOrganisationImportsResponse, It.IsAny<DateTime>()))
               .Returns(Task.CompletedTask);

            // Act
            await handler.Handle(command, CancellationToken.None);

            edubaseService.Verify(x => x.PopulateStagingEducationalOrganisations(), Times.Once);
            edOrgImportService.Verify(x => x.GetAll(), Times.Once);
            edOrgService.Verify(x => x.PopulateDataFromStaging(getAllOrganisationImportsResponse, It.IsAny<DateTime>()), Times.Once);
        }

        [Test, MoqAutoData]
        public async Task Return_WhenNoOrganisationsExist(
           [Frozen] Mock<IEducationalOrganisationEntityService> edOrgService,
           [Frozen] Mock<IEducationalOrganisationImportService> edOrgImportService,
           [Frozen] Mock<IEdubaseService> edubaseService,
           ImportEducationalOrganisationsCommand command,
           ImportEducationalOrganisationsCommandHandler handler)
        {
            edubaseService
                .Setup(m => m.PopulateStagingEducationalOrganisations())
                .ReturnsAsync(false);

            // Act
            await handler.Handle(command, CancellationToken.None);

            edubaseService.Verify(x => x.PopulateStagingEducationalOrganisations(), Times.Once);
            edOrgImportService.Verify(x => x.GetAll(), Times.Never);
            edOrgService.Verify(x => x.PopulateDataFromStaging(It.IsAny<IEnumerable<EducationalOrganisationImport>>(), It.IsAny<DateTime>()), Times.Never);
        }
    }
}
