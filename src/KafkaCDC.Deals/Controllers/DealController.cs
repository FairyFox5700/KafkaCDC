using KafkaCDC.Deals.Comands;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace KafkaCDC.Deals.Controllers
{
    [Route("api/deal")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DealController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<ActionResult> AddDeal([FromBody] AddDealCommand command)
            => Ok(await _mediator.Send(command));


        [HttpPut()]
        public async Task<ActionResult> UpdateDealPrice([FromBody] UpdateDealPriceCommand command)
            => Ok(await _mediator.Send(command));
    }
}