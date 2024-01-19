using System.ComponentModel.DataAnnotations;
namespace HotelAPI.DTO
{
    public class RoleDTO
    {
        [Required]
        public string roleName { get; set; }
    }
}
