using HotelWebApp.HelperAPI;
using HotelWebApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace HotelWebApp.Controllers
{

    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44317/api/Account");
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var token = await TokenAPI.GetJwtTokenAsync(_httpClient, loginVM, "/Login");
                if (!string.IsNullOrEmpty(token))
                {
                    Response.Cookies.Append("jwtTokenUser", token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Username or Password Incorrect");
                }
            }
            return View(loginVM);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            TokenSerializer.TokenSaved = null;
            TokenSerializer.ClaimName = null;
            TokenSerializer.ClaimUserId = null;
            Response.Cookies.Delete("jwtTokenUser"); // delete cookie for user
            return RedirectToAction("Login", "Account");

        }
    }
}
