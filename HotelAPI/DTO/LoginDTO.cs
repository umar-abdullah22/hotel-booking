using System.ComponentModel.DataAnnotations;

namespace HotelAPI.DTO
{
    public class LoginDTO
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
