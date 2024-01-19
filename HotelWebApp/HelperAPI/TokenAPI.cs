using HotelWebApp.ViewModel;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelWebApp.HelperAPI
{
    public static class TokenAPI
    {
        public enum TokenUserType
        {
            Name,
            userId
        }
        public static async Task<string> GetJwtTokenAsync(HttpClient _httpClient, LoginVM model, string endpoint)
        {
            string? responseTokenContent = string.Empty;

            //Serialize Login Model to json
            var json = JsonConvert.SerializeObject(model);
            // cast string json to HttpContent
            var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
            // Set the request content with the login credentials
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress + endpoint);
            httpRequest.Content = jsonContent;
            var response = await _httpClient.SendAsync(httpRequest);
            if (response.IsSuccessStatusCode)  // ok 200
            {
                var responceContent = await response.Content.ReadAsStringAsync();
                responseTokenContent = JsonConvert.DeserializeObject<TokenSerializer>(responceContent).Token;
                TokenSerializer.TokenSaved = responseTokenContent;  // save token in property static to access it from anywhere
                TokenSerializer.ClaimName = DeserializeTokenUser(responseTokenContent,TokenUserType.Name); // assign desrilized ClaimName Token to ClaimName property
                TokenSerializer.ClaimUserId = DeserializeTokenUser(responseTokenContent,TokenUserType.userId); // assign desrilized ClaimName Token to userId property
            }

            // Handle error response
            // For example, you can throw an exception or display an error message to the user
            return responseTokenContent;
        }
        public static string DeserializeTokenUser(string token, TokenUserType userType)
        {
            string ClaimsName = string.Empty;
            if (userType == TokenUserType.Name)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                if (jwtToken != null)
                {
                    ClaimsName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value; // get username from token from api
                    if (ClaimsName != null)
                    {
                        return ClaimsName;
                    }
                }
            }
            else if (userType == TokenUserType.userId)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                if (jwtToken != null)
                {
                    ClaimsName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value; // get username from token from api
                    if (ClaimsName != null)
                    {
                        return ClaimsName;
                    }
                }
            }
            return ClaimsName;


        }
    }
}
