using Hotel.Data;
using HotelAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelAPI.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime orderCreation { get; set; }
        public int RoomId { get; set; }
        public string UserId { get; set; }
    }
}
