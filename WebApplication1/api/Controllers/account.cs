using System.Security.Claims;
using System.Text;
using db;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.models;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication1.Controllers;


[Route("[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration _configuration;

    public AuthenticateController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost]  
    [Route("login")]  
    public async Task<IActionResult> Login([FromBody] login model)  
        {  
            var user = await userManager.FindByEmailAsync(model.email);  
            Console.WriteLine("121212" + user.ToString());
            if (user == null || !await userManager.CheckPasswordAsync(user, model.password))  
                return Unauthorized();  
            
            var userRoles = await userManager.GetRolesAsync(user);  

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };  

            foreach (var userRole in userRoles)  
            {  
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));  
            }  

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));  

            var token = new JwtSecurityToken(  
                issuer: _configuration["JWT:ValidIssuer"],  
                audience: _configuration["JWT:ValidAudience"],  
                expires: DateTime.Now.AddHours(3),  
                claims: authClaims,  
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
                );  

            return Ok(new  
            {  
                token = new JwtSecurityTokenHandler().WriteToken(token),  
                expiration = token.ValidTo  
            });
        }  
  
        [HttpPost]  
        [Route("register")]  
        public async Task<IActionResult> Register([FromBody] register model)  
        {  
            // var userExists = await userManager.FindByNameAsync(model.username);  
            var is_user = await userManager.FindByEmailAsync(model.email);
            if (is_user != null)  
                return StatusCode(StatusCodes.Status500InternalServerError);  
  
            User user = new User()  
            {  
                Email = model.email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName = model.username  
            };  
            var result = await userManager.CreateAsync(user, model.password);
            if (!result.Succeeded)  
                return BadRequest(result.Errors);  
  
            return Ok("User created successfully!");  
        }  
}