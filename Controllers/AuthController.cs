using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCiCd.Entities;
using WebApiCiCd.Helpers;
using WebApiCiCd.Models;
using WebApiCiCd.Services;

namespace WebApiCiCd.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController: Controller
    {
        #region Constructor
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }
        #endregion

        #region Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            
            return Created("success", await _repository.Create(user));
        }
        #endregion

        #region Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _repository.GetByEmail(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return BadRequest(new {message = "Invalid Credentials"});

            var jwt = _jwtService.Generate(user.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });
            
            return Ok(new
            {
                message = "success"
            });
        }
        #endregion

        #region User
        [HttpGet("user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                var token = _jwtService.Verify(jwt);

                var userId = int.Parse(token.Issuer);
                var user = await _repository.GetById(userId);

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        #endregion
        
        #region Logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            
            return Ok(new
            {
                message = "success"
            });
        }
        #endregion
        
        #region Users
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];
                _jwtService.Verify(jwt);
                
                return Ok(await _repository.GetAll());
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
        #endregion
    }
}