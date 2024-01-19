using HotelWebApp.ViewModel;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace HotelWebApp.HelperAPI
{
    public static class OrderAPI
    {
        public async static Task<List<OrderVM>> GetUserOrderAPIAsync(HttpClient httpClient, string endpoint, string user_id)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, httpClient.BaseAddress + endpoint + $"/{user_id}");
            var response = await httpClient.SendAsync(httpRequest);
            if (response.IsSuccessStatusCode)
            {
                var responceContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<OrderVM>>(responceContent);
            }
            else
            {
                return null;
            }
        }
        public async static Task<HttpStatusCode> AddOrderAPIAsync(HttpClient httpClient, string endpoint, OrderVM orderVM)
        {
            var json = JsonConvert.SerializeObject(orderVM);
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
        public async static Task<HttpStatusCode> DeleteOrderAsync(HttpClient httpClient, string endpoint)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, httpClient.BaseAddress + endpoint);
            var response = await httpClient.SendAsync(httpRequest);
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