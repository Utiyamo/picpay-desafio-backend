using DC.PicpaySim.Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DC.PicpaySim.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] AuthUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if(!result.isSuccess)
                {
                    if (result.Status == 404)
                        return NotFound();
                    else
                        return StatusCode(result.Status, result);
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
