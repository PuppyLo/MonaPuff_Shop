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
            new KeyboardButton[] {"Меню", "Контакты"},
            new KeyboardButton[] {"Наш Instagram", "Наш Telegram"},
            new KeyboardButton[] {"Наши магазины"}
    })
    {
        ResizeKeyboard = true
    };

    InlineKeyboardMarkup inlineKeyboard_Kontakty = new(new[]
    {
         new[]
         {
                InlineKeyboardButton.WithUrl("Связаться c Армянином", @"https://t.me/vova534"),
                InlineKeyboardButton.WithUrl("Связаться c Азером", @"https://t.me/loaffer"),
         }
    });

    InlineKeyboardMarkup inlineKeyboard_Menu = new(new[]
    {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Электронки", "электронки"),
                InlineKeyboardButton.WithCallbackData("Жидкости", "жидкости"),
                InlineKeyboardButton.WithCallbackData("Кальянный табак", "кальянный табак")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Стики/Сигареты", "стики/сигареты"),
                InlineKeyboardButton.WithCallbackData("POD/Устройства", "POD/Устройства"),
                InlineKeyboardButton.WithCallbackData("Аксессуары", "аксессуары")
            },
                });

    Message msg = new();

    //Message msg = await botClient.SendTextMessageAsync(message.Chat.Id, "Дообро пожаловать в магазин MonaPuff", replyMarkup: keyboard);

    switch (message.Text)
    {
        case "/start":
            {
                msg = await botClient.SendTextMessageAsync(message.Chat.Id, $"{firstName}, Добро пожаловать в MonaPuff, выберите какой-то пункт 🔽", replyMarkup: keyboard);
                break;
            }
        case "Меню":
            {
                //msg = await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали - Электронки", replyMarkup: keyboard);
                msg = await botClient.SendTextMessageAsync(message.Chat.Id, "Выбирайте ^_^", replyMarkup: inlineKeyboard_Menu);

                break;
            }
        case "Контакты":
            {
                //msg = await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали - Жидкости", replyMarkup: keyboard);
                msg = await botClient.SendTextMessageAsync(message.Chat.Id, "Связь с чёрными ", replyMarkup: inlineKeyboard_Kontakty);

                break;
            }
        case "Наш Instagram":
            {
                var hyperLinkKeyboard = new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl("MonaPuff", "https://www.instagram.com/_monapuff_/"));
                await botClient.SendTextMessageAsync(message.Chat.Id, "Наш Instagram:", replyMarkup: hyperLinkKeyboard);
                break;
            }
        case "Наш Telegram":
            {
                var hyperLinkKeyboard = new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl("MonaPuff", "https://t.me/monatobacco"));
                await botClient.SendTextMessageAsync(message.Chat.Id, "Наш Telegram канал:", replyMarkup: hyperLinkKeyboard);
                break;
            }
        case "Наши магазины":
            {
                var hyperLinkKeyboard = new InlineKeyboardMarkup(new[] {new[]{
                    InlineKeyboardButton.WithUrl("MonaPuff на Серпуховской", "https://yandex.ru/profile/-/CCUbMIBUWC"),
                    InlineKeyboardButton.WithUrl("MonaPuff на Южной ", "https://yandex.ru/maps/-/CCUbMMEkPD")    }

                });

                //await botClient.SendLocationAsync(message.Chat.Id, 55.727353, 37.626490);
                await botClient.SendTextMessageAsync(message.Chat.Id, "Наши магазины на яндекс картах:", replyMarkup: hyperLinkKeyboard);
                break;
            }
        default: break;
    }
    return;
}

