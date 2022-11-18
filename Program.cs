using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;

var botClient = new TelegramBotClient("5528348013:AAG6xMA0mrnL3QOUrFUcSYqCcs65QgEGqsY");

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

    ReplyKeyboardMarkup keyboard = new(
        new[]
        {
            new KeyboardButton[] {"Меню", "Контакты"},
           // new KeyboardButton[] {"Отзывы"}
        })
    {
        ResizeKeyboard = true
    };

    InlineKeyboardMarkup inlineKeyboard_Kontakty = new(new[]
    {
         new[]
         {
                InlineKeyboardButton.WithUrl("Связаться c Владимиром", @"https://t.me/vova534"),
         }
    });

    InlineKeyboardMarkup inlineKeyboard_Menu = new(new[]
    {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Электронки", "электронки"),
                InlineKeyboardButton.WithCallbackData("Жидкости", "жидкости")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("POD/Устройства", "POD/Устройства"),
                InlineKeyboardButton.WithCallbackData("Аксессуары", "аксессуары")
            }
                });

    switch (message.Text)
    {
        case "/start":
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"{firstName}, Добро пожаловать в MonaPuf! Выберите, что вас интересует 🔽", replyMarkup: keyboard);
                Console.WriteLine(message.From.FirstName + " - " + message.Text + " - " + message.Chat.FirstName + " - " + message.From.LastName + " - " + message.Chat.Username);
                break;
            }
        case "Меню":
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Выбирайте ^_^", replyMarkup: inlineKeyboard_Menu);
                break;
            }
        case "Контакты":
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Связаться с менеджером/совершить заказ: ", replyMarkup: inlineKeyboard_Kontakty);
                break;
            }

        default: break;
    }
    return;
}

