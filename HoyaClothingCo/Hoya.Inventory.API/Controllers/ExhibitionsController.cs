using Hoya.Inventory.Application.BusinessLogic.Exhibitions;
using Hoya.Inventory.Application.BusinessLogic.Invoice;
using Hoya.Inventory.Application.DTOs;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hoya.Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExhibitionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExhibitionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExhibition(CreateExhibitionDto exhibitionDto)
        {
            var command = exhibitionDto.Adapt<CreateExhibitionCommand>();

            // Implement exhibition creation logic here
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            // Implement exhibition creation logic here
            return Ok(await _mediator.Send(new GetExhibitionQuery()));
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Get(string id)
        {

            // Implement exhibition creation logic here
            return Ok(await _mediator.Send(new GetExhibitionDetailsQuery(id)));
        }

        [HttpPost("Expenses")]
        public async Task<IActionResult> CreateExhibitionExpense(CreateExpenseDto expense)
        {
            var command = expense.Adapt<CreateExhibitionExpenseCommand>();

            // Implement exhibition creation logic here
            return Ok(await _mediator.Send(command));
        }


        [HttpGet("Overview/{id}")]
        public async Task<IActionResult> GetExhibitionOverview(string id)
        {
            return Ok(await _mediator.Send(new GetExhibitionOverviewQuery(id)));
        }
    }
}
