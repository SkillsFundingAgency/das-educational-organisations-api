using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EducationalOrganisations.Application.Commands.ImportEducationalOrganisations;

namespace SFA.DAS.EducationalOrganisations.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("/ops/[controller]")]
    public class DataLoadController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DataLoadController> _logger;

        public DataLoadController(
            IMediator mediator,
            ILogger<DataLoadController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Import()
        {
            try
            {
                _logger.LogInformation("Organisations import request received");
                await _mediator.Send(new ImportEducationalOrganisationsCommand());
                _logger.LogInformation("Organisations import completed successfully");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during data import");
                return BadRequest(ex.Message);
            }           
        }
    }
}
