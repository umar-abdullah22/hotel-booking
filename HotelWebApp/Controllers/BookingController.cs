using HotelWebApp.HelperAPI;
using HotelWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HotelWebApp.Controllers
{
    public class BookingController : Controller
    {
        private readonly HttpClient _httpClient;
        List<OrderVM> resultOrderAPI;
        public BookingController(IHttpClientFactory httpClientFactory)
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

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBooking(BookingVM bookingVM)
        {
            if (ModelState.IsValid)
            {
                #region Get Select Option Values
                string selectedValueAdult = Request.Form["AdultsSelect"];
                string selectedValueChildren = Request.Form["ChildrenSelect"];
                int selectedAdult = Convert.ToInt32(selectedValueAdult);
                int selectedChildren = Convert.ToInt32(selectedValueChildren);
                bookingVM.Adults = selectedAdult;
                bookingVM.Children = selectedChildren;
                #endregion

                resultOrderAPI = await OrderAPI.GetUserOrderAPIAsync(_httpClient, "Order/GetOrderByUserId", TokenSerializer.ClaimUserId);
                var PersonCount = resultOrderAPI.Sum(x => x.Rooms.person);  // sum person of reserved rooms
                if (bookingVM.Adults < PersonCount) // if adult option less than person of rooms
                {
                    foreach (var item in resultOrderAPI)  // foreach to add bookin order by order
                    {
                        bookingVM.RoomId = item.RoomId;
                        bookingVM.UserId = item.UserId;
                        var resultAPI = await BookingAPI.AddBookingAPIAsync(_httpClient, "Booking/AddBook", bookingVM);
                        if (resultAPI == System.Net.HttpStatusCode.OK)
                        {
                            TempData["book_added"] = "booking Added Sucessfully";
                            await OrderAPI.DeleteOrderAsync(_httpClient, $"Order/RemoveOrder/{item.Id}");
                        }
                        else
                        {
                            return NotFound("Please Try Agian");
                        }
                    }
                }
                else
                {
                    TempData["errBooking"] = $"Please Enter Adults less than Rooms Person  Number of people in the rooms Order : {PersonCount} Person";
                    return RedirectToAction("Index", "Booking");
                }
            }
            return RedirectToAction("Index", "Booking");
        }
    }
}
