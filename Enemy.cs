using static Hero;
using static Program;

public class Enemy
{

    // Статические
    static Random random = new Random();
    enum EnemyRace
    {
        enemy,
        goblin,
        orc,
        skelet,
        wolf,
        bandit,
        bear,
        ghost,
        berserk,
        greatOgr,
        ent
    }

    // НЕстатические
    public string name;
    public int health;
    public int maxHealth;
    public int attackPower;
    public int armor;
    public bool magicResist;

    public Enemy(string name, int health, int attackPower, int armor, bool magicResist)
    {
        this.name = name;
        this.health = health;
        this.maxHealth = health;
        this.attackPower = attackPower;
        this.armor = armor;
        this.magicResist = magicResist;
    }

    public static Enemy CreateRandomEnemy()
    {
        EnemyRace enemyType = GetRandomEnemyType();
        string name = "";
        int health = 0;
        int attackPower = 0;
        int armor = 0;
        bool magicResist = false;

        switch (enemyType)
        {
            case EnemyRace.goblin:
                name = "Гоблин";
                health = random.Next(50, 61);
                attackPower = random.Next(10, 16);
                armor = random.Next(0, 3);
                magicResist = false;
                break;
            case EnemyRace.orc:
                name = "Орк";
                health = random.Next(60, 81);
                attackPower = random.Next(15, 21);
                armor = random.Next(2, 4);
                magicResist = false;
                break;
            case EnemyRace.skelet:
                name = "Скелет";
                health = random.Next(80, 91);
                attackPower = random.Next(10, 16);
                armor = random.Next(0, 1);
                magicResist = true;
                break;
            case EnemyRace.wolf:
                name = "Волк";
                health = random.Next(40, 51);
                attackPower = random.Next(10, 16);
                armor = random.Next(0, 1);
                magicResist = false;
                break;
            case EnemyRace.bandit:
                name = "Бандит";
                health = random.Next(60, 71);
                attackPower = random.Next(10, 21);
                armor = random.Next(1, 4);
                magicResist = false;
                break;
            case EnemyRace.bear:
                name = "Медведь";
                health = random.Next(90, 141);
                attackPower = random.Next(15, 26);
                armor = random.Next(2, 5);
                magicResist = false;
                break;
            case EnemyRace.ghost:
                name = "Призрак";
                health = random.Next(160, 201);
                attackPower = random.Next(5, 16);
                armor = 999;
                magicResist = false;
                break;
            case EnemyRace.berserk:
                name = "Герой Берсерк";
                health = random.Next(120, 161);
                attackPower = random.Next(25, 36);
                armor = random.Next(5, 11);
                magicResist = true;
                break;
            case EnemyRace.greatOgr:
                name = "Великий Вождь Огр";
                health = random.Next(160, 201);
                attackPower = random.Next(30, 51);
                armor = random.Next(10, 16);
                magicResist = false;
                break;
            case EnemyRace.ent:
                name = "Древний оскверненный Энт";
                health = random.Next(240, 301);
                attackPower = random.Next(41, 71);
                armor = random.Next(15, 26);
                magicResist = true;
                break;
        }

        return new Enemy(name, health, attackPower, armor, magicResist);
    }


    public void Attack(Hero target)
    {
        int totalDamage = attackPower + random.Next(1, 5) - target.armor;
        if (totalDamage <= 0) totalDamage = 0;
        target.TakeDamage(totalDamage);
        GameMessages.ShowDamage(name, target.name, totalDamage);
    }

    public void TakeDamage(int damage)
    {
        if (health > 0) health = health - damage;

        if (health <= 0) health = 0;
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
        int roll = random.Next(1, 27);
        EnemyRace enemyType = EnemyRace.enemy;

        switch (roll)
        {
            case int n when n == 26:
                return enemyType = EnemyRace.ent;
            case int n when n >= 24 && n <= 25:
                return enemyType = EnemyRace.greatOgr;
            case int n when n >= 22 && n <= 23:
                return enemyType = EnemyRace.berserk;
            case int n when n >= 19 && n <= 21:
                return enemyType = EnemyRace.ghost;
            case int n when n >= 16 && n <= 18:
                return enemyType = EnemyRace.bear;
            case int n when n >= 13 && n <= 15:
                return enemyType = EnemyRace.bandit;
            case int n when n >= 10 && n <= 12:
                return enemyType = EnemyRace.wolf;
            case int n when n >= 7 && n <= 9:
                return enemyType = EnemyRace.skelet;
            case int n when n >= 4 && n <= 6:
                return enemyType = EnemyRace.orc;
            case int n when n >= 1 && n <= 3:
                return enemyType = EnemyRace.goblin;
            default: 
                return enemyType;

        }
    }
}
