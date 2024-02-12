using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Application.Queries.SearchEducationalOrganisations
{
    public class SearchEducationalOrganisationsResult
    {
        public IEnumerable<EducationalOrganisationEntity> EducationalOrganisations { get; set; }
    }
}
