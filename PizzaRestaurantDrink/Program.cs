using Microsoft.EntityFrameworkCore;
using Dblayer.Models;
using PizzaRestaurantDrink.HelperClass; // NECESSARY: Import the helpers namespace

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database Connection
builder.Services.AddDbContext<ProPizzResturentandDrinkDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Session Configuration
builder.Services.AddSession();

// --- NEW CODE: Register the Helpers for Dependency Injection ---
builder.Services.AddScoped<IEmailHelper, EmailHelper>();
builder.Services.AddScoped<IFileHelper, FileHelper>();
// -------------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

// Enable Session before Authorization
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();