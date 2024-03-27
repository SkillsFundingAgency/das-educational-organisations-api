using EdubaseSoap;

namespace SFA.DAS.EducationalOrganisations.Domain.Interfaces
{
    public interface IEdubaseSoapService
    {
        Task<FindEstablishmentsResponse> FindEstablishmentsAsync(EstablishmentFilter filter);
    }
}
