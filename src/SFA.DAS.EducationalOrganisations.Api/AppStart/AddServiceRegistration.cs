using SFA.DAS.EducationOrganisations.Application.Commands.GetAllEducationalOrganisations;
using SFA.DAS.EducationOrganisations.Application.Factories;
using SFA.DAS.EducationOrganisations.Application.Interfaces;
using SFA.DAS.EducationOrganisations.Application.Services;

namespace SFA.DAS.EducationalOrganisations.Api.AppStart;

public static class AddServiceRegistrationExtension
{
    public static void AddServiceRegistration(this IServiceCollection services)
    {
        // services.AddScoped<IEducationOrganisationRepository, EducationOrganisationRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllEducationalOrganisationsQuery).Assembly));
        services.AddTransient<IEdubaseService, EdubaseService>();
        services.AddTransient<IEdubaseClientFactory, EdubaseClientFactory>();


    }
}