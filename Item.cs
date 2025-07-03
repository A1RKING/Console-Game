using static Item;
using static Hero;
using static Enemy;
using static GameEngine;
using static GameMessages;
using static Program;
public class Item
{
    Random random = new Random();
    public string name;
    public string rare;
    public int healthBonus;
    public int attackBonus;
    public int magicBonus;
    public int armorBonus;
    public ItemRare Rarity;

    public Item()
    {
        this.name = itemNames[random.Next(itemNames.Length)];
        DetermineRarity();
        RarityBonus();
    }

    public void ItemDisplayInfo()
    {
        Console.WriteLine($"[{this.rare}] {this.name} \n +{this.healthBonus} HP \n +{this.attackBonus} AP \n +{this.magicBonus} MP");
    }


    public string[] itemNames =
    {
    // ============ ОРУЖИЕ ============
    "Меч Погибели Тьмы",
    "Лук Стремительного Ветра",
    "Посох Вечного Пламени",
    "Кинжалы Теневого Шёпота",
    "Молот Громового Гнева",
    "Клинок Пробуждённой Ярости",
    "Арбалет Лунного Пронзания",
    "Секира Расколотого Черепа",
    "Коса Вечного Забвения",
    "Кувалда Титанового Гнева",
    "Копьё Небесной Кара",
    "Глефа Искажённой Реальности",
    "Катана Вишнёвого Ветра",
    "Цепь Пылающего Возмездия",
    "Костолом Древних Королей",
    "Коготь Теневого Дракона",
    "Рапира Аристократа Крови",
    "Серп Звёздного Жнеца",
    "Дубина Пещерного Тролля",
    "Булава Священного Грома",

    // ============ ДОСПЕХИ ============
    "Доспех Непробиваемой Воли",
    "Нагрудник Древнего Стража",
    "Шлем Неуязвимости",
    "Поножи Странника Бездны",
    "Рукавицы Ярости Титана",
    "Латный Набор Грозового Властителя",
    "Кираса Проклятого Паладина",
    "Шлем Испытания Богами",
    "Ножные Латы Вечного Дозора",
    "Перчатки Тысячи Ударов",
    "Наплечники Разрушенных Врат",
    "Пояс Несокрушимой Твердыни",
    "Сапоги Странствий между Мирами",
    "Нагрудник Ледяного Клинка",
    "Шлем Пылающего Демона",
    "Наручи Скрытого Потенциала",
    "Поножи Павшего Гиганта",
    "Ботфорты Короля Пустоши",
    "Перчатки Призрачного Удара",
    "Щит Абсолютной Защиты",

    // ============ АКСЕССУАРЫ ============
    "Амулет Скрытой Мощи",
    "Кольцо Бессмертного Духа",
    "Ожерелье Лунного Света",
    "Пояс Неутомимого Воина",
    "Плащ Исчезающего Тумана",
    "Печать Запретного Знания",
    "Камень Пробуждения Дракона",
    "Браслет Вечной Верности",
    "Талисман Забытых Богов",
    "Око Древнего Змея",
    "Серёги Шепчущего Ветра",
    "Гривна Вождя Орды",
    "Кулон Застывшего Времени",
    "Перстень Проклятой Крови",
    "Поясница Короля Пустыни",
    "Маска Тысячи Личин",
    "Накидка Падающей Звезды",
    "Знак Отверженного Героя",
    "Розетка Алхимика",
    "Обруч Пробуждённого Сознания",

    // ============ МАГИЧЕСКИЕ АРТЕФАКТЫ ============
    "Сфера Хаотической Энергии",
    "Свиток Забытых Заклинаний",
    "Книга Древних Пророчеств",
    "Фляга Вечной Жизни",
    "Кристалл Безграничной Маны",
    "Глаз Бездны",
    "Жезл Искажения Реальности",
    "Сердце Феникса",
    "Рунический Камень Сигил",
    "Чаша Грехов",
    "Зеркало Прошлых Жизней",
    "Тотем Первобытного Духа",
    "Фиал Слёз Богини",
    "Карта Иных Измерений",
    "Ключ От Врат Ада",
    "Дирижабль Алхимика",
    "Колокол Пробуждения",
    "Кукла Вуду Вечной Муки",
    "Скипетр Ледяного Владыки",
    "Оракул Судного Дня",

    // ============ ОРИГИНАЛЬНЫЕ ПРЕДМЕТЫ ============
    "Кольцо решимости",
    "Плащ маштабирования",
    "Зачарованые сапоги",
    "Наручи ловкости",
    "Шлем благословления",
    "Анальная пробка достоинства"
};



    public void DetermineRarity()
    {
        int roll = random.Next(1, 21);

        if (roll >= 19)
            this.Rarity = ItemRare.legendary;
        else if (roll >= 16)
            this.Rarity = ItemRare.epic;
        else if (roll >= 11)
            this.Rarity = ItemRare.rare;
        else
            this.Rarity = ItemRare.uncommon;
    }

    public void RarityBonus()
    {
        switch (this.Rarity)
        {
            case ItemRare.uncommon:
                this.rare = "Uncommon";
                this.healthBonus = random.Next(1, 10);
                this.attackBonus = random.Next(1, 2);
                this.magicBonus = random.Next(1, 4);
                break;
            case ItemRare.rare:
                this.rare = "\u001b[32mRare\u001b[0m";
                this.healthBonus = random.Next(2, 15);
                this.attackBonus = random.Next(2, 4);
                this.magicBonus = random.Next(2, 8);
                break;
            case ItemRare.epic:
                this.rare = "\u001b[35mEpic\u001b[0m";
                this.healthBonus = random.Next(3, 20);
                this.attackBonus = random.Next(3, 6);
                this.magicBonus = random.Next(3, 12);
                break;
            case ItemRare.legendary:
                this.rare = "\u001b[33mLegendary\u001b[0m";
                this.healthBonus = random.Next(4, 25);
                this.attackBonus = random.Next(4, 8);
                this.magicBonus = random.Next(4, 16);
                break;
        }
    }

    public enum ItemRare
    {
        uncommon,
        rare,
        epic,
        legendary
    }
}

public class Inventory
{
    Item head;
    Item hands;
    Item legs;
    Item body;
    List<Item> rings;
    Item weapon;
}