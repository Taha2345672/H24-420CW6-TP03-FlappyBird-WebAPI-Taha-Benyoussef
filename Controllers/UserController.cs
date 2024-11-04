
using FlappyBird.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.DTO;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Route("api/[controller]/[action]")]
        [ApiController]
        public class UsersController : ControllerBase
        {
            readonly UserManager<User> UserManager;

            public UsersController(UserManager<User> userManager)
            {
                UserManager = userManager;
            }
            [HttpPost]

            async public Task<ActionResult> Register(RegisterDTO register)

            {

                if (register.Password != register.PasswordConfirm)

                {

                    return StatusCode(StatusCodes.Status400BadRequest,

                        new { Message = "Les deux mots de passe spécifiés sont différents" });

                }


                User user = new User()

                {

                    UserName = register.UserName,

                    Email = register.Email

                };

                IdentityResult ir = await this.UserManager.CreateAsync(user, register.Password);

                if (!ir.Succeeded)

                {

                    return StatusCode(StatusCodes.Status500InternalServerError,

                        new { Message = "La création de l'utilisateur a échoué." });

                }

                return Ok();

            }

            [HttpPost]

            public async Task<ActionResult> Login(LoginDTO login)

            {

                User user = await UserManager.FindByNameAsync(login.UserName);

                if (user != null && await UserManager.CheckPasswordAsync(user, login.Password))

                {

                    //IList<string> roles = await UserManager.GetRolesAsync(user);
                    List<Claim> authClaims = new List<Claim>();

                    //foreach (string role in roles)                

                    //{  // authClaims.Add(new Claim(ClaimTypes.Role, role));              
                    //}
                    authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

                    SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Loo00gue Phrase SinON!"));

                    JwtSecurityToken token = new JwtSecurityToken(

                        issuer: "https://localhost:7075",

                        audience: "http://localhost:4200",

                        claims: authClaims,

                        expires: DateTime.Now.AddMinutes(30),

                        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

                        );

                    return Ok(new
                    {

                        token = new JwtSecurityTokenHandler().WriteToken(token),

                        validTo = token.ValidTo

                    });

                }

                else
                {

                    return StatusCode(StatusCodes.Status400BadRequest,

                        new { Message = "Le nom d'utilisateur ou le mot de passe est invalide." });

                }

            }
        }
    }
}




