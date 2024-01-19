using HotelAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAPI.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Time { get; set; }
        public int RoomId { get; set; }

        public string UserId { get; set; }

    }
}
