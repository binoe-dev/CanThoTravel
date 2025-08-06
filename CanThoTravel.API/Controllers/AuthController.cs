using CanThoTravel.Application.CQRS.Members.Commands;
using CanThoTravel.Application.CQRS.Members.Queries;
using CanThoTravel.Application.DTOs.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            try
            {
                var result = await _mediator.Send(new RegisterMemberCommand(request));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            try
            {
                var result = await _mediator.Send(new LoginQuery(request));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
