using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardData/Monster")]
public class MonsterCardData : CardData
{
    public int range;
    public int attack;
    public int speed;
    public int health;

    public MonsterCardData()
    {
        cardType = CardType.Monster;
    }


    public void Attack()
    {
        Debug.Log($"{cardName} attacks with {attack} damage!");
    }
}

