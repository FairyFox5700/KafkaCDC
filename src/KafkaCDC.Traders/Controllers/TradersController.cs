using KafkaCDC.Traders.Commands;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace KafkaCDC.Traders.Controllers
{
    [Route("api/trader")]
    [ApiController]
    public class TradersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TradersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> AddTrader([FromBody] TraderCreatedCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPost("subscription")]
        public async Task<ActionResult> AddSubscription([FromBody] TraderSubscriptionAddedCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("suscription")]
        public async Task<ActionResult> AddTrader([FromBody] TraderSubscriptionChangedCommand command)
            => Ok(await _mediator.Send(command));
    }
}
