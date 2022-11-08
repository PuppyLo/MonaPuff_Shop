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
    string firstName = message.From.FirstName;

    ReplyKeyboardMarkup keyboard = new(new[]
    {
            new KeyboardButton[] {"Электронки", "Жидкости"},
            new KeyboardButton[] {"Кальянный табак", "Стики/Сигареты"},
            new KeyboardButton[] {"POD/Устройства", "Аксессуары"}
    })
    {
        ResizeKeyboard = true
    };

    InlineKeyboardMarkup inlineKeyboard_Vape = new(new[]
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

    InlineKeyboardMarkup inlineKeyboard_Zhidkosti = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Brusko", "brusko"),
                InlineKeyboardButton.WithCallbackData("Boshki", "boshki"),
                InlineKeyboardButton.WithCallbackData("Мишки", "мишки"),

            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("HotSpot", "hotspot"),
                InlineKeyboardButton.WithCallbackData("Husky", "husky"),
                InlineKeyboardButton.WithCallbackData("Maxwell's", "maxwells"),

            },
                });

    Message msg = new();
    //Message msg = await botClient.SendTextMessageAsync(message.Chat.Id, "Дообро пожаловать в магазин MonaPuff", replyMarkup: keyboard);

    switch (message.Text)
    {
        case "/start":
            {
                msg = await botClient.SendTextMessageAsync(message.Chat.Id, $"{firstName}, выберите какой-то пункт", replyMarkup: keyboard);
                break;
            }
        case "Электронки":
            {
                msg = await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали - Электронки", replyMarkup: keyboard);
                msg = await botClient.SendTextMessageAsync(message.Chat.Id, "Выбирайте ^_^", replyMarkup: inlineKeyboard_Vape);

                break;
            }
        case "Жидкости":
            {
                msg = await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали - Жидкости", replyMarkup: keyboard);
                msg = await botClient.SendTextMessageAsync(message.Chat.Id, "Выбирайте ^_^", replyMarkup: inlineKeyboard_Zhidkosti);

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
    InlineKeyboardMarkup inlineKeyboard_Zakaz = new(new[]
    {
         new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c Армянином", @"https://t.me/vova534"),
                InlineKeyboardButton.WithUrl("Связаться c Азером", @"https://t.me/loaffer"),
}
    });

    InlineKeyboardMarkup inlineKeyboard_Vape = new(new[]
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

    InlineKeyboardMarkup inlineKeyboard_HQD = new(new[]
               {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("CUVIE", "cuvie"),
                InlineKeyboardButton.WithCallbackData("CUVIE PLUS", "cuvie plus"),
                InlineKeyboardButton.WithCallbackData("HIT", "hit"),
                InlineKeyboardButton.WithCallbackData("MEGA", "mega"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("KING", "king"),
                InlineKeyboardButton.WithCallbackData("MAXX", "maxx"),
                InlineKeyboardButton.WithCallbackData("CUVIE AIR", "cuvie air"),
                InlineKeyboardButton.WithCallbackData("HOT", "hot"),
            },
            new[]
            {                
                InlineKeyboardButton.WithCallbackData("Назад", "main menu")
            },
                });

    InlineKeyboardMarkup inlineKeyboard_ElfBar = new(new[]
    {
        new[]
            {
                InlineKeyboardButton.WithCallbackData("1500", "elf_1500"),
                InlineKeyboardButton.WithCallbackData("2000", "elf_2000"),
                InlineKeyboardButton.WithCallbackData("3000", "elf_3000"),
                InlineKeyboardButton.WithCallbackData("4000", "elf_4000"),
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "main menu")
            },
    });


    if (callbackQuery.Data == "main menu")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выбирайте ^_^?",
            replyMarkup: inlineKeyboard_Vape);
        return;
    }

    if (callbackQuery.Data == "hqd")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Вы хотите купить HQD?",
            replyMarkup: inlineKeyboard_HQD);
        return;
    }

    if (callbackQuery.Data == "elf bar")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Вы хотите купить Elf Bar?",
            replyMarkup: inlineKeyboard_ElfBar);
        return;
    }



        await botClient.SendTextMessageAsync(
        callbackQuery.Message.Chat.Id,
        $"Для заказа напишите нам:",
        replyMarkup: inlineKeyboard_Zakaz);
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
