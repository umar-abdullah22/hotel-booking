using System.ComponentModel.DataAnnotations.Schema;

namespace HotelWebApp.ViewModel
{
    public class OrderVM
    {
        public int Id { get; set; }
        public DateTime orderCreation { get; set; }
        public int RoomId { get; set; }
        public RoomsVM Rooms { get; set; }
        public string UserId { get; set; }

    }
}
