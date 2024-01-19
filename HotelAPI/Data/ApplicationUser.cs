using Microsoft.AspNetCore.Identity;

namespace Hotel.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string Country { get; set; } = "Egypt";
    }
}
