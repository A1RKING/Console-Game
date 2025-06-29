// ЗАДАЧА: Создать простую боевую систему RPG
// 
// ТРЕБОВАНИЯ:
// 1. Класс Hero - игровой персонаж игрока
//    V Поля: name, health, maxHealth, attackPower, magicPower
//    V Методы: Attack(), CastSpell(), TakeDamage(), Heal(), IsAlive(), DisplayStatus()
//
// 2. Класс Enemy - враг
//    V Поля: name, health, maxHealth, attackPower  
//    V Методы: Attack(), TakeDamage(), IsAlive(), DisplayInfo()
//
// 3. Статический класс GameEngine
//    V Метод StartBattle() - управление боем
//    V Метод ShowPlayerMenu() - показ действий игрока
//    V Метод GetPlayerChoice() - получение выбора
//    V Метод CreateRandomEnemy() - создание случайного врага
//
// 4. Статический класс GameMessages
//    V Метод ShowWelcome() - приветствие
//    V Метод ShowBattleResult() - результат боя
//    V Метод ShowDamage() - сообщение об уроне
//
// МЕХАНИКА БОя:
//    V Игрок выбирает: 1-атака, 2-заклинание, 3-лечение
//    V Враг атакует автоматически
//    V Ходы по очереди до смерти одного персонажа
//    V После победы появляется новый враг
//===================================================================================================================================================================
//   Что нужно сделать:
//    V Создать класс предмета.
//    V Добавить в этот класс характеристики предмета (например, +к урону или +к хп).
//    V Характеристики назначаются рандомные при создании предмета.
//    V Игроку добавить инвентарь (список предметов).
//    V После убийства моба создается случайный предмет и добавляется в инвентарь.
//    V При атаке игрока или при получении урона считаются все предметы и их баффы на урон или защиту.

//   Задача со звездочкой:
//    V Создать enum редкости предметов и добавить в класс предмета.
//    V При создании предмета рандомно определяется еще редкость.
//    V Каждая редкость имеет постоянный коэффициент к характеристикам предмета.
//    V У каждой редкости есть свой цвет (раз уж в консоль выводить научился). Пишешь в консоль после убийства моба - получен предмет с такими-то характеристиками.
//    V Присвоение случайного имени
//   Задача с двумя звездочками:
//    - Создать enum с типами предметов(кольцо, меч, шапка и т.д..)
//    - При создании предмета добавлять ему еще тип.

//
// ПРИМЕР СТРУКТУРЫ:
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static Item;

public class Hero
{
    Random random = new Random();
    public List<Item> inventory = new List<Item>(10);
    public string name;
    public int health;
    public int maxHealth;
    public int attackPower;
    public int magicPower;
    public int armor;
    public int maxInventorySize = 10;

    public Hero(string heroName)
    {
        name = heroName;
        health = 100;
        maxHealth = 100;
        attackPower = 15;
        magicPower = 25;
        armor = 3;
        inventory = new List<Item>();
    }

    public void Attack(Enemy target)
    {
        int totalDamage = attackPower + random.Next(1, 5);
        target.TakeDamage(totalDamage);
        GameMessages.ShowDamage(this.name, target.name, totalDamage);
    }

    public void CastSpell(Enemy target)
    {
        int recoil = 0;
        int totalDamage = magicPower + random.Next(1, 10);
        target.TakeDamage(totalDamage);
        this.TakeDamage(totalDamage / 3);
        recoil = (totalDamage / 3);
        GameMessages.ShowDamage(this.name, target.name, totalDamage);
        Console.WriteLine($"Магическая отдача нанесла {recoil} урона!");
    }

    public void TakeDamage(int damage)
    {
        if (this.health > 0)
        {
            this.health = this.health - damage;
        }
        else if (this.health <= 0)
        {
            this.health = 0;
        }
    }

    public void Heal()
    {
        int totalHeal = this.magicPower + random.Next(10, 25);
        this.health = this.health + totalHeal;
        Console.WriteLine($"{this.name} восполнил здоровье на {totalHeal}!");

        if (this.health > this.maxHealth)
        {
            this.health = this.maxHealth;
        }
        Console.WriteLine($"Уровень очков здоровья: {this.health}");
    }

    public bool IsAlive()
    {
        return this.health > 0;
    }

    public void DisplayStatus()
    {
        if (this.health > this.maxHealth * 0.6)
        {
            Console.WriteLine($"Герой: [{this.name}] | HP: [\u001b[32m{this.health}\u001b[0m]/[{this.maxHealth}] | Атака: [{this.attackPower}]");
        }
        else if (this.health < (this.maxHealth / 3) * 2 && this.health >= this.maxHealth / 3)
        {
            Console.WriteLine($"Герой: [{this.name}] | HP: [\u001b[33m{this.health}\u001b[0m]/[{this.maxHealth}] | Атака: [{this.attackPower}]");
        }
        else if (this.health < this.maxHealth / 3 && this.health >= 0)
        {
            Console.WriteLine($"Герой: [{this.name}] | HP: [\u001b[31m{this.health}\u001b[0m]/[{this.maxHealth}] | Атака: [{this.attackPower}]");
        }
    }

