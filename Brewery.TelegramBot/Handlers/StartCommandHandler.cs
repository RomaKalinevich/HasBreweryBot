using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBaseContext;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Handlers;

public class StartCommandHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly BreweryDbContext _dbContext;

    public StartCommandHandler(ITelegramBotClient botClient, BreweryDbContext dbContext)
    {
        _botClient = botClient;
        _dbContext = dbContext;
    }

    public async Task HandleAsync(Message message, CancellationToken ct)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.TelegramId == message.From.Id.ToString(), ct);

        if (user == null)
        {
            user = new Domain.Entities.User
            {
                TelegramId = message.From.Id.ToString(),
                FullName = message.From.FirstName,
                Role = Role.Unknown,
                IsActive = false,
                CreatedAt = DateTime.Now.ToUniversalTime(),
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync(ct);

            await _botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Вы зарегистрированы! Ожидайте подтверждения роли от администратора.",
                cancellationToken: ct);
        }
        else
        {
            await _botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: $"С возвращением, {user.FullName}! Твоя роль {user.Role.ToString()}",
                cancellationToken: ct);
        }
    }
}