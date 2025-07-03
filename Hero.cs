using static Item;
using static Enemy;
using System.Diagnostics.CodeAnalysis;

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
        health = 100;
        maxHealth = 100;
        attackPower = 150;
        magicPower = 25;
        armor = 3;
    }

    public void Attack(Enemy target)
    {
        int totalDamage = attackPower + random.Next(1, 5);
        target.TakeDamage(totalDamage);
        GameMessages.ShowDamage(this.name, target.name, totalDamage);
    }

    public void CastSpell(Enemy target)
    {
        int totalDamage = magicPower + random.Next(1, 10);
        target.TakeDamage(totalDamage);
        int recoil = (totalDamage / 3);
        this.TakeDamage(recoil);
        GameMessages.ShowDamage(this.name, target.name, totalDamage);
        Console.WriteLine($"Магическая отдача нанесла {recoil} урона!");
    }

    public void TakeDamage(int damage)
    {
        if (this.health > 0)
        {
            this.health = this.health - (damage - armor);
        }
        
        if (this.health <= 0)
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
