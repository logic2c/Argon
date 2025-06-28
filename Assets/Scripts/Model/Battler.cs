using System.Collections.Generic;

public class Battler
{
    public BattlerData BattlerData;
    public int CurrentHealth;
    public int MaxHealth;
    public string BattlerName;
    public List<Card> Deck;

    public Battler(BattlerData data)
    {
        BattlerData = data;
        MaxHealth = data.maxHealth;
        CurrentHealth = MaxHealth;
        BattlerName = data.battlerName;
        Deck = new List<Card>();
        foreach (CardData cardData in data.Deck)
        {
            Card card = CardFactory.Instance.CreateCard(cardData);
            Deck.Add(card);
        }

    }
    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }
    }
    public void Heal(int amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
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

public class Enemy : Battler
{
    public Enemy(EnemyData data) : base(data)
    {
        // Additional enemy-specific initialization can go here
    }

    // ai part
}