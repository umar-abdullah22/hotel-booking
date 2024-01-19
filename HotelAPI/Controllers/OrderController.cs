using HotelAPI.DTO;
using HotelAPI.Models;
using HotelAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder _Order;
        public OrderController(IOrder _Order)
        {
            this._Order = _Order;
        }
        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _Order.GetOrdersAsync();
            return Ok(orders);
        }
        [HttpGet("GetOrderByUserId/{userID}")]
        public async Task<IActionResult> GetOrdersByUserId(string userID) // GetOrderByUserId/3  or GetOrderByUserId?userID=3
        {
            var orders = await _Order.GetOrderByUserIdAsync(userID);
            return Ok(orders);
        }
        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder(OrderDTO orderDTO)
        {
            if (orderDTO != null)
            {
                Order order = new()
                {
                    orderCreation = orderDTO.orderCreation,
                    RoomId = orderDTO.RoomId,
                    UserId = orderDTO.UserId,
                };
                await _Order.AddOrderAsync(order);
                return Ok(order);
            }
            else
            {
                return BadRequest("Order Not Found");
            }
        }
        [HttpDelete("RemoveOrder/{id:int}")]
        public async Task<IActionResult> RemoveOrder(int id)
        {
            var result = await _Order.DeleteOrderAsync(id);
            if (result != null)
                return Ok(result);
            else
                return NotFound("Order Not Found");
        }
    }
}
