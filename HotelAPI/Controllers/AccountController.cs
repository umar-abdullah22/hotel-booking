using HotelAPI.DTO;
using HotelAPI.Repositories.Interfaces;
using HotelAPI.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _account;
        private readonly IConfiguration _configuration;
        public AccountController(IAccount admin, IConfiguration configuration)
        {
            this._account = admin;
            _configuration = configuration; 
        }
        // create new user "Registration"  Post Method
        [HttpPost("register")] // api/Account/register
        public async Task<IActionResult> Registration(RegisterUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                if (userDTO != null)
                {
                    var result = await _account.AddUserAsync(userDTO);
                    if (result.Succeeded)
                    {
                        UserRoleDTO userRoleDTO = new UserRoleDTO()  // by default add role "User" to all new Users
                        {
                            RoleName = "User",
                            Username = userDTO.Username
                        };
                        var userRoleResult = await _account.AddUserRoleAsync(userRoleDTO);
                        if (userRoleResult.Succeeded)
                            return Ok("User Added Sucessfully");
                        else
                            return BadRequest(result.Errors.FirstOrDefault());
                    }
                    else
                    {
                        return BadRequest(result.Errors.FirstOrDefault());
                    }

                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("AddRole")] // api/Account/AddRole
        public async Task<IActionResult> AddRole(RoleDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                if (userDTO != null)
                {
                    var result = await _account.AddRoleAsync(userDTO);
                    if (result.Succeeded)
                    {
                        return Ok("Role Added Sucessfully");
                    }
                    else
                    {
                        return BadRequest(result.Errors.FirstOrDefault());
                    }
                }
            }
            return BadRequest(ModelState);
        }



        // Check Account valid "Login"  Post Method
        [HttpPost("Login")]  // api/Account/Login
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                // check user
                var jwtResult = await _account.AuthenticatedWithToken(loginDTO, _configuration); // check user and generate token
                if (jwtResult != null)
                {
                    return Ok(new
                    {
                        token= new JwtSecurityTokenHandler().WriteToken(jwtResult),
                        exprireDate = jwtResult.ValidTo
                    });
     
                }
                else
                {
                    return Unauthorized("username or password incorrect");
                }
            }
            return Unauthorized();
        }
        [HttpPost("Logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return Ok();
        }



    }
}