async Task HandleCallbackQuery(ITelegramBotClient botClient, CallbackQuery callbackQuery)
{
    Message msg = callbackQuery.Message;

    InlineKeyboardMarkup inlineKeyboard_Kontakty = new(new[]
    {
         new[]
         {
                InlineKeyboardButton.WithUrl("Связаться c Владимиром", @"https://t.me/vova534"),
         }
    });

    InlineKeyboardMarkup inlineKeyboard_Menu = new(new[]
{
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Электронки", "электронки"),
                InlineKeyboardButton.WithCallbackData("Жидкости", "жидкости")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("POD/Устройства", "POD/Устройства"),
                InlineKeyboardButton.WithCallbackData("Аксессуары/Запчасти к POD","аксессуары")
            }
                });

    if (callbackQuery.Data == "main menu")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выбирайте ^_^",
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
                InlineKeyboardButton.WithCallbackData("LOST MARY", "lost mary")


            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("PUFFMI", "puffmi"),
                InlineKeyboardButton.WithCallbackData("iJOY LIO", "lio"),
                InlineKeyboardButton.WithCallbackData("INFLAVE", "inflave")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("UDN", "udn"),
                InlineKeyboardButton.WithCallbackData("ENERGY", "energy"),
                InlineKeyboardButton.WithCallbackData("LISSANELLI", "lissanelli")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "main menu")
            }
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
            }
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
            }
    });
    InlineKeyboardMarkup inlineKeyboard_LostMary = new(new[]
{
        new[]
            {
                InlineKeyboardButton.WithCallbackData("4000", "lostmary_4000"),
                InlineKeyboardButton.WithCallbackData("5000", "lostmary_5000")
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "электронки")
            }
    });
    InlineKeyboardMarkup inlineKeyboard_Puffmi = new(new[]
{
        new[]
            {
                InlineKeyboardButton.WithCallbackData("1500", "puffmi_1500"),
                InlineKeyboardButton.WithCallbackData("3500", "puffmi_3500"),
                InlineKeyboardButton.WithCallbackData("4500", "puffmi_4500"),
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "электронки")
            }
    });
    InlineKeyboardMarkup inlineKeyboard_Udn = new(new[]
    {
        new[]
            {
                InlineKeyboardButton.WithCallbackData("6000", "udn_6000")
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "электронки")
            }
    });
    InlineKeyboardMarkup inlineKeyboard_Energy = new(new[]
{
        new[]
            {
                InlineKeyboardButton.WithCallbackData("5000", "energy_5000")
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "электронки")
            }
    });
    InlineKeyboardMarkup inlineKeyboard_Lissanelli = new(new[]
{
        new[]
            {
                InlineKeyboardButton.WithCallbackData("1000", "lissanelli_1000")
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "электронки")
            }
    });
    InlineKeyboardMarkup inlineKeyboard_Lio = new(new[]
{
        new[]
            {
                InlineKeyboardButton.WithCallbackData("LIO Boom (3500)", "lio_3500"),
                InlineKeyboardButton.WithCallbackData("LIO COMMA (5000)", "lio_5000")
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "электронки")
            }
    });
    InlineKeyboardMarkup inlineKeyboard_Inflave = new(new[]
{
        new[]
            {
                InlineKeyboardButton.WithCallbackData("INFLAVE PLUS", "inflave_2200"),
                InlineKeyboardButton.WithCallbackData("INFLAVE MAX", "inflave_4000")
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "электронки")
            }
    });


    #endregion

    if (callbackQuery.Data == "электронки")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите бренд:",
            replyMarkup: inlineKeyboard_Vape);
        return;
    }

    #region HQD
    if (callbackQuery.Data == "hqd")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите колличество затяжек:",
            replyMarkup: inlineKeyboard_HQD);
        return;
    }

    InlineKeyboardMarkup inlineKeyboard_HQDBack = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "hqd")
            }
    });


    if (callbackQuery.Data == "cuvie")
    {
        try
        {
            await botClient.EditMessageTextAsync(
                callbackQuery.From.Id.ToString(),
                callbackQuery.Message.MessageId,
                $"Выберите вкус и напишите нам: \n Big Smoke \n Ананас \n Апельсин \n Арбуз \n Банан \n Ванильное мороженое \n Виноград \n Гранатовый сок Смородина и лимон \n Дыня \n Жвачка Мята Арбуз \n Йогурт Лесные ягоды \n Капучино \n Клубника \n Клубника Киви \n Кола \n Лимонный пирог \n Личи \n Малина Лимон \n Манго \n Персик \n Розовый лимонад \n Сибирь,мята,хвоя и лесные ягоды \n Фруктовый микс \n Черника \n Черника Малина Виноград \n Энергетик \n Яблоко \n Яблоко Киви Энергетик\r\n",
                replyMarkup: inlineKeyboard_HQDBack);
            return;
        }
        catch { return; };
    }
    if (callbackQuery.Data == "cuvie plus")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус и напишите нам:\n BigSmoke \n FruitFusion \n Lov66 \n PinkLemon \n PogOrangeGuava \n Ананас \n Арбузнаяжвачка \n Байкал \n Банан \n Ванильноемороженое \n Виноград \n ГранатКиви \n Гранатовыйсоксмородинаилимон \n Дыня \n ДыняТорпедо \n Ежевика \n ЖвачкаМятаАрбуз \n ЙогуртЛесныеягоды \n Кактусовыйлимонад \n Кислыемармеладныечервячки \n Клубника \n КлубникаБанан \n КлубникаКиви \n Клубника-питайя \n Клубничноепеченье \n Клубничныйлимонад \n Клубничныймилкшейк \n Ледянаямята \n ЛедянойПерсик \n Лимонсморскойсолью \n Личи \n МалинаЛимон \n Манго \n МангоПерсик \n Маракуйя \n Мороженое \n Мультифрукт \n ПинаКолада \n Соленаякарамель \n Тархун \n Черника \n ЧерникаМалина \n ЧерникаМалинаВиноград \n ЧерничныйЛимонад \n Черныйчайсосмородиной \n Чистый \n Энергетик \n Яблоко \n ЯблокоПерсик \n ЯблочныйПерсик\r\n",
            replyMarkup: inlineKeyboard_HQDBack);
        return;
    }
    if (callbackQuery.Data == "hit")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: \n Апероль шприц \n Ароматный глинтвейн \n Банановый кекс \n Барбарис \n Блины с медом \n Вафли с кленовым сиропом \n Виноград и Алоэвера \n Вишневый энергетик \n Жвачка \n Карамельный попкорн \n Клубника Маракуйя \n Кола Лимон \n Кола Манго \n Лимонад Кактус-лайм \n Малина и клюква \n Персик Абрикос \n Раф с лесным ягодами \n Тайга (хвоя и смородина) \n Холодный чай с лимоном \n Черничный лимонад \n Черный чай с ягодным вареньем \n Яблоко Манго-груша \n Ягода мушмула \n Ягодный сорбет\r\n",
            replyMarkup: inlineKeyboard_HQDBack);
        return;
    }
    if (callbackQuery.Data == "mega")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус и напишите нам: \n Ананасовый экспресс \n Арбузная жвачка \n Белый Русский \n Ванильное мороженое \n Клубника Арбуз \n Клубника Банан \n Клубника Киви \n Клубничный милкшейк \n Клубничный пончик \n Ледяная мята \n Лимонад Черника-малина \n Малина \n Манго \n Манго Дыня \n Мармелад \n Мармеладные мишки \n Персик \n Пинаколада \n Сочный арбуз \n Сочный виноград \n Черника \n Энергетик \n Яблоко Персик \n Ягодный фреш\r\n",
            replyMarkup: inlineKeyboard_HQDBack);
        return;
    }
    if (callbackQuery.Data == "king")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус и напишите нам: \n Ананас \n Ванильное мороженое \n Виноград \n Гранатовый сок Смородина и лимон \n Жвачка \n Жвачка Мята Арбуз \n Йогурт Лесные ягоды \n Капучино \n Клубника \n Клубника Банан \n Клубника Киви \n Коктейль Белый Русский \n Лайм Кола \n Малина Лимон \n Манго \n Мультифрукт \n Мятная Жвачка \n Персик \n Пинаколада \n Сибирь Мята Хвоя и лесные ягоды \n Туманы майями \n Черника \n Черника малина виноград \n Энергетик\r\n",
            replyMarkup: inlineKeyboard_HQDBack);
        return;
    }
    if (callbackQuery.Data == "maxx")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус и напишите нам: \n Ананас Манго Персик \n Апельсин Манго-гуава \n Барбарис \n Вишневая кола \n Двойное яблоко \n Карамельный табак \n Клубника Виноград \n Коктейль испанская орчата \n Коктейль Карибский дождь \n Коктейль Лонг Айленд \n Коктейль Маргарита \n Кола-ваниль \n Ликер Егерь \n Манго Клубника \n Мохито \n Ореховый батончик \n Персик Манго Арбуз \n Пинаколада \n Попкорн \n Сахарная вата \n Хвоя и лесные ягоды \n Черная смородина \n Черный чай со смородиной \n Энергетик Яблоко-киви \n Яблоко Манго Груша\r\n",
            replyMarkup: inlineKeyboard_HQDBack);
        return;
    }
    if (callbackQuery.Data == "cuvie air")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус и напишите нам: \n Арбуз Лёд \n Ананас \n Барбарис \n Виноград \n Гранатовый сок смородина и лимон \n Ежевика \n Жвачка \n Киви Лимонад \n Клубника Арбуз \n Клубника Киви \n Клубника Кокос \n Клубника манго \n Лайм кола \n Ледяной Банан \n Ледяной Шоколад \n Лесные ягоды \n Личи Айс \n Манго \n Мармеладные Мишки \n Мятная жвачка \n Персик \n Радуга \n Русский крем \n Сибирь \n Черника \n Черника Лимон \n Черника Малина \n Яблоко Груша \n Конфеты\r\n",
            replyMarkup: inlineKeyboard_HQDBack);
        return;
    }
    if (callbackQuery.Data == "hot")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус и напишите нам: \n Ананас \n Арбуз \n Арбуз Банан \n Виноград \n Виноград Алоэ \n Вишневая кола \n Гранатовый сок со смородиной \n Ежевика \n Жвачка \n Йогуртовое Мороженое \n Клубника Банан \n Клубника Киви \n Лайм Малина \n Ледяное клубничное мороженое \n Лимон-Маракуйя \n Малина Лимон \n Манго \n Манго Персик Арбуз \n Персик \n Черная Смородина Мята Алое \n Черника \n Черника Малина Виноград \n Яблоко виноград лед \n Яблоко Персик \n Ягодный мохито \n Черная смородина\r\n",
            replyMarkup: inlineKeyboard_HQDBack);
        return;
    }
    #endregion

    #region ELF BAR
    InlineKeyboardMarkup inlineKeyboard_ElfBarBack = new(new[]
          {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "elf bar")
            }
                });
    if (callbackQuery.Data == "elf bar")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите колличество затяжек:",
            replyMarkup: inlineKeyboard_ElfBar);
        return;
    }
    if (callbackQuery.Data == "elf_1500")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: \n Арбуз \n Ананас Персик Манго \n Банановое молоко \n Банановый лед \n Виноград \n Виноградный энергетик \n Дыня кокос \n Киви Маракуйя Гуава \n Кислое яблоко \n Клубника Банан \n Клубника Виноград \n Клубника Виноград \n Клубничное мороженое \n Клубничный энергетик \n Кокосовая дыня \n Лимонад Голубика Малина \n Манго \n Манго Персик Арбуз \n Персик Манго Гуава \n РедБулл Виноград \n РедБулл Клубника \n Розовый лимонад \n Черника\r\n",
            replyMarkup: inlineKeyboard_ElfBarBack);
    }
    if (callbackQuery.Data == "elf_2000")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: \n Ананас манго апельсин \n Голубика малина лед \n Киви маракуйя гуава \n Киви ягода \n Клубника арбуз \n Кола \n Красный мохито \n Манго Персик Арбуз \n Персик Манго Гуава \n Яблоко Персик",
            replyMarkup: inlineKeyboard_ElfBarBack);
        return;
    }
    if (callbackQuery.Data == "elf_3000")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: \n Ананасовый лед \n Гуавовый лед \n Киви Гуава \n Киви Маракуйя Гуава \n Клубника Ананас Кокос \n Клубника Манго \n Клубничный Киви \n Клубничный лед \n Клюквенный Виноград \n Красный Мохито \n Лимонная мята \n Манго Абрикос Персик \n Манго Персик \n Персик Манго Арбуз \n Сакура Виноград \n Синий Разз Айс \n Тройная дыня \n Энергетик    ",
            replyMarkup: inlineKeyboard_ElfBarBack);
        return;
    }
    if (callbackQuery.Data == "elf_4000")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: Авокадо Крем\n Арбузный лед\n Голубика лед\n Голубика Малина Лед\n Зеленое яблоко\n Киви Маракуйя Гуава\n Клубника Киви\n Клубника Манго\n Клюква Виноград\n Кола\n Красный Мохито\n Лимон Мята\n Манго Персик\n Маракуйя Апельсин Гуава\n Персик Манго Арбуз\n Сакура Виноград\n Синий Разз Айс\n Тройной ягодный лед\n Черничный лед\n Энергетик\n Grape Energy\n Grape HoneyDew\n Juicy Peach\n Mocha Chocolate\n Strawberry grape\n Strawberry Ice\n Strawberry Ice Cream\n Taro Ice Cream\n Vanilla Ice Cream\n\n",
            replyMarkup: inlineKeyboard_ElfBarBack);
        return;
    }
    #endregion

    #region Puffmi
    InlineKeyboardMarkup inlineKeyboard_PuffmiBack = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "puffmi")
            }
    });
    if (callbackQuery.Data == "puffmi")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите колличество затяжек:",
            replyMarkup: inlineKeyboard_Puffmi);
        return;
    }
    if (callbackQuery.Data == "puffmi_1500")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Арбузный лед\n Банановый лед\n Виноградный лед\n Двойное яблоко\n Жвачка\n Клубничная Жвачка\n Клубничное мороженое\n Манговый Лед\n Персиковый лед\n Энергетик\n Ягодный микс\n Ягоды и Арбуз\n Cotton Candy\n Mint\n\n",
            replyMarkup: inlineKeyboard_PuffmiBack);
        return;
    }
    if (callbackQuery.Data == "puffmi_3500")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Алоэ и Виноград\n Ананас Манго\n Арбузный лед\n Кислый Яблочный Лёд\n Клубника и Киви\n Клубничное мороженное\n Клюквенный лимонный лед\n Коктейль\n Ледяная мята\n Манговый лед\n Мята Гуава\n Персик и Лимон\n Персиковый Лед\n Пина колада\n Черника и Малина\n Черничный лед\n\n",
            replyMarkup: inlineKeyboard_PuffmiBack);
        return;
    }
    if (callbackQuery.Data == "puffmi_4500")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Banana Ice\n Blue Razz\n Cola Ice\n Cool Mint\n Energy Drink\n Grape Ice\n Mango Ice\n Pink Lemonade\n Quad Berry\n Strawberry Ice Cream\n Strawberry Kiwi\n Tobacco\n Watermelon Berry\n Watermelone Ice\n\n",
            replyMarkup: inlineKeyboard_PuffmiBack);
        return;
    }
    #endregion

    #region Lost Mary
    InlineKeyboardMarkup inlineKeyboard_LostMaryBack = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "lost mary")
            }
    });
    if (callbackQuery.Data == "lost mary")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите колличество затяжек:",
            replyMarkup: inlineKeyboard_LostMary);
        return;
    }
    if (callbackQuery.Data == "lostmary_4000")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: \nАнанас Манго\n Арбуз\n Виноград\n Голубика Малина Лед\n Киви Маракуйя Гуава\n Клубника Манго\n Клубника Пинаколада\n Клубничный Лед\n Клюквенная Сода\n Мороженое с Клубничным Джемом\n Сочный Персик\n Черничный Лед\n\n",
            replyMarkup: inlineKeyboard_LostMaryBack);
        return;
    }
    if (callbackQuery.Data == "lostmary_5000")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: \nАнанасовый кокосовый лед\n Арбузный лед\n Виноградный яблочный лед\n Вишня Персик Лимонад\n Клубника Черника Вишня\n Клубничное мороженое\n Клюквенная сода\n Красный яблочный лед\n Манго Маракуйя\n Мармеладные мишки\n Сахарная вата\n Смешанные ягоды\n Черника Малина Вишня\n Энергетик\n\n",
            replyMarkup: inlineKeyboard_LostMaryBack);
        return;
    }
    #endregion

    #region UDN
    InlineKeyboardMarkup inlineKeyboard_UDNBack = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "udn")
            }
    });
    if (callbackQuery.Data == "udn")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите колличество затяжек:",
            replyMarkup: inlineKeyboard_Udn);
        return;
    }
    if (callbackQuery.Data == "udn_6000")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Banan Banan Melon\n Blackcurrant Lemon\n Blueberry Watermelon\n Cherry Lemon\n Guava Kiwi Passion Fruit\n Lychee Ice\n Peach Lemon\n Pineapple Coconut\n Pink lemon\n Pomegranate\n Smooth Tobacco\n Steawberry Banana\n Strawberry Lemon\r\n\r\n",
            replyMarkup: inlineKeyboard_UDNBack);
        return;
    }
    #endregion

    #region Energy
    InlineKeyboardMarkup inlineKeyboard_EnergyBack = new(new[]
          {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "energy")
            }
    });

    if (callbackQuery.Data == "energy")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите колличевство затяжек:",
            replyMarkup: inlineKeyboard_Energy);
        return;
    }
    if (callbackQuery.Data == "energy_5000")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Cool Mint\n Energy Drink\n Fruit Fusion\n Grape Ice\n Gummy Bear\n Lush Ice\n Mango Straweberry Ice\n Orange Soda\n Pina Colada Rum\n Red Apple Lemon\n Strawberry Donut\r\n\r\n",
            replyMarkup: inlineKeyboard_EnergyBack);
        return;
    }
    #endregion

    #region Lissanelli
    InlineKeyboardMarkup inlineKeyboard_LissanelliBack = new(new[]
          {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "lissanelli")
            }
    });
    if (callbackQuery.Data == "lissanelli")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите колличество затяжек:",
            replyMarkup: inlineKeyboard_Lissanelli);
        return;
    }
    if (callbackQuery.Data == "lissanelli_1000")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Red Bull\n Ананас\n Виноград\n Гуава Манго\n Зеленый чай\n Капучино\n Клубника Банан\n Клубника со сливками\n Красный апельсин\n Ледяная дыня\n Ледяной арбуз\n Ледяной банан\n Ледяной личи\n Маракуйя\n Пина Колада\n Черная Смородина\r\n\r\n",
            replyMarkup: inlineKeyboard_LissanelliBack);
        return;
    }
    #endregion

    #region Lio
    InlineKeyboardMarkup inlineKeyboard_LioBack = new(new[]
          {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "lio")
            }
    });
    if (callbackQuery.Data == "lio")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите колличество затяжек:",
            replyMarkup: inlineKeyboard_Lio);
        return;
    }
    if (callbackQuery.Data == "lio_3500")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Арбуз со Льдом\n Арбузная жвачка\n Грейпфрут Клубника Банан\n Клубника Арбуз\n Клубника Киви\n Клубника Киви Гранат\n Клубничный Коктейль\n Кока-Кола\n Ледяная Клубника\n Ледяной банан\n Лимонные Конфеты со Льдом\n Личи со Льдом\n Малина Лимон\n Манго Маракуйя\n Манго со Льдом\n Мята\n Персиковый Лимонад\n Пина Колада\n Радужная Сладость\n Фруктовый Пунш\n Черная Ежевика\n Яблочный Сок\n Ягодный Микс\r\n\r\n\r\n",
            replyMarkup: inlineKeyboard_LioBack);
        return;
    }
    if (callbackQuery.Data == "lio_5000")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Банан с Холодком\n Ежевика\n Грейпфрут Банан Киви\n Черника Малина\n Черничный Чизкейк\n Жвачка\n Холодный Кофе\n Энергетик\n Виноград с Холодком\n Лимонные Конфеты с Холодком\n Арбуз с Холодком\n Манго с Холодком\n Апельсин Манго Арбуз\n Персиковый Лимонад\n Фруктовые Конфеты\n Клубничный Коктейль Дайкири\n Клубника с Холодком\n Клубника Киви\n Ледяная Мята\r\n\r\n",
            replyMarkup: inlineKeyboard_LioBack);
        return;
    }
    #endregion

    #region Inflave
    InlineKeyboardMarkup inlineKeyboard_InflaveBack = new(new[]
          {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "inflave")
            }
    });
    if (callbackQuery.Data == "inflave")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите колличество затяжек:",
            replyMarkup: inlineKeyboard_Inflave);
        return;
    }
    if (callbackQuery.Data == "inflave_2200")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Ананас\n Арбуз\n Банан с кокосом\n Виноград\n Вишневая кола\n Вишня лимон персик\n Грейпфрут и ягоды\n Дыня Яблоко Клубника\n Кактус\n Клубника Киви\n Клубничная жвачка\n Клюквенная сода\n Кокосовое Мороженое\n Леденец\n Лимон Киви\n Лимон мята\n Малиновый йогурт\n Манго Персик Ананас\n Мармеладный взрыв\n Мохито\n Орехи и табак\n Персиковый чай\n Спелый Мандарин\n Черная смородина\n Черника\n Эрл Грей\n Ягодный микс\r\n\r\n",
            replyMarkup: inlineKeyboard_InflaveBack);
        return;
    }
    if (callbackQuery.Data == "inflave_4000")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Ананас\n Арбуз\n Вишня Лайм Яблоко\n Гранат Яблоко Черника\n Киви\n Клубника Банан\n Клубничный Милкшейк\n Клюква Виноград\n Клюквенная содовая\n Лайм Мохито\n Малиновый Йогурт\n Манго Дыня Шишка\n Манго Персик Ананас\n Мармеладные мишки\n Миндальное мороженое\n Нежный грейпфрут\n Персиковый Чай\n Розовый мохито\n Свежая Мята\n Секс на пляже\n Фрукт Дракона\n Черная смородина\n Черника Лимон\n Черника Малина\n Яблоко Груша\r\n\r\n",
            replyMarkup: inlineKeyboard_InflaveBack);
        return;
    }
    #endregion


    #endregion

    #region Жидкости
    #region Команды
    InlineKeyboardMarkup inlineKeyboard_Zhidkosti = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Brusko", "brusko"),
                InlineKeyboardButton.WithCallbackData("Boshki", "boshki"),
                InlineKeyboardButton.WithCallbackData("Мишка", "мишки"),
                InlineKeyboardButton.WithCallbackData("HQD MIX IT", "hqdmix")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("HotSpot", "hotspot"),
                InlineKeyboardButton.WithCallbackData("Husky", "husky"),
                InlineKeyboardButton.WithCallbackData("Maxwell's", "maxwells"),
                InlineKeyboardButton.WithCallbackData("MAD", "mad")
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
                InlineKeyboardButton.WithCallbackData("HotSpot 18 - 2%", "hotspot2"),
                InlineKeyboardButton.WithCallbackData("HotSpot 20 - 5%", "hotspot5")
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "жидкости")
            },
                });
    InlineKeyboardMarkup inlineKeyboard_Mishki = new(new[]
  {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Мишка - 2%", "mishki2"),
                InlineKeyboardButton.WithCallbackData("Мишка - 5%", "mishki5")
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "жидкости")
            },
                });
    InlineKeyboardMarkup inlineKeyboard_Husky = new(new[]
{
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Husky - 2%", "Husky2"),
                InlineKeyboardButton.WithCallbackData("Husky - 5%", "Husky5")
            },
        new[]
            {
                InlineKeyboardButton.WithCallbackData("Назад", "жидкости")
            },
                });
    InlineKeyboardMarkup inlineKeyboard_Boshki = new(new[]
{
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Boshki - 2%", "boshki2"),
                InlineKeyboardButton.WithCallbackData("Boshki - 5%", "boshki5")
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

    #region Brusko
    InlineKeyboardMarkup inlineKeyboard_BruskoBack = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "brusko")
            }
    });
    if (callbackQuery.Data == "brusko")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите крепость:",
            replyMarkup: inlineKeyboard_Brusko);
        return;
    }
    if (callbackQuery.Data == "brusko2")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Американский Десерт\n Английская Ириска\n Апельсиновый лимонад \n Банановое Суфле\n Белый Русский\n Ванильный Табак\n Виноградные Леденцы\n Вишневая Кола\n Гранатовый Сок со Смородиной и Лимоном\n Грейпфрутовый Сок с Ягодами\n Зеленый чай с зимней хвоей\n Кактусовый Ананас\n Кактусовый Лимонад\n Карамельный Табак\n Кислинка\n Клюквенный лимонад \n Кокосовый Десерт\n Конфеты с Апельсиновым Ликером\n Космополитен\n Крем-Сода\n Ледяная Дыня\n Ледяная Клубника с Земляникой\n Ледяное Манго\n Ледяной Ананас\n Ледяной Арбуз\n Ледяной Манго\n Ледяные Яблоки\n Малиновый Лимонад\n Малиновый Пончик\n Мандарин\n Маргарита\n Мелисса с Мятой\n Ментол\n Розовый лимонад\n Сахарная Вата\n Сибирский Лимонад\n Табак с Орехами\n Табак с Черникой\n Таежный Морс\n Тархун\n Творожный Десерт с Кусочками Банана\n Тропический Коктейль\n Фруктовое Драже\n Фруктовый мусс\n Шоколад с Лесным Орехом\n Энергетик\n Энергетик с Манго\n Энергетик с Яблоком и Киви\n Ягодная Хвоя\n Ягодный Десерт\r\n\r\n",
            replyMarkup: inlineKeyboard_BruskoBack);
        return;
    }
    if (callbackQuery.Data == "brusko5")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Американский Десерт\n Английская Ириска\n Апельсиновый лимонад \n Банановое Суфле\n Белый Русский\n Ванильный Табак\n Виноградные Леденцы\n Вишневая Кола\n Гранатовый Сок со Смородиной и Лимоном\n Грейпфрутовый Сок с Ягодами\n Зеленый чай с зимней хвоей\n Кактусовый Ананас\n Кактусовый Лимонад\n Карамельный Табак\n Кислинка\n Клюквенный лимонад \n Кокосовый Десерт\n Конфеты с Апельсиновым Ликером\n Космополитен\n Крем-Сода\n Ледяная Дыня\n Ледяная Клубника с Земляникой\n Ледяное Манго\n Ледяной Ананас\n Ледяной Арбуз\n Ледяной Манго\n Ледяные Яблоки\n Малиновый Лимонад\n Малиновый Пончик\n Мандарин\n Маргарита\n Мелисса с Мятой\n Ментол\n Розовый лимонад\n Сахарная Вата\n Сибирский Лимонад\n Табак с Орехами\n Табак с Черникой\n Таежный Морс\n Тархун\n Творожный Десерт с Кусочками Банана\n Тропический Коктейль\n Фруктовое Драже\n Фруктовый мусс\n Шоколад с Лесным Орехом\n Энергетик\n Энергетик с Манго\n Энергетик с Яблоком и Киви\n Ягодная Хвоя\n Ягодный Десерт\r\n\r\n",
            replyMarkup: inlineKeyboard_BruskoBack);
        return;
    }
    #endregion

    #region HotSpot
    InlineKeyboardMarkup inlineKeyboard_HotSpotBack = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "hotspot")
            }
    });
    if (callbackQuery.Data == "hotspot")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите крепость:",
            replyMarkup: inlineKeyboard_HotSpot);
        return;
    }
    if (callbackQuery.Data == "hotspot2")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Ананас Ежевика\n Ананас Кокос\n Арбуз\n Брусника Лимон\n Дыня Черника\n Зеленое яблоко\n Киви Банан\n Киви Помело\n Кислые лесные ягоды\n Лайм Личи\n Ледяная вишня\n Ледяной Виноград\n Манго Грейпфрут\n Манго Персик\n Маракуйя\n Персик Маракуйя\n Персиковый сок\n Свежая Перечная Мята\n Смородина Мята\n Хвоя Грейпфрут\n Яблоко Груша\r\n\r\n",
            replyMarkup: inlineKeyboard_HotSpotBack);
        return;
    }
    if (callbackQuery.Data == "hotspot5")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Banana Coconut (Shot - 10 мл) \n Black Currant (Shot - 10 мл)\n Fresh Mango (Shot - 10 мл)\n Grape Aloe (Shot - 10 мл)\n Juicy Orange (Shot - 10 мл)\n Pink Grapefruit (Shot - 10 мл)\n Ripe Melon (Shot - 10 мл)\n Ripe Melon (Shot - 10 мл)\n Strawberry-Lychee (Shot - 10 мл)\n Sweet Green Mint (Shot - 10 мл)\n Tropical Pineapple (Shot - 10 мл)\n Watermelon\n Ананас Ежевика\n Ананас Кокос\n Брусника Лимон\n Дыня Черника\n Зеленое яблоко\n киви банан\n Киви Помело\n Кислая Маракуйя\n Кислое зеленое яблоко\n Кислое киви\n Кислые лесные ягоды\n Кислый ананас\n Ледяная вишня\n Ледяной виноград\n Личи Лайм\n Манго грейпфут\n Манго Персик\n Маракуйя\n Персик Маракуйя\n Свежая Перченая мята\n Смородина Мята\n Сочный персик\n Хвоя Грейпфрут\n Яблоко Груша\r\n\r\n",
            replyMarkup: inlineKeyboard_HotSpotBack);
        return;
    }
    #endregion

    #region Mishki
    InlineKeyboardMarkup inlineKeyboard_MishkiBack = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "мишки")
            }
    });
    if (callbackQuery.Data == "мишки")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите крепость:",
            replyMarkup: inlineKeyboard_Mishki);
        return;
    }
    if (callbackQuery.Data == "mishki2")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Ванильный Попкорн\n Манго Маракуйя\n Морозная Смородина\n Морс смородина калина\n Хвоя Ягоды\n Холодная Вишня\n Холодный ананас\n Чай смородина лимон\n Яблоко Энергетик\r\n",
            replyMarkup: inlineKeyboard_MishkiBack);
        return;
    }
    if (callbackQuery.Data == "mishki5")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Cinnabon\n Дыня Арбуз Огурец\n Кофе 3 в 1\n Морс Смородина калина\n Пончик с кремом\n Хвоя ягоды\n Чай смородина лимон\r\n",
            replyMarkup: inlineKeyboard_MishkiBack);
        return;
    }
    #endregion

    #region Husky
    InlineKeyboardMarkup inlineKeyboard_HuskyBack = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "husky")
            }
    });
    if (callbackQuery.Data == "husky")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите крепость:",
            replyMarkup: inlineKeyboard_Husky);
        return;
    }
    if (callbackQuery.Data == "Husky2")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Malaysian - Berserk - Лесные Ягоды\n Malaysian - Gum Wolf - Ледяная арбузная жвачка\n Malaysian - Ice Woody - Ледяная хвоя\n Malaysian - Jungle Hunter - Манго апельсин лёд\n Malaysian - Lemon Flock - Цитрусовый лёд\n Malaysian - Red Warg - Ледяная клубника малина \n Malaysian - Shake Pears - Персик Груша Личи лёд \n Malaysian - Sour Beast - Киви клубника и мята со льдом \n Malaysian - Tropic Hunter - Ананас Дыня и манго со льдом  \n Malaysian - Wolfberry - Волчья ягода со льдом\n Mint Series - Berry Hunter - Лесные Ягоды с мятой\n Mint Series - Blue Up - Голубика с мятой \n Mint Series - Citrus Days - Апельсин лимон с мятой\n Mint Series - Juicy Grapes - Виноград с мятой\n Mint Series - Red Garden - Клубника с мятой\n Mint Series - Sakura Forest - Вишня с мятой\n Mint Series - Sweet Buckshot - Гранат с мятой\n Mint Series - Water Place - Арбуз с мятой\n Premium - Animal Jam - Лесные Ягоды Малиновый Джем Лёд \n Premium - Big Ball - Арбуз Дыня Клубника Лёд\n Premium - Blood Boy - Манго  \n Premium - Dark Flesh - Черника Гуава Лёд\n Premium - Miami Snow - Ананас Личи Банан Лёд \n Premium - Yellow King - Дыня Алоэ Лёд\n Premium - Yogi Doggy - Йогурт Персик Клубника Лёд \r\n\r\n",
            replyMarkup: inlineKeyboard_HuskyBack);
        return;
    }
    if (callbackQuery.Data == "Husky5")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Malaysian - Berserk - Лесные Ягоды\n Malaysian - Gum Wolf - Ледяная арбузная жвачка\n Malaysian - Ice Woody - Ледяная хвоя\n Malaysian - Jungle Hunter - Манго апельсин лёд\n Malaysian - Lemon Flock - Цитрусовый лёд\n Malaysian - Red Warg - Ледяная клубника малина \n Malaysian - Shake Pears - Персик Груша Личи лёд \n Malaysian - Sour Beast - Киви клубника и мята со льдом \n Malaysian - Tropic Hunter - Ананас Дыня и манго со льдом  \n Malaysian - Wolfberry - Волчья ягода со льдом\n Mint Series - Berry Hunter - Лесные Ягоды с мятой\n Mint Series - Blue Up - Голубика с мятой \n Mint Series - Citrus Days - Апельсин лимон с мятой\n Mint Series - Juicy Grapes - Виноград с мятой\n Mint Series - Red Garden - Клубника с мятой\n Mint Series - Sakura Forest - Вишня с мятой\n Mint Series - Sweet Buckshot - Гранат с мятой\n Mint Series - Water Place - Арбуз с мятой\n Premium - Animal Jam - Лесные Ягоды Малиновый Джем Лёд \n Premium - Big Ball - Арбуз Дыня Клубника Лёд\n Premium - Blood Boy - Манго  \n Premium - Dark Flesh - Черника Гуава Лёд\n Premium - Miami Snow - Ананас Личи Банан Лёд \n Premium - Yellow King - Дыня Алоэ Лёд\n Premium - Yogi Doggy - Йогурт Персик Клубника Лёд \r\n\r\n",
            replyMarkup: inlineKeyboard_HuskyBack);
        return;
    }
    #endregion

    #region HQD MIX
    InlineKeyboardMarkup inlineKeyboard_HQDMIXBack = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "жидкости")
            }
    });
    if (callbackQuery.Data == "hqdmix")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Апельсин Черная смородина\n Арбуз Лайм и алоэ\n Банан Клубника Жвачка\n Вишня Яблоко и лайм\n Грейпфрут Лесные ягоды\n Гуава Гуанабана Маракуйя\n Киви Клубника\n Киви Личи\n Лимонное печенье\n Малиновый джем с маслом\n Манго Персик\n Мармеладные мишки\n Сибирь\n Хвоя с лимоном и мятой\n Хвоя Черная смородина\n Чай с лимоном и ягодами\r\n\r\n",
            replyMarkup: inlineKeyboard_HQDMIXBack);
        return;
    }
    #endregion

    #region Maxwell's
    InlineKeyboardMarkup inlineKeyboard_MaxwellBack = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "жидкости")
            }
    });
    if (callbackQuery.Data == "maxwells")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Altay\n Apple pie\n Black\n Cherry Punch\n Chill\n Green\n Jelly\n Lemon cake\n Mango\n Mojito\n Pink\n Pops\n Red\n Rich\n Shoria\n Shoria Summer\n Split\n Vera\n Тундра\r\n\r\n",
            replyMarkup: inlineKeyboard_MaxwellBack);
        return;
    }
    #endregion

    #region MAD
    InlineKeyboardMarkup inlineKeyboard_MadBack = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "жидкости")
            }
    });
    if (callbackQuery.Data == "mad")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n Apple Pasion Fruit\n Cactus Lime\n Cherry\n Cold Mango\n Ice Lychee\n Kiwi Melon\n Kiwi Pineapple\n Kuvi\n Lime Bubble Gum\n Lime Grapes\n Mix Wild Berries Red Bull\n Orange soda\n Peach Raspberry Lemonad\n Straweberry Guava\n Tropic Mix\n Морс\r\n",
            replyMarkup: inlineKeyboard_MadBack);
        return;
    }
    #endregion

    #region Boshki
    InlineKeyboardMarkup inlineKeyboard_BoshkiBack = new(new[]
           {
            new[]
            {
                InlineKeyboardButton.WithUrl("Связаться c менеджером", @"https://t.me/vova534"),
                InlineKeyboardButton.WithCallbackData("Назад", "boshki")
            }
    });
    if (callbackQuery.Data == "boshki")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберитеme крепость:",
            replyMarkup: inlineKeyboard_Boshki);
        return;
    }
    if (callbackQuery.Data == "boshki2")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберитеme вкус:\n Бодрые\n Дачные\n Добрые\n Докторские\n Зимние\n Злые\n Кубанские\n садовые\n Садовые\n Сахарные\n Сочные\n Тропические\n Целебние\n Черные\n Ягодки\n Cs\n Exotic\n Neon\n Original",
            replyMarkup: inlineKeyboard_BoshkiBack);
        return;
    }
    if (callbackQuery.Data == "boshki5")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберитеme вкус:\n Бодрые\n Дачные\n Добрые\n Добрые On Ice\n Зимние\n Злые\n Кубанские\n Садовые\n Сахарные\n Сочные\n Целебные\n Черные\n Exotic\n Neon\n Original\r\n",
            replyMarkup: inlineKeyboard_BoshkiBack);
        return;
    }
    #endregion


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

    

    if (callbackQuery.Data == "контакты")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Для заказа свяжитесь с кем-либо из них:",
            replyMarkup: inlineKeyboard_Kontakty);
        return;
    }

    /*
    await botClient.SendTextMessageAsync(
         callbackQuery.Message.Chat.Id,
         $"Для уточнения наличия товара и его бронирования свяжитесь с одним из чёрных:",
         replyMarkup: inlineKeyboard_Kontakty);*/


    /*if (callbackQuery.Message.MessageId != null)
    {
        await botClient.DeleteMessageAsync(
            msg.Chat.Id,
            callbackQuery.Message.MessageId);
        return;
    }*/
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

