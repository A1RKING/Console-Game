// ЗАДАЧА: Создать простую боевую систему RPG
// 
// ТРЕБОВАНИЯ:
// 1. Класс Hero - игровой персонаж игрока
//   [V] Поля: name, health, maxHealth, attackPower, magicPower
//   [V] Методы: Attack(), CastSpell(), TakeDamage(), Heal(), IsAlive(), DisplayStatus()
//
// 2. Класс Enemy - враг
//   [V] Поля: name, health, maxHealth, attackPower  
//   [V] Методы: Attack(), TakeDamage(), IsAlive(), DisplayInfo()
//
// 3. Статический класс GameEngine
//   [V] Метод StartBattle() - управление боем
//   [V] Метод ShowPlayerMenu() - показ действий игрока
//   [V] Метод GetPlayerChoice() - получение выбора
//   [V] Метод CreateRandomEnemy() - создание случайного врага
//
// 4. Статический класс GameMessages
//   [V] Метод ShowWelcome() - приветствие
//   [V] Метод ShowBattleResult() - результат боя
//   [V] Метод ShowDamage() - сообщение об уроне
//
// МЕХАНИКА БОя:
//   [V] Игрок выбирает: 1-атака, 2-заклинание, 3-лечение
//   [V] Враг атакует автоматически
//   [V] Ходы по очереди до смерти одного персонажа
//   [V] После победы появляется новый враг
//===================================================================================================================================================================
//   Что нужно сделать:
//   [V] Создать класс предмета.
//   [V] Добавить в этот класс характеристики предмета (например, +к урону или +к хп).
//   [V] Характеристики назначаются рандомные при создании предмета.
//   [V] Игроку добавить инвентарь (список предметов).
//   [V] После убийства моба создается случайный предмет и добавляется в инвентарь.
//   [V] При атаке игрока или при получении урона считаются все предметы и их баффы на урон или защиту.

//   Задача со звездочкой:
//   [V] Создать enum редкости предметов и добавить в класс предмета.
//   [V] При создании предмета рандомно определяется еще редкость.
//   [V] Каждая редкость имеет постоянный коэффициент к характеристикам предмета.
//   [V] У каждой редкости есть свой цвет (раз уж в консоль выводить научился). Пишешь в консоль после убийства моба - получен предмет с такими-то характеристиками.
//   [V] Присвоение случайного имени
//   Задача с двумя звездочками:
//   [V] Создать enum с типами предметов(кольцо, меч, шапка и т.д..)
//   [V] При создании предмета добавлять ему еще тип.

//
// ПРИМЕР СТРУКТУРЫ:
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static Item;
using static Hero;
using static Enemy;

public static class GameEngine
{
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
                    ShowPlayerMenu();
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
            Enemy currentEnemy = Enemy.CreateRandomEnemy();
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