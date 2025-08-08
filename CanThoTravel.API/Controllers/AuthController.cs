using CanThoTravel.Application.CQRS.Members.Commands;
using CanThoTravel.Application.CQRS.Members.Queries;
using CanThoTravel.Application.DTOs.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CanThoTravel.Application.CQRS.Authentication.Queries;

namespace CanThoTravel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("generate-token")]
        public async Task<IActionResult> GenerateToken([FromBody] GenerateTokenRequestDTO request)
        {
            var token = await _mediator.Send(new GenerateTokenQuery(request));
            if (token == null)
            {
                return NotFound("Member not found.");
            }
            return Ok(new { Token = token });
        }
    }
}
