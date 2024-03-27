using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFA.DAS.EducationalOrganisations.Domain.Entities;

namespace SFA.DAS.EducationalOrganisations.Data.EntityConfiguration
{
    public class ImportAuditConfiguration : IEntityTypeConfiguration<ImportAudit>
    {
        public void Configure(EntityTypeBuilder<ImportAudit> builder)
        {
            builder.ToTable("ImportAudit");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.TimeStarted).HasColumnName("TimeStarted").HasColumnType("DateTime").IsRequired();
            builder.Property(x => x.TimeFinished).HasColumnName("TimeFinished").HasColumnType("DateTime").IsRequired();
            builder.Property(x => x.RowsImported).HasColumnName("RowsImported").HasColumnType("int").IsRequired();

            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}