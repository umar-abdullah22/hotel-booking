using Hotel.Data;
using HotelAPI.Data;
using HotelAPI.Models;
using HotelAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Repositories
{
    public class OrderRepo : IOrder
    {
        private readonly AppDbContext _dbContext;

        public OrderRepo(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            if (order == null)
            {
                return null;
            }
            await _dbContext.Order.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order> DeleteOrderAsync(int id)
        {
            var order = await _dbContext.Order.FirstOrDefaultAsync(x => x.Id == id);
            if (order != null)
            {
                _dbContext.Order.Remove(order);
                await _dbContext.SaveChangesAsync();
                return order;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Order>> GetOrderByUserIdAsync(string userId)
        {
            var result = await _dbContext.Order.Include(x =>x.Rooms).Where(x => x.UserId == userId).ToListAsync();
            return result;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _dbContext.Order.ToListAsync();
        }

        public async Task<decimal> GetOrderTotal(string id)
        {
            decimal total = 0;
            var orderUser = await _dbContext.Order.Include(x => x.Rooms).Where(x => x.UserId == id).ToListAsync();
            foreach (var order in orderUser)
            {
                total += order.Rooms.price;
            }
            return total;
        }
    }
}
