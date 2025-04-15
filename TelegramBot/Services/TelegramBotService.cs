using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Services;

public class TelegramBotService : BackgroundService
{
	private readonly ITelegramBotClient _botClient;

    public TelegramBotService(IConfiguration configuration)
    {
        var token = configuration["TelegramBot:Token"];
        _botClient = new TelegramBotClient(token!);
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

        if (message.Text is "/start")
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Привет! Выберите свою роль: /admin /brewer /driver");
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken ct)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException =>
                $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}