using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.EducationalOrganisations.Data;
using SFA.DAS.EducationOrganisations.Domain.Configuration;

namespace SFA.DAS.EducationalOrganisations.Api.AppStart;

public static class DatabaseExtensions
{
    public static void AddDatabaseRegistration(this IServiceCollection services, EducationOrganisationsConfiguration config, string? environmentName)
    {
        services.AddHttpContextAccessor();
        if (environmentName.Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
        {
            services.AddDbContext<EducationalOrganisationDataContext>(options => options.UseInMemoryDatabase("SFA.DAS.EducationalOrganisation"), ServiceLifetime.Transient);
        }
        else if (environmentName.Equals("LOCAL", StringComparison.CurrentCultureIgnoreCase))
        {
            services.AddDbContext<EducationalOrganisationDataContext>(options=>options.UseSqlServer(config.ConnectionString),ServiceLifetime.Transient);
        }
        else
        {
            services.AddDbContext<EducationalOrganisationDataContext>(ServiceLifetime.Transient);    
        }
            
        services.AddSingleton(new EnvironmentConfiguration(environmentName));

        services.AddScoped<IEducationalOrganisationDataContext, EducationalOrganisationDataContext>(provider => provider.GetService<EducationalOrganisationDataContext>()!);
        services.AddScoped(provider => new Lazy<EducationalOrganisationDataContext>(provider.GetService<EducationalOrganisationDataContext>()!));
        services.AddSingleton(new ChainedTokenCredential(
            new ManagedIdentityCredential(),
            new AzureCliCredential(),
            new VisualStudioCodeCredential(),
            new VisualStudioCredential())
        );
    }
}