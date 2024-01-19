using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Insert Username")]
        public string username { get; set; }
        [Required(ErrorMessage = "Insert Password")]
        public string password { get; set; }
    }
}
