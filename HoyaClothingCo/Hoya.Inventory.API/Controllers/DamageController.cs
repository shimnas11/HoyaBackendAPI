using Hoya.Inventory.Application.BusinessLogic.Misc;
using Hoya.Inventory.Application.BusinessLogic.Products;
using Hoya.Inventory.Application.DTOs;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hoya.Inventory.API.Controllers
{
    [ApiController]
    [Route("api/damage")]
    public class DamageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DamageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDamageDto request)
        {

            var command = request.Adapt<MarkProductDamagedCommand>();
            return Ok(await _mediator.Send(command));

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var result = await _mediator.Send(new GetDamagedProductQuery());
            return Ok(result);

        }
    }
}
