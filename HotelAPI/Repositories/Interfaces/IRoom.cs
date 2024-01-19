using HotelAPI.Models;

namespace HotelAPI.Repositories.Interfaces
{
    public interface IRoom
    {
        public Task<List<Rooms>> GetRooms();
    }
}
