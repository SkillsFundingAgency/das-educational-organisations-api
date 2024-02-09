namespace SFA.DAS.EducationalOrganisations.Domain.Configuration;

public class EnvironmentConfiguration(string environmentName)
{
    public string EnvironmentName { get;} = environmentName;
}