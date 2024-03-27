using EdubaseSoap;
using Microsoft.Extensions.Logging;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.Services
{
    public class EdubaseService : IEdubaseService
    {
        private readonly ILogger<EdubaseService> _logger;
        private readonly IEdubaseSoapService _edubaseSoapService;
        private readonly IEducationalOrganisationImportService _educationalOrganisationImportService;

        public EdubaseService(
                ILogger<EdubaseService> logger, 
                IEdubaseSoapService edubaseSoapService,
                IEducationalOrganisationImportService educationalOrganisationImportService)
        {
            _logger = logger;
            _educationalOrganisationImportService = educationalOrganisationImportService;
            _edubaseSoapService = edubaseSoapService;
        }

        public async Task<bool> PopulateStagingEducationalOrganisations()
        {
            var filter = new EstablishmentFilter
            {
                Fields = new StringList
                {
                    "EstablishmentName",
                    "TypeOfEstablishment",
                    "Street",
                    "Locality",
                    "Address3",
                    "Town",
                    "County",
                    "Postcode",
                    "URN"
                }
            };

            try
            {
                _logger.LogInformation("EducationalOrganisation Import - data into staging - started");

                await _educationalOrganisationImportService.ClearStagingData();

                var result = await FindEstablishmentsAndInsertIntoStaging(filter);

                _logger.LogInformation("EducationalOrganisation Import - data into staging - finished");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not get organisations from Edubase API");
                throw;
            }
        }

        private async Task<bool> FindEstablishmentsAndInsertIntoStaging(EstablishmentFilter filter)
        {
            int batchSize = 100;
            List<Establishment> allRecords = [];
            filter ??= new EstablishmentFilter();
            filter.Page = 0;

            FindEstablishmentsResponse response = new();

            do
            {
                response = await _edubaseSoapService.FindEstablishmentsAsync(filter);

                if (response?.Establishments == null)
                {
                    return false;
                }

                allRecords.AddRange(response.Establishments);

                if (allRecords.Count >= batchSize)
                {
                    await InsertIntoDatabaseAsync(allRecords);
                    allRecords.Clear();
                }

                filter.Page++;

            } while (filter.Page < response.PageCount);           

            if (allRecords.Count > 0)
            {
                await InsertIntoDatabaseAsync(allRecords);
            }

            return true;
        }
     
        private async Task InsertIntoDatabaseAsync(List<Establishment> establishments)
        {
            var organisations = establishments.Select(x => new EducationalOrganisationEntity
            {
                Name = x.EstablishmentName,
                EducationalType = x.TypeOfEstablishment?.DisplayName ?? string.Empty,
                AddressLine1 = x.Street,
                AddressLine2 = x.Locality,
                AddressLine3 = x.Address3,
                Town = x.Town,
                County = x.County?.DisplayName ?? string.Empty,
                PostCode = x.Postcode,
                URN = x.URN.ToString()
            }).ToArray();

            await _educationalOrganisationImportService.InsertDataIntoStaging(organisations.Select(c => (EducationalOrganisationImport)c).ToList());
        }
    }
}