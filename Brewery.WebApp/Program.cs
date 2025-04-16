using Microsoft.EntityFrameworkCore;
using Persistence.DataBaseContext;

var builder = WebApplication.CreateBuilder(args);

// Конфигурация
builder.Services.AddControllersWithViews();

// Подключение БД
builder.Services.AddDbContext<BreweryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
