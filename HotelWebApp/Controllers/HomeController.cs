using HotelWebApp.HelperAPI;
using HotelWebApp.Models;
using HotelWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HotelWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44317/api/");
        }
        public async Task<IActionResult> Index()
        {
            if (Request.Cookies["jwtTokenUser"] == null) // if user not authenticated (token = null)
            {
                return RedirectToAction("Login", "Account");
            }
            #region this regoin to assign userid, username from cookie stored in web browser
            if (TokenSerializer.ClaimName == null || TokenSerializer.ClaimUserId == null) // intialize if cookie of token user stored and doesn't desrialize
            {
                TokenSerializer.ClaimName = TokenAPI.DeserializeTokenUser(Request.Cookies["jwtTokenUser"], TokenAPI.TokenUserType.Name);
                TokenSerializer.ClaimUserId = TokenAPI.DeserializeTokenUser(Request.Cookies["jwtTokenUser"], TokenAPI.TokenUserType.userId);
            }
            #endregion

            var resultRoomAPI = await RoomAPI.GetRoomsAPIAsync(_httpClient, "Rooms/GetRooms");
            var resultOrderAPI = await OrderAPI.GetUserOrderAPIAsync(_httpClient, "Order/GetOrderByUserId", TokenSerializer.ClaimUserId);
            var resultBookingAPI = await BookingAPI.GetBookingAsync(_httpClient, "Booking/GetAllBooking");
            RoomOrderVM roomOrder = new()
            {
                RoomsVM = resultRoomAPI,
                OrderVM = resultOrderAPI
            };
            ViewBag.OrderCount = resultOrderAPI.Count;
            ViewBag.BookingBag = resultBookingAPI;
            return View(roomOrder);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderVM orderVM)
        {
            OrderVM order = new()
            {
                orderCreation = DateTime.Now,
                RoomId = orderVM.RoomId,
                UserId = TokenSerializer.ClaimUserId
            };
            var statusOrderResult = await OrderAPI.AddOrderAPIAsync(_httpClient, "Order/AddOrder", order);
            if(statusOrderResult == System.Net.HttpStatusCode.OK)
            {
                TempData["order_added"] = "Order Added Sucessfully";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return NotFound("Please Try Agian");
            }
        }



    }
}