    public void AddToInventory(Item item)
    {
        if (inventory.Count < this.maxInventorySize)
        {
            this.inventory.Add(item);
            this.health += item.healthBonus;
            this.maxHealth += item.healthBonus;
            this.attackPower += item.attackBonus;
            this.magicPower += item.magicBonus;
        }
        else
        {
            Console.WriteLine("Инвентарь полон!");
        }


    }

    public void InventoryDisplayInfo()
    {
        Console.WriteLine("\n=== ИНВЕНТАРЬ! ===");
        if (inventory.Count == 0)
        {
            Console.WriteLine("Инвентарь пуст!");
        }
        else
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                Console.WriteLine($"{i}");
                inventory[i].ItemDisplayInfo();
            }
        }
    }

}

public class Item
{
    Random random = new Random();
    public string name;
    public string rare;
    public int healthBonus;
    public int attackBonus;
    public int magicBonus;
    public int armorBonus;
    public Hero Hero;
    public ItemRare Rarity;

    public Item()
    {
        this.name = itemNames[random.Next(itemNames.Length)];
        DetermineRarity();
        RarityBonus();
    }

    public void ItemDisplayInfo()
    {
        if (rare == "Uncommon")
        {
            Console.WriteLine($"[{this.rare}] {this.name} \n +{this.healthBonus} HP \n +{this.attackBonus} AP \n +{this.magicBonus} MP");
        } else if (rare == "Rare")
        {
            Console.WriteLine($"[\u001b[32m{this.rare}\u001b[0m] {this.name} \n +{this.healthBonus} HP \n +{this.attackBonus} AP \n +{this.magicBonus} MP");
        } else if (rare == "Epic")
        {
            Console.WriteLine($"[\u001b[35m{this.rare}\u001b[0m] {this.name} \n +{this.healthBonus} HP \n +{this.attackBonus} AP \n +{this.magicBonus} MP");
        } else if (rare == "Legendary")
        {
            Console.WriteLine($"[\u001b[33m{this.rare}\u001b[0m] {this.name} \n +{this.healthBonus} HP \n +{this.attackBonus} AP \n +{this.magicBonus} MP");
        } else
        {
            Console.WriteLine("System Error");
        }


    }


