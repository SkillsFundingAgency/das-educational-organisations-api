using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SFA.DAS.EducationalOrganisations.Data.EducationalOrganisation;
using SFA.DAS.EducationOrganisations.Domain.Configuration;
using SFA.DAS.EducationOrganisations.Domain.EducationalOrganisation;

namespace SFA.DAS.EducationalOrganisations.Data;

public interface IEducationalOrganisationDataContext
{
    DbSet<EducationalOrganisationEntity> EducationalOrganisationEntities { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken  = default (CancellationToken));
}
public class EducationalOrganisationDataContext : DbContext, IEducationalOrganisationDataContext
{
    private const string AzureResource = "https://database.windows.net/";
    private readonly ChainedTokenCredential _azureServiceTokenProvider;
    private readonly EnvironmentConfiguration _environmentConfiguration;
    public DbSet<EducationalOrganisationEntity> EducationalOrganisationEntities { get; set; }

    private readonly EducationOrganisationsConfiguration? _configuration;

    public EducationalOrganisationDataContext()
    {
    }

    public EducationalOrganisationDataContext(DbContextOptions options) : base(options)
    {
            
    }
    public EducationalOrganisationDataContext(IOptions<EducationOrganisationsConfiguration> config, DbContextOptions options, ChainedTokenCredential azureServiceTokenProvider, EnvironmentConfiguration environmentConfiguration) :base(options)
    {
        _azureServiceTokenProvider = azureServiceTokenProvider;
        _environmentConfiguration = environmentConfiguration;
        _configuration = config.Value;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
            
        if (_configuration == null 
            || _environmentConfiguration.EnvironmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase)
            || _environmentConfiguration.EnvironmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase))
        {
            return;
        }
            
        var connection = new SqlConnection
        {
            ConnectionString = _configuration.ConnectionString,
            AccessToken = _azureServiceTokenProvider.GetTokenAsync(new TokenRequestContext(scopes: new string[] { AzureResource })).Result.Token,
        };
            
        optionsBuilder.UseSqlServer(connection,options=>
            options.EnableRetryOnFailure(
                5,
                TimeSpan.FromSeconds(20),
                null
            ));

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EducationalOrganisationEntityConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}