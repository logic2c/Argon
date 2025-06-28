using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory
{
    public static CardFactory Instance { get; private set; } = new CardFactory();
    public virtual Card CreateCard(CardData data)
    {
        Card card = new Card(data);
        //card.Name = data.name;
        //card.Description = data.description;
        //card.cost = data.cost;
        //card.Type = data.cardType;
        //card.Power = data.power;
        //card.Image = data.image; // Assuming CardData has an Image property
        return card;
    }
}

public class MonsterCardFactory : CardFactory
{
    public static new MonsterCardFactory Instance { get; private set; } = new MonsterCardFactory();
    public override Card CreateCard(CardData data)
    {
        if (data is MonsterCardData monsterData)
        {
            MonsterCard card = new MonsterCard(monsterData);
            return card;
        }
        else
        {
            Debug.LogError("Invalid data type for MonsterCard creation.");
            return null;
        }
    }    
}

public class SpellCardFactory : CardFactory
{
    public static new SpellCardFactory Instance { get; private set; } = new SpellCardFactory();
    public override Card CreateCard(CardData data)
    {
        if (data is SpellCardData spellData)
        {
            SpellCard card = new SpellCard(spellData);
            return card;
        }
        else
        {
            Debug.LogError("Invalid data type for SpellCard creation.");
            return null;
        }
    }
}
