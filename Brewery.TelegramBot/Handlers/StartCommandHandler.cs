using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBaseContext;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Handlers;

public class StartCommandHandler
{
	private readonly ITelegramBotClient _botClient;
	private readonly BreweryDbContext _dbContext;

	private readonly string defaultUrl = "https://tsunami-frequencies-precise-respectively.trycloudflare.com";

	public StartCommandHandler(ITelegramBotClient botClient, BreweryDbContext dbContext)
	{
		_botClient = botClient;
		_dbContext = dbContext;
	}

	public async Task HandleAsync(Message message, CancellationToken ct)
	{
		try
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
				var keyboard = new InlineKeyboardMarkup(new[]
				{
					new[]
					{
						InlineKeyboardButton.WithWebApp(
							"Управление организациями",
							new WebAppInfo
							{
								Url = "https://make-constructed-drops-breeds.trycloudflare.com/organizations"
							})
					}
				});

				await _botClient.SendTextMessageAsync(
					chatId: message.Chat.Id,
					text: $"С возвращением, {user.FullName}!",
					replyMarkup: keyboard,
					cancellationToken: ct);
			}
		}
		catch (Exception ex)
		{
			await _botClient.SendTextMessageAsync(
				chatId: message.Chat.Id,
				text: $"Произошла какая-то ошибка! {ex}",
				cancellationToken: ct);
		}
	}
}