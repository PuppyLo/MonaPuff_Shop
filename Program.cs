using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

var botClient = new TelegramBotClient("5706550033:AAG-qSTqviUuQbQKZAyl6cw2rUOYeLzi5BE");

using var cts = new CancellationTokenSource();

var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { }
};

botClient.StartReceiving(
    HandleUpdatesAsync,
    HandleErrorAsync,
    receiverOptions,
    cancellationToken: cts.Token);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Начал прослушку @{me.Username}");
Console.ReadLine();

cts.Cancel();

async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Type == UpdateType.Message && update?.Message?.Text != null)
    {
        await HandleMessage(botClient, update.Message);
        return;
    }

    if (update.Type == UpdateType.CallbackQuery)
    {
        await HandleCallbackQuery(botClient, update.CallbackQuery);
        return;
    }
}

async Task HandleMessage(ITelegramBotClient botClient, Message message)
{
    ReplyKeyboardMarkup keyboard = new(new[]
    {
            new KeyboardButton[] {"Электронки", "Жидкости"},
            new KeyboardButton[] {"Кальянный табак", "Стики/Сигареты"},
            new KeyboardButton[] {"POD/Устройства", "Аксессуары"}
    })
    {
        ResizeKeyboard = true
    };

    string firstName = message.From.FirstName;


    switch (message.Text)
    {
        case "/start":
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Дообро пожаловать в магазин MonaPuff, {firstName}, выберите какой то пункт", replyMarkup: keyboard);
                break;
            }
        case "Электронки":
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали - Электронки", replyMarkup: keyboard);

                InlineKeyboardMarkup inlineKeyboard = new(new[]
                {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("HQD", "hqd"),
                InlineKeyboardButton.WithCallbackData("ELF BAR", "elf bar"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("PUFFMI", "puffmi"),
                InlineKeyboardButton.WithCallbackData("UDN", "UDN"),
            },
                });

                await botClient.SendTextMessageAsync(message.Chat.Id, "Выбирайте ^_^", replyMarkup: inlineKeyboard);

                break;
            }
        case "Жидкости":
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали - Жидкости", replyMarkup: keyboard);
                break;
            }
        case "Кальянный табак":
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали - Кальянный табак", replyMarkup: keyboard);
                break;
            }
        case "Стики/Сигареты":
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали - Стики/Сигареты", replyMarkup: keyboard);
                break;
            }
        case "POD/Устройства":
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали - POD/Устройства", replyMarkup: keyboard);
                break;
            }
        case "Аксессуары":
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали - Аксессуары", replyMarkup: keyboard);
                break;
            }
        default: break;
    }

    //await botClient.SendTextMessageAsync(message.Chat.Id, $"Вы выбрали:\n{message.Text}");

    return;
}

async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
{
    if (callbackQuery.Data == "hqd")
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"Вы хотите купить? hqd"
        );
        return;
    }
    if (callbackQuery.Data == "elf bar")
    {
        await botClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            $"Вы хотите продать? elf bar"
        );
        return;
    }

        await botClient.SendTextMessageAsync(
        callbackQuery.Message.Chat.Id,
        $"Это еще не работает: {callbackQuery.Data}"
        );
    return;
}

Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Ошибка телеграм АПИ:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
        _ => exception.ToString()
    };
    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}
