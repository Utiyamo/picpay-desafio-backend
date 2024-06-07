using DC.PicpaySim.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DC.PicpaySim.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _userRepository.FindById(0);
                if(result == null)
                    return NotFound();

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
            
        }
    }
}
