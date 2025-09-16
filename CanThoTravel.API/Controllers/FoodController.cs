using CanThoTravel.Application.CQRS.Foods.Commands;
using CanThoTravel.Application.CQRS.Foods.Queries;
using CanThoTravel.Application.DTOs.Food;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CanThoTravel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly ILogger<FoodController> _logger;
        private readonly IMediator _mediator;
        public FoodController(ILogger<FoodController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> GetByIdAsync(GetFoodDTO request)
        {
            var food = await _mediator.Send(new GetByIDFoodQuery(request.Id));
            if (food == null)
            {
                return NotFound();
            }
            return Ok(food);
        }
    }
}
