using DC.PicpaySim.Domain.Commands;
using DC.PicpaySim.Infrastructure.Repositories;
using DC.PicpaySim.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DC.PicpaySim.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            try
            {
                var userQuery = new GetUserQuery(id);

                var result = await _mediator.Send(userQuery);
                if (!result.isSuccess)
                {
                    if (result.Status == 404)
                        return NotFound();
                    else
                        return StatusCode(result.Status, result);
                }

                return Ok(result.Data);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            try
            {
                var resultNewUser = await _mediator.Send(command);
                if (!resultNewUser.isSuccess)
                {
                    if (resultNewUser.Status == 404)
                        return NotFound();
                    else
                        return StatusCode(resultNewUser.Status, resultNewUser);
                }

                return Created("User created sucessfully", resultNewUser.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
