using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.DataBaseContext;
using Telegram.Bot;
using TelegramBot.Handlers;
using TelegramBot.Services;

var builder = Host.CreateDefaultBuilder(args)
	.ConfigureAppConfiguration(config => { config.AddJsonFile("appsettings.json", optional: false); })
	.ConfigureServices((context, services) =>
	{
		services.AddDbContext<BreweryDbContext>(options =>
			options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));

		services.AddTransient<StartCommandHandler>();
		services.AddSingleton<ITelegramBotClient>(provider =>
		{
			var configuration = provider.GetRequiredService<IConfiguration>();
			var token = configuration["Brewery.TelegramBot:Token"];
			return new TelegramBotClient(token);
		});

		services.AddHostedService<TelegramBotService>();
	});
await builder.RunConsoleAsync();