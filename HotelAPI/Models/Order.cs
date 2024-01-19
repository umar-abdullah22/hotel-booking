using Hotel.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime orderCreation {  get; set; }
        [ForeignKey("Rooms")]
        public int RoomId { get; set; }
        public Rooms Rooms { get; set; }
        [ForeignKey("user")]
        public string UserId { get; set; }
        public ApplicationUser user { get; set; }


    }
}
