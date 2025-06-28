using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattlerData : ScriptableObject
{
    public enum BattlerType
    {
        Player,
        Enemy,
        NPC
    }
    protected BattlerType battlerType;
    public Image battlerImage;
    public string battlerName;
    public List<CardData> Deck;

    public int maxHealth;
    //public int attack;  // weapon?

}

public class PlayerData : BattlerData
{
    public PlayerData()
    {
        battlerType = BattlerType.Player;
    }
}

public class EnemyData : BattlerData
{
    public EnemyData()
    {
        battlerType = BattlerType.Enemy;
    }
}
