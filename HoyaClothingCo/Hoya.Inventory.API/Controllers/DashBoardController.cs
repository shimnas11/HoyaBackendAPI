using Hoya.Inventory.Application.BusinessLogic.Dashboard;
using Hoya.Inventory.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hoya.Inventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashBoardController : ControllerBase
    {


        private readonly IMediator _mediator;

        public DashBoardController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetDashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            // Implement exhibition creation logic here
            return Ok(await _mediator.Send(new GetDashboardOverviewQuery()));
        }

        
        [HttpGet("hotSellingProducts")]
        public async Task<IActionResult> GetHotSellingProducts()
        {
            // Implement exhibition creation logic here
            return Ok(await _mediator.Send(new GetHotSellingProductQuery()));
        }

        [HttpGet("getExhibitionOverview")]
        public async Task<IActionResult> getExhibitionOverview()
        {
            // Implement exhibition creation logic here
            return Ok(await _mediator.Send(new GetExhibitionOverviewQuery()));
        }
    }
}
