namespace HotelAPI.Models
{
    public class Rooms
    {
        public int Id { get; set; }
        public string? RoomName { get; set; }
        public int stars { get; set;}
        public decimal price { get; set; }
        public int person {  get; set; }
        public string View { get; set;}
        public int Beds { get; set; }  
        public string ?Description { get; set; }
        public string? imageURL { get; set; }


    }
}
