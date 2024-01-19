using HotelAPI.DTO;
using HotelAPI.Models;

namespace HotelAPI.Repositories.Interfaces
{
    public interface IBooking
    {
        public Task<IQueryable<Booking>> GetBookingsAsync();
        public Task<List<Booking>> GetBookingsByRoomIdAsync(int roomId);
        public Task<Booking> AddBookingAsync(Booking booking);
    }
}
