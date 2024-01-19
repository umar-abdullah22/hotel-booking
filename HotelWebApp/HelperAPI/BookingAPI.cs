using HotelWebApp.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace HotelWebApp.HelperAPI
{
    public static class BookingAPI
    {
        public async static Task<List<BookingVM>> GetBookingAsync(HttpClient httpClient, string endpoint)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress + endpoint );
            var response = await httpClient.SendAsync(httpRequest);
            if (response.IsSuccessStatusCode)
            {
                var responceContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<BookingVM>>(responceContent);
            }
            else
            {
                return null;
            }
        }
        public async static Task<HttpStatusCode> AddBookingAPIAsync(HttpClient httpClient, string endpoint, BookingVM bookingVM)
        {
            var json = JsonConvert.SerializeObject(bookingVM);
            // cast string json to HttpContent
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(httpClient.BaseAddress + endpoint, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                return response.StatusCode;
            }
            else
            {
                return response.StatusCode;
            }
        }
    }
}
