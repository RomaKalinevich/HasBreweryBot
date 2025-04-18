using Microsoft.EntityFrameworkCore;
using Persistence.DataBaseContext;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<BreweryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowWebApp", policy =>
	{
		policy.WithOrigins(
				"https://make-constructed-drops-breeds.trycloudflare.com",
				"http://localhost:5173"
			)
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Контроллеры (если используешь API controllers)
builder.Services.AddControllers();

// Создание приложения
var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowWebApp");

app.UseAuthorization();

// Маршрутизация контроллеров
app.MapControllers();

app.Run();
