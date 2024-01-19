using HotelAPI.Data;
using HotelAPI.DTO;
using HotelAPI.Models;
using HotelAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Repositories
{
    public class BookingRepo : IBooking
    {
        private readonly AppDbContext _dbContext;

        public BookingRepo(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            if (booking == null)
            {
                return null;
            }
            await _dbContext.booking.AddAsync(booking);
            await _dbContext.SaveChangesAsync();
            return booking;
        }

        public async Task<IQueryable<Booking>> GetBookingsAsync()
        {
            return _dbContext.booking.AsQueryable();
        }
        public async Task<List<Booking>> GetBookingsByRoomIdAsync(int roomId)
        {
            return await _dbContext.booking.Where(x=>x.RoomId == roomId).ToListAsync();
        }
    }
}
