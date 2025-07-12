using static Item;
using static Hero;
using static Enemy;
using static GameEngine;
using static GameMessages;
using static Program;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

public class Item
{
    Random random = new Random();
    public string name;
    public string rare;
    public string type;
    public string typeName;
    public int healthBonus;
    public int attackBonus;
    public int magicBonus;
    public int armorBonus;
    public ItemRare Rarity;
    public ItemType Type;

    public class Inventory
    {
        public static Item head = null;
        public static Item hands = null;
        public static Item legs = null;
        public static Item body = null;
        public static Item rings = null;
        public static Item weapon = null;

        public static void EquipItem(Item item)
        {
            switch (item.Type)
            {
                case ItemType.head:
                    head = item;
                    break;
                case ItemType.hands:
                    hands = item;
                    break;
                case ItemType.legs:
                    legs = item;
                    break;
                case ItemType.body:
                    body = item;
                    break;
                case ItemType.rings:
                    rings = item;
                    break;
                case ItemType.weapon:
                    weapon = item;
                    break;
            }
        }

        public static Item GetEquippedItem(ItemType type)
        {
            Item result = null;
            switch (type)
            {
                case ItemType.head:
                    result = head;
                    break;
                case ItemType.hands:
                    result = hands;
                    break;
                case ItemType.legs:
                    result = legs;
                    break;
                case ItemType.body:
                    result = body;
                    break;
                case ItemType.rings:
                    result = rings;
                    break;
                case ItemType.weapon:
                    result = weapon;
                    break;
                default:
                    result = null;
                    break;
            }
            return result;
        }

        public static (int health, int attack, int magic, int armor) GetStatsSum()
        {
            int sumHealth = (head?.healthBonus ?? 0) +
                          (hands?.healthBonus ?? 0) +
                          (legs?.healthBonus ?? 0) +
                          (body?.healthBonus ?? 0) +
                          (rings?.healthBonus ?? 0) +
                          (weapon?.healthBonus ?? 0);

            int sumAttack = (head?.attackBonus ?? 0) +
                          (hands?.attackBonus ?? 0) +
                          (legs?.attackBonus ?? 0) +
                          (body?.attackBonus ?? 0) +
                          (rings?.attackBonus ?? 0) +
                          (weapon?.attackBonus ?? 0);

            int sumMagic = (head?.magicBonus ?? 0) +
                         (hands?.magicBonus ?? 0) +
                         (legs?.magicBonus ?? 0) +
                         (body?.magicBonus ?? 0) +
                         (rings?.magicBonus ?? 0) +
                         (weapon?.magicBonus ?? 0);

            int sumArmor = (head?.armorBonus ?? 0) +
                         (hands?.armorBonus ?? 0) +
                         (legs?.armorBonus ?? 0) +
                         (body?.armorBonus ?? 0) +
                         (rings?.armorBonus ?? 0) +
                         (weapon?.armorBonus ?? 0);

            return (sumHealth, sumAttack, sumMagic, sumArmor);
        }
    }

    public Item()
    {
        healthBonus = 0;
        attackBonus = 0;
        magicBonus = 0;
        armorBonus = 0;

        GetItemType();
        GetItemName();
        GetItemRarity();
        RarityBonus();
    }

    public void ItemDisplayInfo()
    {
        Console.WriteLine($"\n [{this.typeName}] \n [{this.rare}] {this.name} " +
                        $"\n +{this.healthBonus} HP " +
                        $"\n +{this.attackBonus} AP " +
                        $"\n +{this.magicBonus} MP " +
                        $"\n +{this.armorBonus} DEF");
    }

    public void GetItemName()
    {
        if (Type == ItemType.head)
        {
            this.name = ItemNames.head[random.Next(ItemNames.head.Length)];
        }
        else if (Type == ItemType.hands)
        {
            this.name = ItemNames.hands[random.Next(ItemNames.hands.Length)];
        }
        else if (Type == ItemType.legs)
        {
            this.name = ItemNames.legs[random.Next(ItemNames.legs.Length)];
        }
        else if (Type == ItemType.body)
        {
            this.name = ItemNames.body[random.Next(ItemNames.body.Length)];
        }
        else if (Type == ItemType.rings)
        {
            this.name = ItemNames.rings[random.Next(ItemNames.rings.Length)];
        }
        else
        {
            this.name = ItemNames.weapon[random.Next(ItemNames.weapon.Length)];
        }
    }

    public void GetItemRarity()
    {
        int roll = random.Next(1, 21);

        if (roll >= 18)
        {
            this.Rarity = ItemRare.legendary;
        }
        else if (roll >= 15)
        {
            this.Rarity = ItemRare.epic;
        }
        else if (roll >= 8)
        {
            this.Rarity = ItemRare.rare;
        }
        else
        {
            this.Rarity = ItemRare.uncommon;
        }
    }

