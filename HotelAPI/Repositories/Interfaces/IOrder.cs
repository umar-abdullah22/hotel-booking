using HotelAPI.Models;

namespace HotelAPI.Repositories.Interfaces
{
    public interface IOrder
    {
        public Task<List<Order>> GetOrdersAsync();
        public Task<List<Order>> GetOrderByUserIdAsync(string userId);
        public Task<Order> AddOrderAsync(Order order);
        public Task<Order> DeleteOrderAsync(int id);
        public Task<decimal> GetOrderTotal(string id);
    }
}
