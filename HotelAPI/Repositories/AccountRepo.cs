using Hotel.Data;
using HotelAPI.Data;
using HotelAPI.DTO;
using HotelAPI.Repositories.Interfaces;
using HotelAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelAPI.Repositories
{
    public class AccountRepo : IAccount 
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AccountRepo(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<IdentityResult> AddRoleAsync(RoleDTO model)
        {
            var role = new ApplicationRole
            {
                Name = model.roleName,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var result = await _roleManager.CreateAsync(role);
            return result;
        }


        public async Task<IdentityResult> AddUserAsync(RegisterUserDTO model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task<IdentityResult> AddUserRoleAsync(UserRoleDTO userRole)
        {
            IdentityResult result = null;
            var roleNameResult = await _roleManager.FindByNameAsync(userRole.RoleName);
            var user = await _userManager.FindByNameAsync(userRole.Username);
            if (user != null && roleNameResult != null)
            {
                result = await _userManager.AddToRoleAsync(user, roleNameResult.Name);
            }
            return result;
        }
        public async Task<List<ApplicationRole>> GetAllRolesAsync()
        {
            return await _dbContext.Roles.ToListAsync();
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<JwtSecurityToken> AuthenticatedWithToken(LoginDTO loginDTO, IConfiguration config)
        {
            JwtSecurityToken jwtSecurityToken = null;
            var user = await _userManager.FindByNameAsync(loginDTO.username);
            if (user != null)
            {
                bool IsAuthenticated = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                if (IsAuthenticated) // if password exist
                {
                    //claims Token
                    var CliamsTokenList = await CreateCliams(user);

                    //create SecurityKey
                    SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecretKey"]));  // create symantic key to pass in SigningCredentials
                    var signInCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  // for signature in token
                    jwtSecurityToken = new JwtSecurityToken(
                    issuer: config["JWT:Issuer"],   // url provider
                    audience: config["JWT:Audience"],  // url consumer
                    claims: CliamsTokenList,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signInCredential
                    );
                }
                else // if password doesn't exist
                {
                    return null;
                }
            }
            return jwtSecurityToken;
        }
        public async Task<List<Claim>> CreateCliams(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
