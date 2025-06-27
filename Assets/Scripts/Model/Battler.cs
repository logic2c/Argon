public class Battler
{
    public BattlerData battlerData;
    public int currentHealth;
    public int maxHealth;

    public Battler(BattlerData data)
    {
        battlerData = data;
        maxHealth = data.maxHealth;
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}

public class Player : Battler
{
    public Player(PlayerData data) : base(data)
    {
        // Additional player-specific initialization can go here
    }
}

