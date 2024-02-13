﻿using EdubaseSoap;
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

            await using var client = new EdubaseClient();

            try
            {
                client.ClientCredentials.UserName.UserName = _configuration.EdubaseUsername;
                client.ClientCredentials.UserName.Password = _configuration.EdubasePassword;

                var establishments = await client.FindEstablishmentsAsync(new FindEstablishmentsRequest(filter));

                if (establishments == null || establishments.Establishments.Count == 0)
                    return Array.Empty<EducationalOrganisationEntity>();

                return establishments.Establishments.Select(x => new EducationalOrganisationEntity
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
    }
}