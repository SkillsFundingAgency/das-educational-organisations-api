namespace SFA.DAS.EducationalOrganisations.Domain.Entities
{
    public class EducationalOrganisationImport : EducationalOrganisationBase
    {
        public static implicit operator EducationalOrganisationImport(EducationalOrganisationEntity source)
        {
            return new EducationalOrganisationImport
            {
              Id = source.Id,
              Name = source.Name,
              EducationalType = source.EducationalType,
              AddressLine1 = source.AddressLine1,
              AddressLine2 = source.AddressLine2,
              AddressLine3 = source.AddressLine3,
              Town = source.Town,
              County = source.County,
              PostCode = source.PostCode,
              URN = source.URN
            };
        }
    }
}
