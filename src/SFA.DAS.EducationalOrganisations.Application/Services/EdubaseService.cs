using EdubaseSoap;
using Microsoft.Extensions.Logging;
using SFA.DAS.EducationalOrganisations.Domain.Configuration;
using SFA.DAS.EducationalOrganisations.Domain.Entities;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Application.Services
{
    public class EdubaseService : IEdubaseService
    {
        private readonly ILogger<EdubaseService> _logger;
        private readonly EducationalOrganisationsConfiguration _configuration;

        public EdubaseService(ILogger<EdubaseService> logger, EducationalOrganisationsConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<ICollection<EducationalOrganisationEntity>> GetOrganisations()
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
                var establishments = await FindEstablishmentsAsync(filter);

                if (establishments == null || establishments.Count == 0)
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
                    URN = x.URN.ToString()
                }).ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not get organisations from Edubase API");
                throw;
            }
        }

        public async Task<List<Establishment>> FindEstablishmentsAsync(EstablishmentFilter filter)
        {
            await using var client = new EdubaseClient();
            client.ClientCredentials.UserName.UserName = "3741771"; // _configuration.EdubaseUsername;
            client.ClientCredentials.UserName.Password = "rtqmHYnB";// _configuration.EdubasePassword;


            List<Establishment> list = new List<Establishment>();
            filter = filter ?? new EstablishmentFilter();
            filter.Page = 0;
            FindEstablishmentsResponse response = await client.FindEstablishmentsAsync(new FindEstablishmentsRequest(filter));
            list.AddRange(response.Establishments);
            for (int i = 1; i < response.PageCount; i++)
            {
                filter.Page = i;
                List<Establishment> list2 = list;
                IEnumerable<Establishment> establishments = (await client.FindEstablishmentsAsync(new FindEstablishmentsRequest(filter))).Establishments;
                list2.AddRange(establishments);
            }

            return list;
        }      
    }
}
