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

        [Authorize(Roles = "user")]
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
           
            try
            {
                /* var user = new AplicationUser()
                 {
                     Email = "user2312313",
                     Name = "use13",
                     UserName = "username123"
                 };
                 var result = await _userManager.CreateAsync(user,"12312312A!ewrsdf");*/
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
            var token = await _unitOfWork.AuthentiCationRepository.Login(model);
            var user = await _userManager.FindByNameAsync(model.UserName);
            var checkpass = await _userManager.CheckPasswordAsync(user,model.Password);
            return Ok(token);
        }

    }
}
