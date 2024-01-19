using System.ComponentModel.DataAnnotations;

namespace HotelAPI.ViewModels
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "please Insert username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "please Insert Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "please Insert password")]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string confirmPassword { get; set; }
    }
}
