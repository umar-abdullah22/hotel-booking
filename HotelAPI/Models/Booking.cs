using Hotel.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Adults { get; set; }
        public int Children {  get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Time {  get; set; }

        [ForeignKey("Rooms")]
        public int RoomId { get; set; }
        public Rooms Rooms { get; set; }
        [ForeignKey("user")]
        public string UserId { get; set; }
        public ApplicationUser user { get; set; }

    }
}
