using HotelWebApp.HelperAPI;
using HotelWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace HotelWebApp.Controllers
{
    public class CartController : Controller
    {
        private readonly HttpClient _httpClient;
        public CartController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44317/api/");
        }
        public async Task<IActionResult> Index()
        {
            #region this regoin to assign userid, username from cookie stored in web browser
            if (TokenSerializer.ClaimName == null || TokenSerializer.ClaimUserId == null) // intialize if cookie of token user stored and doesn't desrialize
            {
                TokenSerializer.ClaimName = TokenAPI.DeserializeTokenUser(Request.Cookies["jwtTokenUser"], TokenAPI.TokenUserType.Name);
                TokenSerializer.ClaimUserId = TokenAPI.DeserializeTokenUser(Request.Cookies["jwtTokenUser"], TokenAPI.TokenUserType.userId);
            }
            #endregion
            var resultOrderAPI = await OrderAPI.GetUserOrderAPIAsync(_httpClient, "Order/GetOrderByUserId", TokenSerializer.ClaimUserId);
            var resultBookingAPI = await BookingAPI.GetBookingAsync(_httpClient, "Booking/GetAllBooking");

            ViewBag.OrderCount = resultOrderAPI.Count;

            ViewBag.PrevoiusBooking = resultBookingAPI.Where(x => x.UserId == TokenSerializer.ClaimUserId).FirstOrDefault() == null ? ViewBag.PrevoiusBooking = 0 : ViewBag.PrevoiusBooking = 95;
            if (resultOrderAPI.Count == 0)
                return RedirectToAction("Index", "Home");
            else
                return View(resultOrderAPI);
        }
    }
}
