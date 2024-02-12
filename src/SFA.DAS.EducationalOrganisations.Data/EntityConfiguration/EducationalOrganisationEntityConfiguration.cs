using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Data.EntityConfiguration;

public class EducationalOrganisationEntityConfiguration : IEntityTypeConfiguration<EducationalOrganisationEntity>
{
    public void Configure(EntityTypeBuilder<EducationalOrganisationEntity> builder)
    {
        builder.ToTable("EducationalOrganisation");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired().ValueGeneratedOnAdd();

        builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("varchar").IsRequired();
        builder.Property(x => x.EducationalType).HasColumnName("EducationalType").HasColumnType("EducationalType").IsRequired();
        builder.Property(x => x.AddressLine1).HasColumnName("AddressLine1").HasColumnType("AddressLine1").IsRequired(false);
        builder.Property(x => x.AddressLine2).HasColumnName("AddressLine2").HasColumnType("AddressLine2").IsRequired(false);
        builder.Property(x => x.AddressLine3).HasColumnName("AddressLine3").HasColumnType("AddressLine3").IsRequired(false);
        builder.Property(x => x.Town).HasColumnName("Town").HasColumnType("Town").IsRequired(false);
        builder.Property(x => x.County).HasColumnName("County").HasColumnType("County").IsRequired(false);
        builder.Property(x => x.PostCode).HasColumnName("PostCode").HasColumnType("PostCode").IsRequired(false);
        builder.Property(x => x.URN).HasColumnName("URN").HasColumnType("bigint").IsRequired(false);

        builder.HasIndex(x => x.Id).IsUnique();
    }

}