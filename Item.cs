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
        }
        else if (rare == "Rare")
        {
            Console.WriteLine($"[\u001b[32m{this.rare}\u001b[0m] {this.name} \n +{this.healthBonus} HP \n +{this.attackBonus} AP \n +{this.magicBonus} MP");
        }
        else if (rare == "Epic")
        {
            Console.WriteLine($"[\u001b[35m{this.rare}\u001b[0m] {this.name} \n +{this.healthBonus} HP \n +{this.attackBonus} AP \n +{this.magicBonus} MP");
        }
        else if (rare == "Legendary")
        {
            Console.WriteLine($"[\u001b[33m{this.rare}\u001b[0m] {this.name} \n +{this.healthBonus} HP \n +{this.attackBonus} AP \n +{this.magicBonus} MP");
        }
        else
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