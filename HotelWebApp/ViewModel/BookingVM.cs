using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.ViewModel
{
    public class BookingVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Insert Name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Insert CheckIn")]
        public DateTime CheckIn { get; set; }
        [Required(ErrorMessage = "Insert CheckOut")]
        public DateTime CheckOut { get; set; }
        [Required(ErrorMessage = "Insert Adults")]
        public int Adults { get; set; }
        public int Children { get; set; }
        [Required(ErrorMessage = "Insert Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Insert Time")]
        public DateTime Time { get; set; }

        public int RoomId { get; set; }

        public string UserId { get; set; }
    }
}
