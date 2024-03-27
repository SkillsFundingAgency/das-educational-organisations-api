using Microsoft.Extensions.Logging;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.Services
{
    public class EducationalOrganisationImportService : IEducationalOrganisationImportService
    {
        private readonly ILogger<EducationalOrganisationImportService> _logger;

        private readonly IEducationalOrganisationImportRepository _educationalOrganisationImportRepository;

        public EducationalOrganisationImportService(ILogger<EducationalOrganisationImportService> logger, IEducationalOrganisationImportRepository educationalOrganisationImportRepository)
        {
            _logger = logger;
            _educationalOrganisationImportRepository = educationalOrganisationImportRepository;
        }
        public async Task<IEnumerable<EducationalOrganisationImport>> GetAll()
        {
            return await _educationalOrganisationImportRepository.GetAll();
        }

        public async Task ClearStagingData()
        {
            await _educationalOrganisationImportRepository.DeleteAll();
        }

        public async Task<bool> InsertDataIntoStaging(List<EducationalOrganisationImport> organisations)
        {
            try
            {
                await _educationalOrganisationImportRepository.InsertMany(organisations);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EducationalOrganisation Import - an error occurred while trying to import data from LARS file");
                return false;
            }
        }
    }
}