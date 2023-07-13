using web_authentication.entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using web_authentication.Interfaces;
using web_authentication.Dto;
using web_authentication.Data;

namespace web_authentication.Repository
{
    public class AuthenticationRepository : IAuthentiCationRepository
    {
        private readonly UserManager<AplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IHttpContextAccessor _httpContextAccessor;
        private IConfiguration _configuration;
        private DataContext _dataContext;

        public AuthenticationRepository(
            IHttpContextAccessor httpContextAccessor,
            DataContext dataContext,IConfiguration configuration,UserManager<AplicationUser> userManager, RoleManager<IdentityRole> roleManager) { 
                _userManager = userManager;
                _roleManager = roleManager;
                _configuration = configuration;
               _dataContext = dataContext;
               _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new Exception("UserName or Password incorrect !");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToString("dd/MM/yyyy"))
            };

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
               claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                expires: DateTime.Now.AddMinutes(30),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(RegisterModel model)
        {
             var checkUser = await _userManager.FindByNameAsync(model.UserName);
             if (checkUser != null)
             {
                 throw new Exception("User Name existed");
             }
            var user = new AplicationUser()
            {
                Email = model.Email,
                Name = model.UserName,
                UserName = model.UserName,
            };
            var createUser = await _userManager.CreateAsync(user,model.Password);

            if(!createUser.Succeeded) {
                throw new Exception("create user Failed "+createUser.Errors.ToString());
            }

            foreach(var role in model.Roles)
            {
              
                var addRole = await _userManager.AddToRoleAsync(user, role);

                if (!addRole.Succeeded)
                {
                    throw new Exception("add" + role +" role failed " + addRole.Errors.ToString());
                }
            }
            

            return true;
           
        }

        public async Task<AplicationUser> GetUserAuth()
        {

            //var x = User
           var userName = _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.Name).Select(x => x.Value).FirstOrDefault();
            var user = await _userManager.FindByNameAsync(userName);
            
            return user;
        }
    }
}