async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
{
    InlineKeyboardMarkup inlineKeyboard_Kontakty = new(new[]
    {
         new[]
         {
                InlineKeyboardButton.WithUrl("Связаться c Армянином", @"https://t.me/vova534"),
                InlineKeyboardButton.WithUrl("Связаться c Азером", @"https://t.me/loaffer"),
         }
    });

    InlineKeyboardMarkup inlineKeyboard_Menu = new(new[]
{
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Электронки", "электронки"),
                InlineKeyboardButton.WithCallbackData("Жидкости", "жидкости"),
                InlineKeyboardButton.WithCallbackData("Кальянный табак", "кальянный табак")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Стики/Сигареты", "стики/сигареты"),
                InlineKeyboardButton.WithCallbackData("POD/Устройства", "POD/Устройства"),
                InlineKeyboardButton.WithCallbackData("Аксессуары/Запчасти к POD","аксессуары")
            },
                });

    if (callbackQuery.Data == "main menu")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выбирайте ^_^?",
            replyMarkup: inlineKeyboard_Menu);
        return;
    }

    #region Электронки
    #region Команды
    InlineKeyboardMarkup inlineKeyboard_Vape = new(new[]
    {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("HQD", "hqd"),
                InlineKeyboardButton.WithCallbackData("ELF BAR", "elf bar"),
                InlineKeyboardButton.WithCallbackData("LOST MARY", "lost mary"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("PUFFMI", "puffmi"),
                InlineKeyboardButton.WithCallbackData("UDN", "udn"),
                InlineKeyboardButton.WithCallbackData("ENERGY", "energy"),
                InlineKeyboardButton.WithCallbackData("LISSANELLI", "lissanelli"),

            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "main menu")
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
                InlineKeyboardButton.WithCallbackData("Назад", "электронки")
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
                InlineKeyboardButton.WithCallbackData("Назад", "электронки")
            },
    });

    InlineKeyboardMarkup inlineKeyboard_LostMary = new(new[]
{
        new[]
            {
                InlineKeyboardButton.WithCallbackData("1500", "elf_1500"),
                InlineKeyboardButton.WithCallbackData("4000", "elf_2000"),
                InlineKeyboardButton.WithCallbackData("5000", "elf_3000"),
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "электронки")
            },
    });

    InlineKeyboardMarkup inlineKeyboard_Puffmi = new(new[]
{
        new[]
            {
                InlineKeyboardButton.WithCallbackData("1500", "elf_1500"),
                InlineKeyboardButton.WithCallbackData("3500", "elf_2000"),
                InlineKeyboardButton.WithCallbackData("4500", "elf_3000"),
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "электронки")
            },
    });

    InlineKeyboardMarkup inlineKeyboard_Udn = new(new[]
    {
        new[]
            {
                InlineKeyboardButton.WithCallbackData("4200", "elf_1500"),
                InlineKeyboardButton.WithCallbackData("4500", "elf_2000"),
                InlineKeyboardButton.WithCallbackData("4800", "elf_3000"),
                InlineKeyboardButton.WithCallbackData("5200", "elf_3000"),
                InlineKeyboardButton.WithCallbackData("6000", "elf_3000"),
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "электронки")
            },
    });

    InlineKeyboardMarkup inlineKeyboard_Energy = new(new[]
{
        new[]
            {
                InlineKeyboardButton.WithCallbackData("5000", "elf_1500")
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "электронки")
            },
    });

    InlineKeyboardMarkup inlineKeyboard_Lissanelli = new(new[]
{
        new[]
            {
                InlineKeyboardButton.WithCallbackData("1000", "elf_1500")
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "электронки")
            },
    });
    #endregion

    if (callbackQuery.Data == "электронки")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите производителя?",
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
            $"Вы хотите купить ELF BAR?",
            replyMarkup: inlineKeyboard_ElfBar);
        return;
    }
    if (callbackQuery.Data == "lost mary")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Вы хотите купить LOST MARY?",
            replyMarkup: inlineKeyboard_LostMary);
        return;
    }
    if (callbackQuery.Data == "puffmi")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Вы хотите купить PUFFMI?",
            replyMarkup: inlineKeyboard_Puffmi);
        return;
    }
    if (callbackQuery.Data == "udn")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Вы хотите купить UDN?",
            replyMarkup: inlineKeyboard_Udn);
        return;
    }
    if (callbackQuery.Data == "energy")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Вы хотите купить ENERGY?",
            replyMarkup: inlineKeyboard_Energy);
        return;
    }
    if (callbackQuery.Data == "lissanelli")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Вы хотите купить LISSANELLI?",
            replyMarkup: inlineKeyboard_Lissanelli);
        return;
    }
    #endregion

    #region Жидкости
    #region Команды
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
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "main menu")
            },
                });
    InlineKeyboardMarkup inlineKeyboard_Brusko = new(new[]
          {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Brusko - 2%", "brusko2"),
                InlineKeyboardButton.WithCallbackData("Brusko - 5%", "brusko5")

            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "жидкости")
            },
                });

    InlineKeyboardMarkup inlineKeyboard_HotSpot = new(new[]
      {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("HotSpot 18 - 2%", "brusko2"),
                InlineKeyboardButton.WithCallbackData("HotSpot 20 - 5%", "brusko5")

            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "жидкости")
            },
                });

    #endregion
    if (callbackQuery.Data == "жидкости")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите производителя?",
            replyMarkup: inlineKeyboard_Zhidkosti);
        return;
    }
    if (callbackQuery.Data == "brusko")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выбирайтел",
            replyMarkup: inlineKeyboard_Brusko);
        return;
    }
    if (callbackQuery.Data == "hotspot")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выбирайте? \n 123 \n 123",
            replyMarkup: inlineKeyboard_HotSpot);
        return;
    }

    #endregion

    #region Стики/Сигареты
    #region Команды
    InlineKeyboardMarkup inlineKeyboard_Stics = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Heets", "heets"),
                InlineKeyboardButton.WithCallbackData("Fiit", "fiit"),

            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "main menu")
            },
                });
    InlineKeyboardMarkup inlineKeyboard_Heets = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Amber", "brusko"),
                InlineKeyboardButton.WithCallbackData("Ruby", "boshki"),

            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "стики/сигареты")
            },
                });
    InlineKeyboardMarkup inlineKeyboard_Fiit = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Regular", "brusko"),
                InlineKeyboardButton.WithCallbackData("Viola", "boshki"),

            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "стики/сигареты")
            },
                });
    #endregion
    if (callbackQuery.Data == "стики/сигареты")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Вы хотите купить Стики?",
            replyMarkup: inlineKeyboard_Stics);
        return;
    }
    if (callbackQuery.Data == "heets")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Вы хотите купить Стики HEETS?",
            replyMarkup: inlineKeyboard_Heets);
        return;
    }
    if (callbackQuery.Data == "fiit")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Вы хотите купить Стики?",
            replyMarkup: inlineKeyboard_Fiit);
        return;
    }
    #endregion

    #region Кальянный табак
    #region Команды
    InlineKeyboardMarkup inlineKeyboard_Tabak = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("DarkSide", "darkside"),
                InlineKeyboardButton.WithCallbackData("Must Have", "must have"),
                InlineKeyboardButton.WithCallbackData("burn", "born"),
                InlineKeyboardButton.WithCallbackData("Daily Huak", "daily huak"),

            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "main menu")
            },
                });

    #endregion
    if (callbackQuery.Data == "кальянный табак")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите производителя?",
            replyMarkup: inlineKeyboard_Tabak);
        return;
    }
    #endregion

    #region POD/Устройства
    #region Команды
    InlineKeyboardMarkup inlineKeyboard_Pod = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Voopoo", "voopoo"),
                InlineKeyboardButton.WithCallbackData("Vaparesso", "vaparesso"),
                InlineKeyboardButton.WithCallbackData("Smok", "smok"),
                InlineKeyboardButton.WithCallbackData("Gekk Vape", "geek vape"),

            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "main menu")
            },
                });

    #endregion
    if (callbackQuery.Data == "POD/Устройства")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите производителя?",
            replyMarkup: inlineKeyboard_Pod);
        return;
    }
    #endregion

    #region Аксессуары
    #region Команды
    InlineKeyboardMarkup inlineKeyboard_Acsessuar = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Картриджи", "картриджи"),
                InlineKeyboardButton.WithCallbackData("Испарители", "vaparesso")
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "main menu")
            },
                });

    #endregion
    if (callbackQuery.Data == "аксессуары")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите интересующую категорию?",
            replyMarkup: inlineKeyboard_Acsessuar);
        return;
    }
    #endregion





    await botClient.SendTextMessageAsync(
        callbackQuery.Message.Chat.Id,
        $"Для уточнения наличия товара и его бронирования свяжитесь с одним из чёрных:",
        replyMarkup: inlineKeyboard_Kontakty);
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

