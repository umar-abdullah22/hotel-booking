using System.ComponentModel.DataAnnotations;

namespace HotelAPI.ViewModels
{
    public class UserRoleDTO
    {

        [Required(ErrorMessage = "please Insert Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "please Insert RoleName")]
        public string RoleName { get; set; }
    }
}
