using DC.PicpaySim.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DC.PicpaySim.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("/Wallet/{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                var query = new GetWalletQuery(id);

                var result = await _mediator.Send(query);
                if (result.isSuccess)
                    return Ok(result.Data);
                else
                {
                    if (result.Status == 404)
                        return NotFound();
                    else if (result.Status == 401)
                        return Unauthorized();

                    return StatusCode(result.Status, result);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateWalletCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (!result.isSuccess)
                {
                    if (result.Status == 404)
                        return NotFound();
                    else if (result.Status == 401)
                        return Unauthorized();

                    return StatusCode(result.Status, result);
                }

                return Ok(result.Data);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
