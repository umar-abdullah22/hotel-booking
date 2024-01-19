using HotelAPI.DTO;
using HotelAPI.Models;
using HotelAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBooking _Booking;
        public BookingController(IBooking _Booking)
        {
            this._Booking = _Booking;
        }
        [HttpGet("GetAllBooking")]
        public async Task<IActionResult> GetAllBooking()
        {
            var booking = await _Booking.GetBookingsAsync();
            if (booking != null)
            {
                return Ok(booking);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("GetBookingByRoom/{roomId:int}")]
        public async Task<IActionResult> GetBookingByRoom(int roomId)
        {
            return Ok(await _Booking.GetBookingsByRoomIdAsync(roomId));
        }
        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook(BookingDTO bookingDTO)
        {
            if (bookingDTO != null)
            {
                Booking booking = new()
                {
                    FullName = bookingDTO.FullName,
                    Adults = bookingDTO.Adults,
                    CheckIn = bookingDTO.CheckIn,
                    CheckOut = bookingDTO.CheckOut,
                    Children = bookingDTO.Children,
                    PhoneNumber = bookingDTO.PhoneNumber,
                    Time = bookingDTO.Time,
                    RoomId = bookingDTO.RoomId,
                    UserId = bookingDTO.UserId,
                };
                var result = await _Booking.AddBookingAsync(booking);
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
