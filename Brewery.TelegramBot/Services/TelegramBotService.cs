using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Handlers;

namespace TelegramBot.Services;

public class TelegramBotService : BackgroundService
{
	private readonly ITelegramBotClient _botClient;
	private readonly IServiceScopeFactory _scopeFactory;

	public TelegramBotService(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
    {
        _botClient = botClient;
        _scopeFactory = scopeFactory;
    }

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		var receiverOptions = new ReceiverOptions
		{
			AllowedUpdates = Array.Empty<UpdateType>() // Receive all updates
		};

		_botClient.StartReceiving(
			HandleUpdateAsync,
			HandleErrorAsync,
			receiverOptions,
			cancellationToken: stoppingToken
		);

		var me = await _botClient.GetMeAsync();
		Console.WriteLine($"Telegram Bot started: @{me.Username}");
	}

	private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken ct)
	{
		if (update.Message is not { } message)
			return;
		
		using var scope = _scopeFactory.CreateScope();

		if (message.Text == "/start")
		{
			var handler = scope.ServiceProvider.GetRequiredService<StartCommandHandler>();
			await handler.HandleAsync(message, ct);
		}
	}

	private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken ct)
	{
		var errorMessage = exception switch
		{
			ApiRequestException apiRequestException =>
				$"Telegram Brewery.API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
			_ => exception.ToString()
		};

		Console.WriteLine(errorMessage);
		return Task.CompletedTask;
	}
}