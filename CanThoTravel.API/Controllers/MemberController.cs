using CanThoTravel.Application.CQRS.Members.Commands;
using CanThoTravel.Application.CQRS.Members.Queries;
using CanThoTravel.Application.DTOs.Authentication;
using CanThoTravel.Application.DTOs.Member;
using CanThoTravel.Application.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CanThoTravel.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IMediator _mediator;

        public MemberController(ILogger<MemberController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;                                   
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllMembersQuery());
            if (result == null || !result.Any())
            {
                return NotFound("No members found.");
            }
            return Ok(result);
        }       

        [HttpPost("getbyid")]
        public async Task<IActionResult> GetById(GetMemberDTO request)
        {
            var userContext = HttpContext.Items["UserContext"] as UserContext;

            if (userContext != null)
            {
                // You can now access user properties like:
                // userContext.Id
                // userContext.Name
                // userContext.Email
                // userContext.Type

                _logger.LogInformation("User {UserId} ({UserType}) is requesting data for member {MemberId}", userContext.Id, userContext.Type, request.Id);
            }

            if (request.Id <= 0)
            {
                return BadRequest("ID must be a positive integer.");
            }
            var result = await _mediator.Send(new GetByIDMembersQuery(request.Id));
            if (result == null)
            {
                return NotFound($"Member with ID {request.Id} not found.");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddMember([FromBody] AddMemberRequestDTO request)
        {
            var id = await _mediator.Send(new AddMemberCommand(request));
            if (id <= 0)
            {
                return BadRequest("Failed to add member.");
            }

            return Ok($"Member {id} added successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMember([FromBody] UpdateMemberRequestDTO request)
        {
            if (request.Id <= 0)
            {
                return BadRequest("ID must be a positive integer.");
            }
            await _mediator.Send(new UpdateMemberCommand(request));
            return Ok($"Member with ID {request.Id} updated successfully.");
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