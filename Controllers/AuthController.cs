using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web_authentication.Dto;
using web_authentication.entities;
using web_authentication.Interfaces;

namespace web_authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<AplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public AuthController(UserManager<AplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork) { 
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("addRole")]
        public async Task<IActionResult> Addrole([FromQuery] string role)
        {
            IdentityResult roleResult;
            bool adminRoleExists = await _roleManager.RoleExistsAsync(role);
            if (!adminRoleExists)
            {
               // _logger.LogInformation("Adding Admin role");
                roleResult = await _roleManager.CreateAsync(new IdentityRole(role));
                return Ok(roleResult.Succeeded);
            }

            //if(roleResult == ) { }
            return BadRequest();
           
           
        }

        [HttpGet("getUserAuth")]
        [Authorize]
        public async Task<IActionResult> GetUserAuth()
        {
            var user = await _unitOfWork.AuthentiCationRepository.GetUserAuth();
            
            return Ok(user);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var result = await _unitOfWork.AuthentiCationRepository.Register(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var token = await _unitOfWork.AuthentiCationRepository.Login(model);

                return Ok(token);
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
