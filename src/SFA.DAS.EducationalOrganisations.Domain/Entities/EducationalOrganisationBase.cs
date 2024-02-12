﻿namespace SFA.DAS.EducationalOrganisations.Domain.Entities
{
    public class EducationalOrganisationBase
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EducationalType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }
        public long? URN { get; set; }
    }
}
