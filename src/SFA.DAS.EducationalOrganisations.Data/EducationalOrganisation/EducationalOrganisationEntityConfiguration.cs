using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.EducationOrganisations.Domain.EducationalOrganisation;

namespace SFA.DAS.EducationalOrganisations.Data.EducationalOrganisation;

public class EducationalOrganisationEntityConfiguration : IEntityTypeConfiguration<EducationalOrganisationEntity>
{
    public void Configure(EntityTypeBuilder<EducationalOrganisationEntity> builder)
    {
        builder.ToTable("EducationalOrganisation");
        // builder.HasKey(x => x.Id);
    }
}   