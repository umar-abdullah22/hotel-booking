using HotelAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoom _room;
        public RoomsController(IRoom room)
        {
            this._room = room;
        }
        [HttpGet("GetRooms")]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _room.GetRooms();
            return Ok(rooms);
        }
    }
}