    public void GetItemType()
    {
        int roll = random.Next(1, 19);

        if (roll >= 16)
        {
            this.Type = ItemType.head;
            this.typeName = "Шлем";
        }
        else if (roll >= 13)
        {
            this.Type = ItemType.hands;
            this.typeName = "Перчатки";
        }
        else if (roll >= 10)
        {
            this.Type = ItemType.legs;
            this.typeName = "Штаны";
        }
        else if (roll >= 7)
        {
            this.Type = ItemType.body;
            this.typeName = "Кираса";
        }
        else if (roll >= 4)
        {
            this.Type = ItemType.rings;
            this.typeName = "Кольцо";
        }
        else
        {
            this.Type = ItemType.weapon;
            this.typeName = "Оружие";
        }
    }

    public void RarityBonus()
    {
        switch (this.Rarity)
        {
            case ItemRare.uncommon:
                this.rare = "Обычный";
                this.healthBonus = random.Next(5, 10);
                this.attackBonus = random.Next(1, 3);
                this.magicBonus = random.Next(3, 5);
                this.armorBonus = 2;
                break;
            case ItemRare.rare:
                this.rare = "\u001b[32mРедкый\u001b[0m";
                this.healthBonus = random.Next(10, 15);
                this.attackBonus = random.Next(3, 6);
                this.magicBonus = random.Next(5, 10);
                this.armorBonus = 4;
                break;
            case ItemRare.epic:
                this.rare = "\u001b[35mЭпическый\u001b[0m";
                this.healthBonus = random.Next(15, 20);
                this.attackBonus = random.Next(6, 12);
                this.magicBonus = random.Next(8, 15);
                this.armorBonus = 8;
                break;
            case ItemRare.legendary:
                this.rare = "\u001b[33mЛегендарный\u001b[0m";
                this.healthBonus = random.Next(25, 40);
                this.attackBonus = random.Next(15, 20);
                this.magicBonus = random.Next(15, 25);
                this.armorBonus = 10;
                break;
        }
    }

    public enum ItemRare
    {
        none,
        uncommon,
        rare,
        epic,
        legendary
    }

    public enum ItemType
    {
        none,
        head,
        hands,
        legs,
        body,
        rings,
        weapon
    }
    public static class ItemNames
    {
        // Оружие
        public static readonly string[] weapon =
        {
        "Меч Рассветного Паладина",
        "Топор Гнева Титанов",
        "Лук Лунного Изгнанника",
        "Клинок Теневого Убийцы",
        "Молот Громового Бога",
        "Коса Вечной Жатвы",
        "Посох Ледяного Архимага",
        "Кинжалы Проклятой Ночи",
        "Копьё Небесной Карающей Силы",
        "Цепь Пылающего Ада"
    };

        // Голова
        public static readonly string[] head =
        {
        "Шлем Грозового Властителя",
        "Венец Ледяной Вечности",
        "Маска Теневого Шёпота",
        "Наголовье Пробуждённого Дракона",
        "Капюшон Безмолвного Убийцы",
        "Корона Забытого Короля",
        "Шлем Непробиваемой Воли",
        "Боевой Гребень Ярости",
        "Личина Проклятого Стража",
        "Диадема Вечного Света"
    };

        // Руки
        public static readonly string[] hands =
        {
        "Перчатки Адского Пламени",
        "Наручи Несокрушимой Стали",
        "Рукавицы Громового Удара",
        "Браслеты Лунного Шёпота",
        "Когти Теневого Охотника",
        "Перчатки Вечного Мороза",
        "Захваты Каменного Кулака",
        "Напульсники Проклятой Крови",
        "Рукавицы Божественной Силы",
        "Перчатки Заклинателя Ветров"
    };

        // Ноги
        public static readonly string[] legs =
        {
        "Поножи Бессмертного Воина",
        "Штаны Теневого Плута",
        "Набедренники Древнего Ордена",
        "Брюки Грозового Марша",
        "Ножные Латы Вечной Стали",
        "Килт Ярости Варвара",
        "Пластинчатые Штаны Драконоборца",
        "Штаны Изгнанного Мага",
        "Обмотки Странника Миров",
        "Кожаные Поножи Охотника"
    };

        // Торс
        public static readonly string[] body =
        {
        "Кираса Небесного Стража",
        "Доспех Проклятого Рыцаря",
        "Мундир Теневого Лорда",
        "Панцирь Драконьей Чешуи",
        "Кольчуга Вечного Пламени",
        "Нагрудник Громового Сердца",
        "Латы Непробиваемой Тьмы",
        "Роба Архимага Пустоты",
        "Броня Каменного Голема",
        "Жилет Скрытности Убийцы"
    };

        // Кольца
        public static readonly string[] rings =
        {
        "Кольцо Вечной Мудрости",
        "Перстень Проклятой Крови",
        "Кольцо Ледяного Дыхания",
        "Перстень Теневого Пути",
        "Кольцо Ярости Вулкана",
        "Перстень Забытых Королей",
        "Кольцо Молчаливого Убийцы",
        "Перстень Древнего Духа",
        "Кольцо Божественного Щита",
        "Перстень Хаоса"
    };
    }
}