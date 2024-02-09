using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.EducationalOrganisations.Domain.EducationalOrganisation;

namespace SFA.DAS.EducationalOrganisations.Data.EducationalOrganisations;

public class EducationalOrganisationEntityConfiguration : IEntityTypeConfiguration<EducationalOrganisationEntity>
{
    public void Configure(EntityTypeBuilder<EducationalOrganisationEntity> builder)
    {
        builder.ToTable("EducationalOrganisation");
        // builder.HasKey(x => x.Id);
    }
}