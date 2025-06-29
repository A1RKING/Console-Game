using static Hero;

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

        if (this.health <= 0)
        {
            this.health = 0;
        }
    }

    public bool IsAlive()
    {
        return this.health > 0;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Враг: [{this.name}] | HP: [{this.health}]/[{this.maxHealth}]  | Атака: [{this.attackPower}]");
    }
}
