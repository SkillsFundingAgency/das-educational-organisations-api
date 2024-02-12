using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EducationalOrganisations.Api.Responses;
using SFA.DAS.EducationalOrganisations.Application.Commands.GetAllEducationalOrganisations;
using SFA.DAS.EducationalOrganisations.Application.Commands.GetEducationalOrganisationById;

namespace SFA.DAS.EducationalOrganisations.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/api/[controller]/")]
    public class EducationalOrganisationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<EducationalOrganisationsController> _logger;

        public EducationalOrganisationsController(
            IMediator mediator,
            ILogger<EducationalOrganisationsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllEducationalOrganisationsQuery());

            var response = (GetAllEducationalOrganisationsResponse)result;

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetEducationalOrganisationByIdQuery
            {
                Id = id
            });

            if (result.EducationalOrganisation == null)
            {
                return NotFound();
            }
            return Ok(result.EducationalOrganisation);
        }
    }
}
