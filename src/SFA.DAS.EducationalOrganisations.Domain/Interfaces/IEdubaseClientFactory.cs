using EdubaseSoap;

namespace SFA.DAS.EducationalOrganisations.Domain.Interfaces
{
    public interface IEdubaseClientFactory
    {
        IEdubaseClient Create();
    }
}
