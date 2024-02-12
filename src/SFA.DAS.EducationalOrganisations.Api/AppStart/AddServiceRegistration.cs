using SFA.DAS.EducationalOrganisations.Application.Commands.GetAllEducationalOrganisations;
using SFA.DAS.EducationalOrganisations.Application.Services;
using SFA.DAS.EducationalOrganisations.Domain.Interfaces;

namespace SFA.DAS.EducationalOrganisations.Api.AppStart;

public static class AddServiceRegistrationExtension
{
    public static void AddServiceRegistration(this IServiceCollection services)
    {
        // services.AddScoped<IEducationOrganisationRepository, EducationOrganisationRepository>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllEducationalOrganisationsQuery).Assembly));
        services.AddTransient<IEdubaseService, EdubaseService>();
    }
}