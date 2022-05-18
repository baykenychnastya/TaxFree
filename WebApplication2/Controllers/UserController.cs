using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;

        public UserController(IConfiguration config)
        {
            _config = config;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserModel userLogin)
        {
            var user = Authenticate(userLogin);
            
            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        [HttpPost("signUp")]
        public ActionResult<UserModel> SignUp(UserModel userSignUp)
        {

            using (var context = new TaxFreeContext(null))
            {
                var user = context.Users.ToList().FirstOrDefault(o => o.EmailAddress.ToLower() == userSignUp.EmailAddress.ToLower());

                if(user != null)
                {
                    return BadRequest(new { message = $"User with email { userSignUp.EmailAddress } already exist!" });
                }
                UserModel newUser = new UserModel
                {
                    EmailAddress = userSignUp.EmailAddress,
                    Password = SecurePasswordHasher.Hash(userSignUp.Password)
                };

                context.Users.Add(newUser);
                context.SaveChanges();
                var token = Generate(newUser);
                return Ok(token);
            }
        }
        
        private string Generate(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_config["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
            new Claim(ClaimTypes.Email, user.EmailAddress),
            new Claim("Id", user.Id.ToString()),

              }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

        private UserModel Authenticate(UserModel userLogin) 
        {
            using var context = new TaxFreeContext(null);   

            var currentUser = context.Users.ToList().FirstOrDefault(o => o.EmailAddress.ToLower() == userLogin.EmailAddress.ToLower() && SecurePasswordHasher.Verify(userLogin.Password, o.Password));

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
