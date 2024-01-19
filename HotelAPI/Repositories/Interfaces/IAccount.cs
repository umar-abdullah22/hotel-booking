using Hotel.Data;
using HotelAPI.Data;
using HotelAPI.DTO;
using HotelAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HotelAPI.Repositories.Interfaces
{
    public interface IAccount
    {
        public Task<List<ApplicationUser>> GetAllUsersAsync();
        public Task<IdentityResult> AddUserAsync(RegisterUserDTO model);
        public Task<JwtSecurityToken> AuthenticatedWithToken(LoginDTO loginDTO, IConfiguration config);
        public Task<List<Claim>> CreateCliams(ApplicationUser user);
        ///////////////////////////////////////////////////////////////////
        public Task<List<ApplicationRole>> GetAllRolesAsync();
        public Task<IdentityResult> AddRoleAsync(RoleDTO model);
        ///////////////////////////////////////////////////////////////////
        public Task<IdentityResult> AddUserRoleAsync(UserRoleDTO userRole);


    }
}
