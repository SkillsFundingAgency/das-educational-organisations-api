using Microsoft.Extensions.Logging.ApplicationInsights;
using SFA.DAS.EducationalOrganisations.Application.Infrastructure;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetAllEducationalOrganisations;
using SFA.DAS.EducationalOrganisations.Application.Services;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Api.AppStart;

public static class AddServiceRegistrationExtension
{
    public static void AddServiceRegistration(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllEducationalOrganisationsQuery).Assembly));
        services.AddTransient<IEdubaseService, EdubaseService>();
        services.AddTransient<IEdubaseSoapService, EdubaseSoapService>();

        services.AddTransient<IEducationalOrganisationEntityService, EducationalOrganisationEntityService>();
        services.AddTransient<IEducationalOrganisationImportService, EducationalOrganisationImportService>();

        services.AddTransient<IEdubaseClientFactory, EdubaseClientFactory>();
        services.AddTransient<IOrganisationTypeHelper, OrganisationTypeHelper>();

        services.AddLogging(builder =>
        {
            builder.AddFilter<ApplicationInsightsLoggerProvider>(string.Empty, LogLevel.Information);
            builder.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Information);
        });

        services.AddApplicationInsightsTelemetry();
    }
}