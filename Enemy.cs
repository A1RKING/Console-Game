using static Hero;
using static Program;

public class Enemy
{

    // Статические
    static Random random = new Random();
    enum EnemyRace
    {
        goblin,
        orc,
        skelet,
        wolf,
        bandit,
        GreatOgr
    }

    // НЕстатические
    public string name;
    public int health;
    public int maxHealth;
    public int attackPower;

    public Enemy(string name, int health, int attackPower)
    {
        this.name = name;
        this.health = health;
        this.maxHealth = health;
        this.attackPower = attackPower;
    }

    public static Enemy CreateRandomEnemy()
    {
        EnemyRace enemyType = GetRandomEnemyType();
        string name = "";
        int health = 0;
        int attackPower = 0;

        switch (enemyType)
        {
            case EnemyRace.goblin:
                name = "Гоблин";
                health = random.Next(50, 61);
                attackPower = random.Next(10, 16);
                break;
            case EnemyRace.orc:
                name = "Орк";
                health = random.Next(60, 81);
                attackPower = random.Next(15, 21);
                break;
            case EnemyRace.skelet:
                name = "Скелет";
                health = random.Next(80, 91);
                attackPower = random.Next(15, 26);
                break;
            case EnemyRace.wolf:
                name = "Волк";
                health = random.Next(40, 51);
                attackPower = random.Next(10, 16);
                break;
            case EnemyRace.bandit:
                name = "Бандит";
                health = random.Next(60, 71);
                attackPower = random.Next(10, 21);
                break;
            case EnemyRace.GreatOgr:
                name = "Великий Вождь Огр";
                health = random.Next(160, 201);
                attackPower = random.Next(30, 51);
                break;
        }

        return new Enemy(name, health, attackPower);
    }


    public void Attack(Hero target)
    {
        int totalDamage = attackPower + random.Next(1, 5);
        if (totalDamage <= 0) totalDamage = 0;
        target.TakeDamage(totalDamage);
        GameMessages.ShowDamage(name, target.name, totalDamage);
    }

    public void TakeDamage(int damage)
    {
        if (health > 0)
        {
            health = health - damage;
        }

        if (health <= 0)
        {
            health = 0;
        }
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Враг: [{name}] | HP: [{health}]/[{maxHealth}]  | Атака: [{attackPower}]");
    }

    static EnemyRace GetRandomEnemyType()
    {
        int roll = random.Next(1, 21);
        EnemyRace enemyType;

        if (roll >= 19)
            enemyType = EnemyRace.GreatOgr;
        else if (roll >= 16)
            enemyType = EnemyRace.bandit;
        else if (roll >= 12)
            enemyType = EnemyRace.wolf;
        else if (roll >= 8)
            enemyType = EnemyRace.skelet;
        else if (roll >= 4)
            enemyType = EnemyRace.orc;
        else
            enemyType = EnemyRace.goblin;

        return enemyType;
    }
}
