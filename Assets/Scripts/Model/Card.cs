using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public CardData data;
    private int cost;

    public Card(CardData data)
    {
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
        Debug.Log($"{data.cardName} attacks with {attack} damage!");
    }
}

