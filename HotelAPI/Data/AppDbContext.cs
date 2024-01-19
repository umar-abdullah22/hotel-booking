using Hotel.Data;
using HotelAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string> // use this classes ti inherit from user and role to implement in DB
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Rooms> Room { get; set; }
        public DbSet<Booking> booking { get; set; }
        public DbSet<Order> Order { get; set; }

    }
}
