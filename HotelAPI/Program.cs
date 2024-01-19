using Hotel.Data;
using HotelAPI.Data;
using HotelAPI.Models;
using HotelAPI.Repositories;
using HotelAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
#region Swagger Configuration
builder.Services.AddSwaggerGen(swagger =>
{
    // To Enable authorization using Swagger (JWT)    
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}
                    }
                });
});
#endregion
builder.Services.AddDbContext<AppDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDefault")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
}).AddEntityFrameworkStores<AppDbContext>();



#region [Authorize] use JWT token in check Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;   // use JWT Token instead of cookies
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  // if token not valid then redirect to login form
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; // anything use Scheme make it jwt Scheme
}).AddJwtBearer(options =>    // use this method to check in domain not any valid token
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = true;  // makesure token come from https protocol
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
    };
});
#endregion
builder.Services.AddScoped<IAccount, AccountRepo>();
builder.Services.AddScoped<IRoom, RoomRepo>();
builder.Services.AddScoped<IOrder, OrderRepo>();
builder.Services.AddScoped<IBooking, BookingRepo>();

builder.Services.AddCors(CoresService =>
{
    CoresService.AddPolicy("MyPolicy", coresPolicy =>
    {
        coresPolicy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();  // like html page , images
app.UseHttpsRedirection();
app.UseCors("MyPolicy");   // Cores happen in 2 cases  => 1- diffrenece protocol , 2 - diffrenece domains {configure Cores in services}

app.UseAuthentication(); // by default this use cookie,use this to customize Authentication by Token in Configruation service
app.UseAuthorization();

app.MapControllers();

app.Run();
