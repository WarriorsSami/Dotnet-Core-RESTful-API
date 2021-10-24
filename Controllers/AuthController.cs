using Microsoft.AspNetCore.Mvc;
using WebApiCiCd.Data;

namespace WebApiCiCd.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController: Controller
    {
        private readonly IUserRepository _repository;
        
        public AuthController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("register")]
        public IActionResult Register()
        {
            return Ok("success");
        }
    }
}