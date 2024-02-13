using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.Commands.ImportEducationalOrganisations
{
    public class ImportEducationalOrganisationsCommandHandler : IRequestHandler<ImportEducationalOrganisationsCommand, Unit>
    {
        private readonly ILogger<ImportEducationalOrganisationsCommandHandler> _logger;
        private readonly IEdubaseService _edubaseService;
        private readonly IEducationalOrganisationEntityService _educationalOrganisationEntityService;
        private readonly IEducationalOrganisationImportService _educationalOrganisationImportService;

        public ImportEducationalOrganisationsCommandHandler(ILogger<ImportEducationalOrganisationsCommandHandler> logger,
                IEdubaseService edubaseService,
                IEducationalOrganisationEntityService educationalOrganisationEntityService,
                IEducationalOrganisationImportService educationalOrganisationImportService)
        {
            _logger = logger;
            _edubaseService = edubaseService;
            _educationalOrganisationEntityService = educationalOrganisationEntityService;
            _educationalOrganisationImportService = educationalOrganisationImportService;
        }

        public async Task<Unit> Handle(ImportEducationalOrganisationsCommand request, CancellationToken cancellationToken)
        {
            var importStartTime = DateTime.Now;

            _logger.LogInformation("Attempting GetAllEducationalOrganisationsQuery");

            var organisations = await _edubaseService.GetOrganisations();

            _logger.LogInformation("Retrieved educational organisations with TotalCount: {Count}", organisations.Count);

            if (organisations == null || organisations.Count == 0) return Unit.Value;

            //load data into Staging
            await _educationalOrganisationImportService.ImportDataIntoStaging(organisations);

            var latestDataFromStaging = await _educationalOrganisationImportService.GetAll();

            //populate from Staging
            await _educationalOrganisationEntityService.PopulateDataFromStaging(latestDataFromStaging, importStartTime);

            return Unit.Value;
        }
    }
}