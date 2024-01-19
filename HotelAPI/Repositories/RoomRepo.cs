using Hotel.Data;
using HotelAPI.Data;
using HotelAPI.Models;
using HotelAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Repositories
{
    public class RoomRepo:IRoom
    {
        private readonly AppDbContext _dbContext;

        public RoomRepo(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Rooms>> GetRooms()
        {
            return await _dbContext.Room.ToListAsync();
        }
    }
}
