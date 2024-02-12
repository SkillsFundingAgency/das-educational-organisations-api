using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Data.EntityConfiguration
{
    public class EducationalOrganisationImportConfiguration : IEntityTypeConfiguration<EducationalOrganisationImport>
    {
        public void Configure(EntityTypeBuilder<EducationalOrganisationImport> builder)
        {
            builder.ToTable("EducationalOrganisation_Import");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("uniqueidentifier").IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("nvarchar(350)").IsRequired();
            builder.Property(x => x.EducationalType).HasColumnName("EducationalType").HasColumnType("nvarchar(150)").IsRequired();
            builder.Property(x => x.AddressLine1).HasColumnName("AddressLine1").HasColumnType("nvarchar(150)").IsRequired(false);
            builder.Property(x => x.AddressLine2).HasColumnName("AddressLine2").HasColumnType("nvarchar(150)").IsRequired(false);
            builder.Property(x => x.AddressLine3).HasColumnName("AddressLine3").HasColumnType("nvarchar(150)").IsRequired(false);
            builder.Property(x => x.Town).HasColumnName("Town").HasColumnType("nvarchar(50)").IsRequired(false);
            builder.Property(x => x.County).HasColumnName("County").HasColumnType("nvarchar(50)").IsRequired(false);
            builder.Property(x => x.PostCode).HasColumnName("PostCode").HasColumnType("nvarchar(8)").IsRequired(false);
            builder.Property(x => x.URN).HasColumnName("URN").HasColumnType("nvarchar(100)").IsRequired(false);

            builder.HasIndex(x => x.Id).IsUnique();
            builder.HasIndex(x => x.Name).IsUnique().HasDatabaseName("IX_EducationalOrganisation_Import_Name");

        }
    }
}
