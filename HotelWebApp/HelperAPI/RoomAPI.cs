using HotelWebApp.ViewModel;
using Newtonsoft.Json;
using System.Net.Http;

namespace HotelWebApp.HelperAPI
{
    public static class RoomAPI
    {
        public async static Task<List<RoomsVM>> GetRoomsAPIAsync(HttpClient httpClient, string endpoint)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress + endpoint);
            var response = await httpClient.SendAsync(httpRequest);
            if (response.IsSuccessStatusCode)
            {
                var responceContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RoomsVM>>(responceContent);
            }
            else
            {
                return null;
            }
        }
    }
}
