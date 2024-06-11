using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EducationalOrganisations.Api.Responses;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetAllEducationalOrganisations;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetEducationalOrganisationById;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetIdentifiableOrganisationTypes;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetLatestDetails;
using SFA.DAS.EducationalOrganisations.Application.Queries.SearchEducationalOrganisations;
using SFA.DAS.EducationalOrganisations.Domain.DTO;
using SFA.DAS.EducationalOrganisations.Domain.Exceptions;

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
            IMediator mediator, ILogger<EducationalOrganisationsController> logger,
            IConfiguration configuration)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(GetAllEducationalOrganisationsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllEducationalOrganisationsQuery());

            var response = (GetAllEducationalOrganisationsResponse)result;

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(GetEducationalOrganisationByIdResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetEducationalOrganisationByIdQuery
            {
                Id = id
            });

            var response = (GetEducationalOrganisationByIdResponse)result;

            return Ok(response);
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(SearchEducationalOrganisationsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Search([FromQuery] string searchTerm, [FromQuery] int maximumResults = 500)
        {          
            var result = await _mediator.Send(new SearchEducationalOrganisationsQuery
            {
                SearchTerm = searchTerm,
                MaximumResults = maximumResults
            });

            var response = (SearchEducationalOrganisationsResponse)result;

            return Ok(response);
        }

        [HttpGet]
        [Route("IdentifiableOrganisationTypes")]
        [ProducesResponseType(typeof(OrganisationType[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIdentifiableOrganisationTypes()
        {
            var query = new GetIdentifiableOrganisationTypesQuery();

            var results = await _mediator.Send(query);

            return Ok(results.OrganisationTypes);
        }

        [HttpGet]
        [Route("LatestDetails")]
        [ProducesResponseType(typeof(GetLatestDetailsResponse[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetLatestDetails(string identifier)
        {
            var query = new GetLatestDetailsQuery
            {
                Identifier = identifier
            };

            try
            {
                var result = await _mediator.Send(query);
                var response = (GetLatestDetailsResponse)result;

                return Ok(response);
            }
            catch (BadOrganisationIdentifierException e)
            {
                return BadRequest(e.Message);
            }
            catch (OrganisationNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}