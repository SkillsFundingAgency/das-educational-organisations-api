namespace SFA.DAS.EducationalOrganisations.Api.AppStart;

public static class AddServiceRegistrationExtension
{
    public static void AddServiceRegistration(this IServiceCollection services)
    {
        // services.AddScoped<IEducationOrganisationRepository, EducationOrganisationRepository>();
        // services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(GetAllEducationalOrganisationsQuery).Assembly));
    }
}