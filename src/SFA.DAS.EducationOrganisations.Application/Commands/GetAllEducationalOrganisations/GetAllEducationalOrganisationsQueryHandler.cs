using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.EducationalOrganisations.Data;
using SFA.DAS.EducationOrganisations.Application.Interfaces;

namespace SFA.DAS.EducationOrganisations.Application.Commands.GetAllEducationalOrganisations
{
    public class GetAllEducationalOrganisationsQueryHandler : IRequestHandler<GetAllEducationalOrganisationsQuery, Unit>
    {
        private readonly EducationalOrganisationDataContext _dbContext;
        private readonly ILogger<GetAllEducationalOrganisationsQueryHandler> _logger;
        private readonly IEdubaseService _edubaseService;

        public GetAllEducationalOrganisationsQueryHandler(ILogger<GetAllEducationalOrganisationsQueryHandler> logger, 
                IEdubaseService edubaseService, 
                EducationalOrganisationDataContext dbContext)
        {
            _logger = logger;
            _edubaseService = edubaseService;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(GetAllEducationalOrganisationsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Attempting ImportOrganisationsCommand");

            var organisations = await _edubaseService.GetOrganisations();
            
            _logger.LogInformation($"Retrieved educational organisations with TotalCount: {organisations.Count}");

            //if (organisations == null || organisations.Count == 0) return Unit.Value ;

            //_dbContext.EducationalOrganisationEntities.Ups
            return Unit.Value;
        }
    }
}
