using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetAllEducationalOrganisations;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;
using SFA.DAS.EducationalOrganisations.Application.Commands.ImportEducationalOrganisations;
using Azure.Core;

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
                .Setup(m => m.GetOrganisations())
                .ReturnsAsync(getAllOrganisationsResponse);

            edOrgImportService
             .Setup(m => m.ImportDataIntoStaging(getAllOrganisationsResponse))
             .ReturnsAsync(true);

            edOrgImportService
             .Setup(m => m.GetAll())
             .ReturnsAsync(getAllOrganisationImportsResponse);

            edOrgService
               .Setup(m => m.PopulateDataFromStaging(getAllOrganisationImportsResponse, It.IsAny<DateTime>()))
               .Returns(Task.CompletedTask);

            // Act
            await handler.Handle(command, CancellationToken.None);

            edubaseService.Verify(x => x.GetOrganisations(), Times.Once);
            edOrgImportService.Verify(x => x.ImportDataIntoStaging(getAllOrganisationsResponse), Times.Once);
            edOrgImportService.Verify(x => x.GetAll(), Times.Once);
            edOrgService.Verify(x => x.PopulateDataFromStaging(getAllOrganisationImportsResponse, It.IsAny<DateTime>()), Times.Once);
        }

        [Test, MoqAutoData]
        public async Task Return_WhenNoOrganisationsExist(
           [Frozen] Mock<IEducationalOrganisationEntityService> edOrgService,
           [Frozen] Mock<IEducationalOrganisationImportService> edOrgImportService,
           [Frozen] Mock<IEdubaseService> edubaseService,
           ICollection<EducationalOrganisationEntity> getAllOrganisationsResponse,
           ImportEducationalOrganisationsCommand command,
           ImportEducationalOrganisationsCommandHandler handler)
        {
            getAllOrganisationsResponse.Clear();
            edubaseService
                .Setup(m => m.GetOrganisations())
                .ReturnsAsync(getAllOrganisationsResponse);
            
            // Act
            await handler.Handle(command, CancellationToken.None);

            edubaseService.Verify(x => x.GetOrganisations(), Times.Once);
            edOrgImportService.Verify(x => x.ImportDataIntoStaging(It.IsAny<IEnumerable<EducationalOrganisationEntity>>()), Times.Never);
            edOrgImportService.Verify(x => x.GetAll(), Times.Never);
            edOrgService.Verify(x => x.PopulateDataFromStaging(It.IsAny<IEnumerable<EducationalOrganisationImport>>(), It.IsAny<DateTime>()), Times.Never);
        }
    }
}
