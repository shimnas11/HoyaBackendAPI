using Hoya.Inventory.Application.BusinessLogic.Products;
using Hoya.Inventory.Application.DTOs;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hoya.Inventory.API.Controllers
{
    [ApiController]
    [Route("api/Products")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequestDto request)
        {

            var command = request.Adapt<CreateProductCommand>();

            var id = await _mediator.Send(command);
            return Ok(id);
            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetProductQuery());
            return Ok(result);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(string id,[FromBody] ProductUpdateRequestDto request)
        {
            var command = request.Adapt<UpdateProductCommand>();
            
            var response = await _mediator.Send(command);
            return Ok(response);

        }
    }

    

    
}
