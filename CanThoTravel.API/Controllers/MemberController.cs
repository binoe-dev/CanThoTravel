using CanThoTravel.Application.Service.Member;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CanThoTravel.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IMemberService _memberService;

        public MemberController(ILogger<MemberController> logger, IMemberService memberService)
        {
            _logger = logger;
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _memberService.Get();
            return Ok(result);
        }
    }
}