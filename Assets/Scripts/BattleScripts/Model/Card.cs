using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public CardData cardData;
    public int cost;

    public Card(CardData data)
    {
        cardData = data;
        cost = data.cost;
    }



}


public class MonsterCard : Card
{
    public int range;
    public int attack;
    public int health;
    public int speed;
    public MonsterCard(MonsterCardData data) : base(data)
    {
        range = data.range;
        attack = data.attack;
        health = data.health;
        speed = data.speed;
    }
    public void Attack()
    {
        Debug.Log($"{cardData.cardName} attacks with {attack} damage!");
    }
}

public class SpellCard : Card
{
    public SpellCard(SpellCardData data) : base(data)
    {
    }
    public void Cast()
    {
        Debug.Log($"{cardData.cardName} casts a spell with effect: ");
    }
}

