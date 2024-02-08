using SFA.DAS.EducationOrganisations.Application.Interfaces;
using SFA.DAS.EducationOrganisations.Application.Factories;
using Dfe.Edubase2.SoapApi.Client.EdubaseService;
using SFA.DAS.EducationOrganisations.Domain.EducationalOrganisation;
using Microsoft.Extensions.Logging;

namespace SFA.DAS.EducationOrganisations.Application.Services
{
    public class EdubaseService : IEdubaseService
    {
        private readonly IEdubaseClientFactory _factory;
        private readonly ILogger<EdubaseService> _logger;

        public EdubaseService(IEdubaseClientFactory factory, ILogger<EdubaseService> logger)
        {
            _factory = factory;
            _logger = logger;
        }
        public async Task<ICollection<EducationalOrganisationEntity>> GetOrganisations()
        {
            var client = _factory.Create();

            var filter = new EstablishmentFilter
            {
                Fields = ["EstablishmentName", "TypeOfEstablishment", "Street", "Locality", "Address3", "Town", "County", "Postcode", "URN"]
            };

            try
            {
                var establishments = await client.FindEstablishmentsAsync(filter);

                if (establishments.Count == 0)
                    return Array.Empty<EducationalOrganisationEntity>();

                return establishments.Select(x => new EducationalOrganisationEntity
                {
                    Name = x.EstablishmentName,
                    EducationalType = x.TypeOfEstablishment?.DisplayName ?? string.Empty,
                    AddressLine1 = x.Street,
                    AddressLine2 = x.Locality,
                    AddressLine3 = x.Address3,
                    Town = x.Town,
                    County = x.County?.DisplayName ?? string.Empty,
                    PostCode = x.Postcode,
                    URN = x.URN
                }).ToArray();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not get organisations from Edubase API");
            }

            return Array.Empty<EducationalOrganisationEntity>();
        }
    }
}
