namespace SFA.DAS.EducationalOrganisations.Domain.Interfaces
{
    public interface IEdubaseService
    {
        Task<bool> PopulateStagingEducationalOrganisations();
    }
}