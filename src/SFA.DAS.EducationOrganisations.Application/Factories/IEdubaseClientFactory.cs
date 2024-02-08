using Dfe.Edubase2.SoapApi.Client;

namespace SFA.DAS.EducationOrganisations.Application.Factories
{
    public interface IEdubaseClientFactory
    {
        IEstablishmentClient Create();
    }
}
