namespace SFA.DAS.EducationalOrganisations.Api.AppStart;

public static class ServiceProviderExtensions
{
    public static ILogger GetLogger(this ServiceProvider serviceProvider, string typeName) => serviceProvider.GetService<ILoggerProvider>().CreateLogger(typeName);
}
