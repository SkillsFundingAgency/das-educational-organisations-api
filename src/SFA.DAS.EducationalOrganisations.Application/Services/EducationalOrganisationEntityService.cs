using Microsoft.Extensions.Logging;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.Services
{
    public class EducationalOrganisationEntityService : IEducationalOrganisationEntityService
    {
        private readonly ILogger<EducationalOrganisationEntityService> _logger;

        private readonly IEducationalOrganisationEntityRepository _educationalOrganisationEntityRepository;
        private readonly IImportAuditRepository _importAuditRepository;

        public EducationalOrganisationEntityService(ILogger<EducationalOrganisationEntityService> logger,
            IEducationalOrganisationEntityRepository educationalOrganisationEntityRepository,
            IImportAuditRepository importAuditRepository)
        {
            _logger = logger;
            _educationalOrganisationEntityRepository = educationalOrganisationEntityRepository;
            _importAuditRepository = importAuditRepository;
        }

        public async Task PopulateDataFromStaging(IEnumerable<EducationalOrganisationImport> importOrgs, DateTime importStartTime)
        {
            if (importOrgs.Any())
            {
                try
                {
                    _logger.LogInformation("Educational Organisation data load from staging - started");

                    await _educationalOrganisationEntityRepository.DeleteAll();

                    await _educationalOrganisationEntityRepository.InsertMany(importOrgs
                            .Select(c => (EducationalOrganisationEntity)c).ToList());

                    await _importAuditRepository.Insert(new ImportAudit(importStartTime,
                    importOrgs.Count()));

                    _logger.LogInformation("Educational Organisation data load from staging - finished");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Educational Organisation  - an error occurred while trying to load data from staging tables");
                    throw;
                }
            }
        }
    }
}