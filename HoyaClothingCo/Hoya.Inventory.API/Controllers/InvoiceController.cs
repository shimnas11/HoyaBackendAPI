using Hoya.Inventory.Application.BusinessLogic;
using Hoya.Inventory.Application.BusinessLogic.Invoice;
using Hoya.Inventory.Application.BusinessLogic.Products;
using Hoya.Inventory.Application.DTOs;
using Hoya.Inventory.Domain.DTO;
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
    public class InvoiceController : ControllerBase
    {

        private readonly IMediator _mediator;

        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceRequestDto request)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var command = request.Adapt<InvoiceCreateCommand>();

            var id = await _mediator.Send(command);
            return Ok(id);

        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var result = await _mediator.Send(new GetInvoiceQuery());
            return Ok(result);

        }

        [HttpPost("return/{invoiceId}")]
        public async Task<IActionResult> Return(string invoiceId, [FromBody] ReturnOrderDTO returnOrder)
        {

            var result = await _mediator.Send(new InvoiceProductReturnCommand(invoiceId, returnOrder));
            return Ok(result);

        }
    }
}
