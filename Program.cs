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
//    - Характеристики назначаются рандомные при создании предмета.
//    V Игроку добавить инвентарь (список предметов).
//    - После убийства моба создается случайный предмет и добавляется в инвентарь.
//    - При атаке игрока или при получении урона считаются все предметы и их баффы на урон или защиту.

//   Задача со звездочкой:
//    - Создать enum редкости предметов и добавить в класс предмета.
//    - При создании предмета рандомно определяется еще редкость.
//    - Каждая редкость имеет постоянный коэффициент к характеристикам предмета.
//    - У каждой редкости есть свой цвет (раз уж в консоль выводить научился). Пишешь в консоль после убийства моба - получен предмет с такими-то характеристиками.
//    - Присвоение случайного имени
//   Задача с двумя звездочками:
//    - Создать enum с типами предметов(кольцо, меч, шапка и т.д..)
//    - При создании предмета добавлять ему еще тип.

//
// ПРИМЕР СТРУКТУРЫ:
using System;
using System.Security.Cryptography.X509Certificates;

public class Hero
{
    Random random = new Random();
    public List<Item> inventory = new List<Item>(10);
    public string name;
    public int health;
    public int maxHealth;
    public int attackPower;
    public int magicPower;
    public int maxInventorySize = 10;

    public Hero(string heroName)
    {
        name = heroName;
        health = 100;
        maxHealth = 100;
        attackPower = 15;
        magicPower = 25;
        inventory = new List<Item>();
    }

    public void Attack(Enemy target)
    {
        int totalDamage = attackPower + random.Next(1, 5);
        target.TakeDamage(totalDamage);
        GameMessages.ShowDamage(this.name, target.name, totalDamage);
        // Наносим урон врагу
        // Можно добавить случайность: Random для урона ±5
        // target.TakeDamage(attackPower);
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
        // Магическая атака - больше урона, но тратит здоровье
        // Например: урон = magicPower + 10, но теряем 5 здоровья
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
        // Уменьшаем health на damage
        // Если health < 0, то health = 0
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
        // Восстанавливаем здоровье (например +25)
        // Но не больше maxHealth
    }

    public bool IsAlive()
    {
        return this.health > 0;
        // Возвращаем true если health > 0
    }

    public void DisplayStatus()
    {
        if (this.health > this.maxHealth / 3 * 2)
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

        // Выводим: "Герой: [имя] | HP: [здоровье]/[макс] | Атака: [сила]"
        // Можно цветом: зеленый если здоровья много, красный если мало
    }

    public void AddToInventory(Item item)
    {
        if (inventory.Count < this.maxInventorySize)
        {
            this.inventory.Add(item);
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
    public int healthBonus;
    public int attackBonus;
    public int magicBonus;

    public Item()
    {
        this.name = itemNames[random.Next(itemNames.Length)];
        this.healthBonus = random.Next(5, 20);
        this.attackBonus = random.Next(1, 5);
        this.magicBonus = random.Next(2, 10);
    }

    public void ItemDisplayInfo()
    {
        Console.WriteLine($"{this.name} \n +{this.healthBonus} HP \n +{this.attackBonus} AP \n +{this.magicBonus} MP");
    }


    public string[] itemNames =
    {
        "Кольцо 0" , "Кольцо 1" , "Кольцо 2" , "Кольцо 3" , "Кольцо 4" , "Кольцо 5"
    };
}

// Андрей ЛОХ

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
        int totalDamage = this.attackPower + random.Next(1, 5);
        target.TakeDamage(totalDamage);
        GameMessages.ShowDamage(this.name, target.name, totalDamage);
        // Атакуем героя
        // target.TakeDamage(attackPower);
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
        // Получаем урон, как у героя
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
        // Проверяем жив ли враг
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Враг: [{this.name}] | HP: [{this.health}]/[{this.maxHealth}]  | Атака: [{this.attackPower}]");
        // "Враг: [имя] | HP: [здоровье]/[макс]"
    }
}



public static class GameEngine
{
    static Random random = new Random();

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
            }

            // Проверяем, жив ли враг
            if (!enemy.IsAlive())
            {
                Console.WriteLine($"{enemy.name} повержен!");
                Item item = new Item();
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
    }

    public static int GetPlayerChoice()
    {
        int choice;
        while (true)
        {
            Console.Write("Ваш выбор (1-3): ");
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 3)
            {
                return choice;
            }
            Console.WriteLine("Неверный выбор! Введите число от 1 до 3.");
        }
    }

    public static Enemy CreateRandomEnemy()
    {
        string[] enemyNames = { "Гоблин", "Орк", "Скелет", "Волк", "Бандит" };
        string name = enemyNames[random.Next(enemyNames.Length)];
        int health = random.Next(50, 81); // от 50 до 80 HP
        int attack = random.Next(15, 26);  // от 15 до 25 урона
        

        return new Enemy(name, health, attack);
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
