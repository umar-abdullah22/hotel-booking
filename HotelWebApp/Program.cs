using HotelWebApp.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HotelWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpClient();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //builder.Services.AddAuthentication(o =>
            //{
            //    o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //}
            // ).AddCookie(option =>
            //{
            //   option.Cookie.HttpOnly = false;
            //   option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            //   option.LogoutPath = "/api/Account/Logout";
            //   option.SlidingExpiration = true;
            //   option.Cookie.SameSite = SameSiteMode.Lax;
            //   option.Cookie.IsEssential = true;
            // });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //app.UseCookiePolicy();
            //app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}