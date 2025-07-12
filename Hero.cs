using static Item;
using static Enemy;
using System.Diagnostics.CodeAnalysis;
using System;

public class Hero
{
    Random random = new Random();
    public List<Item> inventory = new List<Item>();
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
        health = 150;
        maxHealth = health;
        attackPower = 15;
        magicPower = 25;
        armor = 3;
    }

    public void Attack(Enemy target)
    {
        int totalDamage = attackPower + random.Next(1, 5) - target.armor;
        if (totalDamage < 0 && target.armor == 999)
        {
            totalDamage = 0;
            Console.WriteLine("Цель имеет иммунитет к магии!");
            target.TakeDamage(totalDamage);
        }
        else if (totalDamage < 0)
        {
            totalDamage = 0;
            target.TakeDamage(totalDamage);
        }
        else
        {
            target.TakeDamage(totalDamage);
        }
        GameMessages.ShowDamage(this.name, target.name, totalDamage);
    }

    public void CastSpell(Enemy target)
    {
        int totalDamage = magicPower + random.Next(1, 10);
        if (target.magicResist == true)
        {
            totalDamage = 0;
            target.TakeDamage(totalDamage);
            Console.WriteLine("Цель имеет иммнитет к магии!");
        }
        else if (target.armor == 9999)
        {
            totalDamage = totalDamage * 3;
            target.TakeDamage(totalDamage);
        }
        else
        {
            target.TakeDamage(totalDamage);
        }

        int recoil = (totalDamage / 4);
        if (target.armor == 9999)
        {
            recoil = 0;
            this.TakeDamage(recoil);
        }
        else
        {
            this.TakeDamage(recoil);
        }

        GameMessages.ShowDamage(this.name, target.name, totalDamage);
        Console.WriteLine($"Магическая отдача нанесла {recoil} урона!");
    }

    public void TakeDamage(int damage)
    {
        if (this.health > 0) this.health = this.health - damage;

        if (this.health <= 0) this.health = 0;
    }

    public void Heal()
    {
        int totalHeal = this.magicPower + random.Next(20, 35);
        this.health = this.health + totalHeal;
        Console.WriteLine($"{this.name} восполнил здоровье на {totalHeal}!");

        if (this.health > this.maxHealth) this.health = this.maxHealth;
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
        Item currentItem = Item.Inventory.GetEquippedItem(item.Type);

        if (currentItem != null)
        {
            Console.WriteLine("\n=== Сравнение предметов ===");
            Console.WriteLine("\nТекущий предмет:");
            currentItem.ItemDisplayInfo();
            Console.WriteLine("\nНовый предмет:");
            item.ItemDisplayInfo();

            PlayerChoiceMenu();

            int choice = GetPlayerChoiceMenu();

            if (choice == 1)
            {
                RemoveItemBonuses(currentItem);
                Item.Inventory.EquipItem(item);
                ApplyItemBonuses(item);
                item.ItemDisplayInfo();
                Console.WriteLine("\nПредмет успешно экипирован!");
            }
            else
            {
                Console.WriteLine("Предмет уничтожен!");
            }
        }
        else
        {
            Item.Inventory.EquipItem(item);
            ApplyItemBonuses(item);
            item.ItemDisplayInfo();
            Console.WriteLine("\nПредмет успешно экипирован!");
        }



        void PlayerChoiceMenu()
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1 - Экипировать");
            Console.WriteLine("2 - Уничтожить");
        }

        int GetPlayerChoiceMenu()
        {
            int choice;
            while (true)
            {
                Console.Write("Ваш выбор (1-2): ");
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 2)
                {
                    return choice;
                }
                Console.WriteLine("Неверный выбор! Введите число от 1 до 2.");
            }
        }
    }

    public void InventoryDisplayInfo()
    {
        Console.WriteLine("\n=== ЭКИПИРОВКА ===");

        // Проверяем и выводим каждый слот
        PrintSlotIfExists(Item.Inventory.head, "Шлем");
        PrintSlotIfExists(Item.Inventory.hands, "Перчатки");
        PrintSlotIfExists(Item.Inventory.legs, "Штаны");
        PrintSlotIfExists(Item.Inventory.body, "Кираса");
        PrintSlotIfExists(Item.Inventory.rings, "Кольцо");
        PrintSlotIfExists(Item.Inventory.weapon, "Оружие");

        // Вспомогательный метод для вывода слота
        void PrintSlotIfExists(Item item, string Type)
        {
            if (item != null)
            {
                item.ItemDisplayInfo();
            }
            else
            {
                Console.WriteLine($"\n [{Type}]: Пусто");
            }
        }
    }

    public void RemoveItemBonuses(Item item)
    {
        this.health -= item.healthBonus;
        this.maxHealth -= item.healthBonus;
        this.attackPower -= item.attackBonus;
        this.magicPower -= item.magicBonus;
        this.armor -= item.armorBonus;
    }

    public void ApplyItemBonuses(Item item)
    {
        this.health += item.healthBonus;
        this.maxHealth += item.healthBonus;
        this.attackPower += item.attackBonus;
        this.magicPower += item.magicBonus;
        this.armor += item.armorBonus;
    }
}