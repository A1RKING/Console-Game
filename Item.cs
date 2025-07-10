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
    public int healthBonus;
    public int attackBonus;
    public int magicBonus;
    public int armorBonus;
    public ItemRare Rarity;
    public ItemType Type;

    public class Inventory()
    {
        public static Item head = new Item();
        public static Item hands = new Item();
        public static Item legs = new Item();
        public static Item body = new Item();
        public static Item rings = new Item();
        public static Item weapon = new Item();
        public void EquipItem(Item item)
        {
            switch (item.Type)
            {
                case ItemType.head:
                    Inventory.head = item;
                    break;
                case ItemType.hands:
                    Inventory.hands = item;
                    break;
                case ItemType.legs:
                    Inventory.legs = item;
                    break;
                case ItemType.body:
                    Inventory.body = item;
                    break;
                case ItemType.rings:
                    Inventory.rings = item;
                    break;
                case ItemType.weapon:
                    Inventory.weapon = item;
                    break;
            }
        }

        public (int health, int attack, int magic, int armor) GetStatsSum()
        {
            int sumHealth = (Inventory.head?.healthBonus ?? 0) +
                           (Inventory.hands?.healthBonus ?? 0) +
                           (Inventory.legs?.healthBonus ?? 0) +
                           (Inventory.body?.healthBonus ?? 0) +
                           (Inventory.rings?.healthBonus ?? 0) +
                           (Inventory.weapon?.healthBonus ?? 0);

            int sumAttack = (Inventory.head?.attackBonus ?? 0) +
                           (Inventory.hands?.attackBonus ?? 0) +
                           (Inventory.legs?.attackBonus ?? 0) +
                           (Inventory.body?.attackBonus ?? 0) +
                           (Inventory.rings?.attackBonus ?? 0) +
                           (Inventory.weapon?.attackBonus ?? 0);

            int sumMagic = (Inventory.head?.magicBonus ?? 0) +
                          (Inventory.hands?.magicBonus ?? 0) +
                          (Inventory.legs?.magicBonus ?? 0) +
                          (Inventory.body?.magicBonus ?? 0) +
                          (Inventory.rings?.magicBonus ?? 0) +
                          (Inventory.weapon?.magicBonus ?? 0);

            int sumArmor = (Inventory.head?.armorBonus ?? 0) +
                          (Inventory.hands?.armorBonus ?? 0) +
                          (Inventory.legs?.armorBonus ?? 0) +
                          (Inventory.body?.armorBonus ?? 0) +
                          (Inventory.rings?.armorBonus ?? 0) +
                          (Inventory.weapon?.armorBonus ?? 0);

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
        Console.WriteLine($" [{Type}] \n [{this.rare}] {this.name} \n +{this.healthBonus} HP \n +{this.attackBonus} AP \n +{this.magicBonus} MP");
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
        int roll = random.Next(1, 20);

        if (roll >= 17)
        {
            this.Rarity = ItemRare.legendary;
        }
        else if (roll >= 14)
        {
            this.Rarity = ItemRare.epic;
        }
        else if (roll >= 11)
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
        }
        else if (roll >= 13)
        {
            this.Type = ItemType.hands;
        }
        else if (roll >= 10)
        {
            this.Type = ItemType.legs;
        }
        else if (roll >= 7)
        {
            this.Type = ItemType.body;
        }
        else if (roll >= 4)
        {
            this.Type = ItemType.rings;
        }
        else
        {
            this.Type = ItemType.weapon;
        }
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
                this.armorBonus = 1;
                break;
            case ItemRare.rare:
                this.rare = "\u001b[32mRare\u001b[0m";
                this.healthBonus = random.Next(2, 15);
                this.attackBonus = random.Next(2, 4);
                this.magicBonus = random.Next(2, 8);
                this.armorBonus = 2;
                break;
            case ItemRare.epic:
                this.rare = "\u001b[35mEpic\u001b[0m";
                this.healthBonus = random.Next(3, 20);
                this.attackBonus = random.Next(3, 6);
                this.magicBonus = random.Next(3, 12);
                this.armorBonus = 3;
                break;
            case ItemRare.legendary:
                this.rare = "\u001b[33mLegendary\u001b[0m";
                this.healthBonus = random.Next(4, 25);
                this.attackBonus = random.Next(4, 8);
                this.magicBonus = random.Next(4, 16);
                this.armorBonus = 4;
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

    public enum ItemType
    { 
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