    public string[] itemNames =
    {
        "Кольцо решимости" , "Плащ маштабирования" , "Зачарованые сапоги" , "Наручи ловкости" , "Шлем благословления" , "Анальная пробка достоинства"
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
                this.rare = "Rare";
                this.healthBonus = random.Next(2, 15);
                this.attackBonus = random.Next(2, 4);
                this.magicBonus = random.Next(2, 8);
                break;
            case ItemRare.epic:
                this.rare = "Epic";
                this.healthBonus = random.Next(3, 20);
                this.attackBonus = random.Next(3, 6);
                this.magicBonus = random.Next(3, 12);
                break;
            case ItemRare.legendary:
                this.rare = "Legendary";
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

public class Enemy
{
    Random random = new Random();
    public string name;
    public int health;
    public int maxHealth;
    public int attackPower;

    public Enemy(string enemyName, int hp, int attack)
    {
        this.name = enemyName;
        this.health = hp;
        this.maxHealth = hp;
        this.attackPower = attack;
    }

    public void Attack(Hero target)
    {
        int totalDamage = this.attackPower + random.Next(1, 5) - target.armor;
        if (totalDamage <= 0) totalDamage = 0; else
        target.TakeDamage(totalDamage);
        GameMessages.ShowDamage(this.name, target.name, totalDamage);
    }

    public void TakeDamage(int damage)
    {
        if (this.health > 0)
        {
            this.health = this.health - damage;
        }
        else if (this.health <= 0)
        {
            this.health = 0;
        }
    }

    public bool IsAlive()
    {
        if (this.health > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Враг: [{this.name}] | HP: [{this.health}]/[{this.maxHealth}]  | Атака: [{this.attackPower}]");
    }
}



public static class GameEngine
{
    static Random random = new Random();
    private static EnemyRace Rarity;
    private static string name = "";
    private static int health;
    private static int attack;


    public static void StartBattle(Hero hero, Enemy enemy)
    {
        Console.WriteLine($"\n=== БОЙ НАЧАЛСЯ! ===");
        Console.WriteLine($"{hero.name} VS {enemy.name}");

        while (hero.IsAlive() && enemy.IsAlive())
        {
            // Ход игрока
            Console.WriteLine("\n--- ВАШ ХОД ---");
            hero.DisplayStatus();
            enemy.DisplayInfo();

            ShowPlayerMenu();
            int choice = GetPlayerChoice();

            // Выполняем действие игрока
            switch (choice)
            {
                case 1:
                    hero.Attack(enemy);
                    break;
                case 2:
                    hero.CastSpell(enemy);
                    break;
                case 3:
                    hero.Heal();
                    break;
                case 4:
                    hero.InventoryDisplayInfo();
                    break;

            }

            // Проверяем, жив ли враг
            if (!enemy.IsAlive())
            {
                Console.WriteLine($"{enemy.name} повержен!");
                Item item = new Item();
                hero.AddToInventory(item);
                item.ItemDisplayInfo();
                return;
            }

            // Ход врага
            Console.WriteLine("\n--- ХОД ВРАГА ---");
            enemy.Attack(hero);

            // Небольшая пауза для драматизма
            System.Threading.Thread.Sleep(1500);
        }

        if (!hero.IsAlive())
        {
            Console.WriteLine("Вы потерпели поражение!");
        }
    }

    public static void ShowPlayerMenu()
    {
        Console.WriteLine("\nВыберите действие:");
        Console.WriteLine("1 - Атака");
        Console.WriteLine("2 - Заклинание (больше урона, но тратит HP)");
        Console.WriteLine("3 - Лечение");
        Console.WriteLine("4 - Показ инвентаря");
    }

    public static int GetPlayerChoice()
    {
        int choice;
        while (true)
        {
            Console.Write("Ваш выбор (1-4): ");
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 4)
            {
                return choice;
            }
            Console.WriteLine("Неверный выбор! Введите число от 1 до 4.");
        }
    }

    public static Enemy CreateRandomEnemy()
    {
        DetermineEnemy();
        EnemyStats();
        


        return new Enemy(name, health, attack);
    }

    public static void DetermineEnemy()
    {
        int roll = random.Next(1, 21);

        if (roll >= 19)
            Rarity = EnemyRace.GreatOgr;
        else if (roll >= 16)
            Rarity = EnemyRace.bandit;
        else if (roll >= 12)
            Rarity = EnemyRace.wolf;
        else if (roll >= 8)
            Rarity = EnemyRace.skelet;
        else if (roll >= 4)
            Rarity = EnemyRace.orc;
        else if (roll >= 0)
            Rarity = EnemyRace.goblin;
    }

    public static void EnemyStats()
    {
        switch (GameEngine.Rarity)
        {
            case EnemyRace.goblin:
                name = "Гоблин";
                health = random.Next(50, 61);
                attack = random.Next(10, 16);
                break;
            case EnemyRace.orc:
                name = "Орк";
                health = random.Next(60, 81);
                attack = random.Next(15, 21);
                break;
            case EnemyRace.skelet:
                name = "Скелет";
                health = random.Next(80, 91);
                attack = random.Next(15, 26);
                break;
            case EnemyRace.wolf:
                name = "Волк";
                health = random.Next(40, 51);
                attack = random.Next(10, 16);
                break;
            case EnemyRace.bandit:
                name = "Бандит";
                health = random.Next(60, 71);
                attack = random.Next(10, 21);
                break;
            case EnemyRace.GreatOgr:
                name = "Великий Вождь Огр";
                health = random.Next(160, 201);
                attack = random.Next(30, 51);
                break;
        }
    }

    enum EnemyRace
    {
        goblin,
        orc,
        skelet,
        wolf,
        bandit,
        GreatOgr
    }

}

public static class GameMessages
{
    public static void ShowWelcome()
    {
        Console.WriteLine("=== ДОБРО ПОЖАЛОВАТЬ В RPG БИТВЫ ===");
        Console.WriteLine("Сражайтесь с врагами и становитесь сильнее!");
    }

    public static void ShowBattleResult(bool playerWon)
    {
        if (playerWon)
        {
            Console.WriteLine("🎉 ПОБЕДА! Вы одолели врага!");
        }
        else
        {
            Console.WriteLine("💀 ПОРАЖЕНИЕ! Вы пали в бою...");
        }
    }

    public static void ShowDamage(string attacker, string target, int damage)
    {
        Console.WriteLine($"⚔️ {attacker} наносит {damage} урона {target}!");
    }
}



class Program
{
    static void Main(string[] args)
    {
        GameMessages.ShowWelcome();

        Console.Write("Введите имя вашего героя: ");
        string heroName = Console.ReadLine();
        Hero player = new Hero(heroName);

        Console.WriteLine($"Герой {heroName} готов к приключениям!");

        // Игровой цикл
        while (player.IsAlive())
        {
            Enemy currentEnemy = GameEngine.CreateRandomEnemy();
            Console.WriteLine($"\nПоявился новый враг: {currentEnemy.name}!");

            GameEngine.StartBattle(player, currentEnemy);

            if (!player.IsAlive())
            {
                Console.WriteLine("\n=== ИГРА ОКОНЧЕНА ===");
                break;
            }

            Console.WriteLine("\nНажмите Enter для следующего боя...");
            Console.ReadLine();
        }

        Console.WriteLine("Спасибо за игру!");
    }
}