using CanThoTravel.Application.CQRS.Foods.Commands;
using CanThoTravel.Application.CQRS.Foods.Queries;
using CanThoTravel.Application.DTOs.Food;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CanThoTravel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FoodController : ControllerBase
    {
        private readonly ILogger<FoodController> _logger;
        private readonly IMediator _mediator;
        public FoodController(ILogger<FoodController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddFoodRequestDTO request)
        {
            var foodId = await _mediator.Send(new AddFoodCommand(request));
            return Ok(foodId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateFoodRequestDTO request)
        {
            var result = await _mediator.Send(new UpdateFoodCommand(request));
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteFoodRequestDTO request)
        {
            var result = await _mediator.Send(new DeleteFoodCommand(request.Id));
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var food = await _mediator.Send(new GetByIDFoodQuery(id));
            if (food == null)
            {
                return NotFound();
            }
            return Ok(food);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var foods = await _mediator.Send(new GetAllFoodQuery());
            if (foods == null || !foods.Any())
            {
                return NotFound();
            }
            return Ok(foods);
        }
    }
}
