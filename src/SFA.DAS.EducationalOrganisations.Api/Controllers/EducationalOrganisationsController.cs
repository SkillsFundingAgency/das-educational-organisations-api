﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.EducationalOrganisations.Api.Responses;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetAllEducationalOrganisations;
using SFA.DAS.EducationalOrganisations.Application.Queries.GetEducationalOrganisationById;
using SFA.DAS.EducationalOrganisations.Application.Queries.SearchEducationalOrganisations;

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
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

            var response = (GetEducationalOrganisationByIdResponse)result;

            return Ok(response);
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(SearchEducationalOrganisationsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Search([FromQuery] string searchTerm, [FromQuery] int maximumResults = 500)
        {
            var result = await _mediator.Send(new SearchEducationalOrganisationsQuery
            {
                SearchTerm = searchTerm,
                MaximumResults = maximumResults
            });

            if (result.EducationalOrganisations == null
                || !result.EducationalOrganisations.Any())
            {
                return NotFound();
            }

            var response = (SearchEducationalOrganisationsResponse)result;

            return Ok(response);
        }
    }
}