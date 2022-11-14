using Microsoft.VisualBasic;
using System.Xml.Linq;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;

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
            new KeyboardButton[] {"Наши магазины"},
            new KeyboardButton[] { "удалить" }

    })
    {
        ResizeKeyboard = true
    };

    InlineKeyboardMarkup inlineKeyboard_Kontakty = new(new[]
    {
         new[]
         {
                InlineKeyboardButton.WithUrl("Связаться c Владимиром", @"https://t.me/vova534"),
                InlineKeyboardButton.WithUrl("Связаться c Расулом", @"https://t.me/loaffer"),
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
            }, new[]
            {
                InlineKeyboardButton.WithCallbackData("Почистить чат", "удалить")
            }
                });

    // Message msg = new();

    //Message msg = await botClient.SendTextMessageAsync(message.Chat.Id, "Дообро пожаловать в магазин MonaPuff", replyMarkup: keyboard);

    switch (message.Text)
    {
        case "/start":
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"{firstName}, Добро пожаловать в MonaPuff, выберите какой-то пункт 🔽", replyMarkup: keyboard);
                break;
            }
        case "Меню":
            {
                //msg = await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали - Электронки", replyMarkup: keyboard);
                await botClient.SendTextMessageAsync(message.Chat.Id, "Выбирайте ^_^", replyMarkup: inlineKeyboard_Menu);

                break;
            }
        case "Контакты":
            {
                //msg = await botClient.SendTextMessageAsync(message.Chat.Id, "Вы выбрали - Жидкости", replyMarkup: keyboard);
                await botClient.SendTextMessageAsync(message.Chat.Id, "Связь с чёрными123 ", replyMarkup: inlineKeyboard_Kontakty);

                Console.WriteLine(message.Text, "\n");

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
        /*case "удалить":
            {
                for (int i = message.MessageId; i >= 0; i--)
                {
                    try
                    {
                        await botClient.DeleteMessageAsync(
                            message.Chat.Id,
                            i);
                        Console.WriteLine(i);
                    }
                    catch
                    {
                        continue;
                    }
                }
                break;
            }*/

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
                InlineKeyboardButton.WithCallbackData("1500", "lostmary_1500"),
                InlineKeyboardButton.WithCallbackData("4000", "lostmary_2500"),
                InlineKeyboardButton.WithCallbackData("5000", "lostmary_4000"),
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
                InlineKeyboardButton.WithCallbackData("4200", "udn_4200"),
                InlineKeyboardButton.WithCallbackData("4500", "udn_4500"),
                InlineKeyboardButton.WithCallbackData("4800", "udn_4800"),
                InlineKeyboardButton.WithCallbackData("5200", "udn_5200"),
                InlineKeyboardButton.WithCallbackData("6000", "udn_6000"),
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
    if (callbackQuery.Data == "cuvie")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: \n Big Smoke \n Ананас \n Апельсин \n Арбуз \n Банан \n Ванильное мороженое \n Виноград \n Гранатовый сок Смородина и лимон \n Дыня \n Жвачка Мята Арбуз \n Йогурт Лесные ягоды \n Капучино \n Клубника \n Клубника Киви \n Кола \n Лимонный пирог \n Личи \n Малина Лимон \n Манго \n Персик \n Розовый лимонад \n Сибирь,мята,хвоя и лесные ягоды \n Фруктовый микс \n Черника \n Черника Малина Виноград \n Энергетик \n Яблоко \n Яблоко Киви Энергетик\r\n",
            replyMarkup: inlineKeyboard_HQD);
        return;
    }
    if (callbackQuery.Data == "cuvie plus")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус:\n BigSmoke \n FruitFusion \n Lov66 \n PinkLemon \n PogOrangeGuava \n Ананас \n Арбузнаяжвачка \n Байкал \n Банан \n Ванильноемороженое \n Виноград \n ГранатКиви \n Гранатовыйсоксмородинаилимон \n Дыня \n ДыняТорпедо \n Ежевика \n ЖвачкаМятаАрбуз \n ЙогуртЛесныеягоды \n Кактусовыйлимонад \n Кислыемармеладныечервячки \n Клубника \n КлубникаБанан \n КлубникаКиви \n Клубника-питайя \n Клубничноепеченье \n Клубничныйлимонад \n Клубничныймилкшейк \n Ледянаямята \n ЛедянойПерсик \n Лимонсморскойсолью \n Личи \n МалинаЛимон \n Манго \n МангоПерсик \n Маракуйя \n Мороженое \n Мультифрукт \n ПинаКолада \n Соленаякарамель \n Тархун \n Черника \n ЧерникаМалина \n ЧерникаМалинаВиноград \n ЧерничныйЛимонад \n Черныйчайсосмородиной \n Чистый \n Энергетик \n Яблоко \n ЯблокоПерсик \n ЯблочныйПерсик\r\n",
            replyMarkup: inlineKeyboard_HQD);
        return;
    }
    if (callbackQuery.Data == "hit")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: \n Апероль шприц \n Ароматный глинтвейн \n Банановый кекс \n Барбарис \n Блины с медом \n Вафли с кленовым сиропом \n Виноград и Алоэвера \n Вишневый энергетик \n Жвачка \n Карамельный попкорн \n Клубника Маракуйя \n Кола Лимон \n Кола Манго \n Лимонад Кактус-лайм \n Малина и клюква \n Персик Абрикос \n Раф с лесным ягодами \n Тайга (хвоя и смородина) \n Холодный чай с лимоном \n Черничный лимонад \n Черный чай с ягодным вареньем \n Яблоко Манго-груша \n Ягода мушмула \n Ягодный сорбет\r\n",
            replyMarkup: inlineKeyboard_HQD);
        return;
    }
    if (callbackQuery.Data == "mega")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: \n Ананасовый экспресс \n Арбузная жвачка \n Белый Русский \n Ванильное мороженое \n Клубника Арбуз \n Клубника Банан \n Клубника Киви \n Клубничный милкшейк \n Клубничный пончик \n Ледяная мята \n Лимонад Черника-малина \n Малина \n Манго \n Манго Дыня \n Мармелад \n Мармеладные мишки \n Персик \n Пинаколада \n Сочный арбуз \n Сочный виноград \n Черника \n Энергетик \n Яблоко Персик \n Ягодный фреш\r\n",
            replyMarkup: inlineKeyboard_HQD);
        return;
    }
    if (callbackQuery.Data == "king")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: \n Ананас \n Ванильное мороженое \n Виноград \n Гранатовый сок Смородина и лимон \n Жвачка \n Жвачка Мята Арбуз \n Йогурт Лесные ягоды \n Капучино \n Клубника \n Клубника Банан \n Клубника Киви \n Коктейль Белый Русский \n Лайм Кола \n Малина Лимон \n Манго \n Мультифрукт \n Мятная Жвачка \n Персик \n Пинаколада \n Сибирь Мята Хвоя и лесные ягоды \n Туманы майями \n Черника \n Черника малина виноград \n Энергетик\r\n",
            replyMarkup: inlineKeyboard_HQD);
        return;
    }
    if (callbackQuery.Data == "maxx")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: \n Ананас Манго Персик \n Апельсин Манго-гуава \n Барбарис \n Вишневая кола \n Двойное яблоко \n Карамельный табак \n Клубника Виноград \n Коктейль испанская орчата \n Коктейль Карибский дождь \n Коктейль Лонг Айленд \n Коктейль Маргарита \n Кола-ваниль \n Ликер Егерь \n Манго Клубника \n Мохито \n Ореховый батончик \n Персик Манго Арбуз \n Пинаколада \n Попкорн \n Сахарная вата \n Хвоя и лесные ягоды \n Черная смородина \n Черный чай со смородиной \n Энергетик Яблоко-киви \n Яблоко Манго Груша\r\n",
            replyMarkup: inlineKeyboard_HQD);
        return;
    }
    if (callbackQuery.Data == "cuvie air")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: \n Арбуз Лёд \n Ананас \n Барбарис \n Виноград \n Гранатовый сок смородина и лимон \n Ежевика \n Жвачка \n Киви Лимонад \n Клубника Арбуз \n Клубника Киви \n Клубника Кокос \n Клубника манго \n Лайм кола \n Ледяной Банан \n Ледяной Шоколад \n Лесные ягоды \n Личи Айс \n Манго \n Мармеладные Мишки \n Мятная жвачка \n Персик \n Радуга \n Русский крем \n Сибирь \n Черника \n Черника Лимон \n Черника Малина \n Яблоко Груша \n Конфеты\r\n",
            replyMarkup: inlineKeyboard_HQD);
        return;
    }
    if (callbackQuery.Data == "hot")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: \n Ананас \n Арбуз \n Арбуз Банан \n Виноград \n Виноград Алоэ \n Вишневая кола \n Гранатовый сок со смородиной \n Ежевика \n Жвачка \n Йогуртовое Мороженое \n Клубника Банан \n Клубника Киви \n Лайм Малина \n Ледяное клубничное мороженое \n Лимон-Маракуйя \n Малина Лимон \n Манго \n Манго Персик Арбуз \n Персик \n Черная Смородина Мята Алое \n Черника \n Черника Малина Виноград \n Яблоко виноград лед \n Яблоко Персик \n Ягодный мохито \n Черная смородина\r\n",
            replyMarkup: inlineKeyboard_HQD);
        return;
    }
    #endregion
    #region ELF BAR
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
            replyMarkup: inlineKeyboard_ElfBar);
    }
    if (callbackQuery.Data == "elf_2000")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: \n Ананас манго апельсин \n Голубика малина лед \n Киви маракуйя гуава \n Киви ягода \n Клубника арбуз \n Кола \n Красный мохито \n Манго Персик Арбуз \n Персик Манго Гуава \n Яблоко Персик",
            replyMarkup: inlineKeyboard_ElfBar);
        return;
    }
    if (callbackQuery.Data == "")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: ",
            replyMarkup: inlineKeyboard_ElfBar);
        return;
    }
    if (callbackQuery.Data == "")
    {
        await botClient.EditMessageTextAsync(
            callbackQuery.From.Id.ToString(),
            callbackQuery.Message.MessageId,
            $"Выберите вкус: ",
            replyMarkup: inlineKeyboard_ElfBar);
        return;
    }
    #endregion
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
            $"Выбирайте: \n 123 \n 123",
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

