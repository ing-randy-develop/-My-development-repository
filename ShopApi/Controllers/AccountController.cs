using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopApi.Dtos;
using ShopApi.Mapping;
using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ShopApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager,RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, IConfiguration configuration){
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            this._configuration = configuration;
        }
        //[Route("Create")]
        //[HttpPost]
        //public async Task<IActionResult> CreateUser([FromBody] UserDto model) 
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var newUser = UserMapper.UserDtoToEntity(model);
        //        var result = await _userManager.CreateAsync(newUser, model.password);
        //        await _roleManager.CreateAsync(new IdentityRole(model.role));
        //        await _userManager.AddToRoleAsync(newUser, model.role.ToString());
        //        if (result.Succeeded)
        //        {
        //            return BuildToken(model);
        //        }
        //        else
        //        {
        //            return BadRequest("Username or password invalid");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest(ModelState);
        //    }
        //}
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserInf userInfo) 
        {
            if (ModelState.IsValid) 
            {
                User userLogin =await _userManager.FindByNameAsync(userInfo.UserName);
                var result = await _signInManager.PasswordSignInAsync(userLogin.UserName, userInfo.Password, true ,lockoutOnFailure: false);
                    if (result.Succeeded)
                {
                    return BuildToken(UserMapper.UserToDto(userLogin));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attemp.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private IActionResult BuildToken(UserDto userInfo) 
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["secret_key"] ));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(1);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "shopapi.com",
                audience: "shopapi.com",
                claims: claims,
                expires: expiration,
                signingCredentials: creds);
            return Ok(new 
            { 
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expiration
            });
        }
    }
}
