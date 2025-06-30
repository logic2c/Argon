using System.Collections.Generic;

public class Battler
{
    public BattlerData BattlerData;
    public int CurrentHealth;
    public int MaxHealth;
    public string BattlerName;
    public List<Card> Hand { get; private set; } = new List<Card>();
    public List<Card> DrawPile { get; private set; } = new List<Card>();
    public List<Card> DiscardPile { get; private set; } = new List<Card>();

    public Battler(BattlerData data)
    {
        BattlerData = data;
        MaxHealth = data.maxHealth;
        CurrentHealth = MaxHealth;
        BattlerName = data.battlerName;
        DrawPile = new List<Card>();
        foreach (CardData cardData in data.Deck)
        {
            Card card = CardFactory.Instance.CreateCard(cardData);
            DrawPile.Add(card);
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

    public void DrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (DrawPile.Count > 0)
            {
                Card drawnCard = DrawPile[0];
                DrawPile.RemoveAt(0);
                Hand.Add(drawnCard);
                BattleEventManager.BattlerEvents.OnBattlerCardDrawn?.Invoke(this);
            }
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

public class NonPlayer : Battler
{
    public NonPlayer(NonPlayerData data) : base(data)
    {
        // Additional enemy-specific initialization can go here
    }

    // ai part
}