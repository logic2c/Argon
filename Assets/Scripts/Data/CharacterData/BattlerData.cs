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
    protected BattlerType type;
    public Image battlerImage;
    public string battlerName;
    public List<Card> deck;

    public int maxHealth;
    //public int attack;  // weapon?

}

public class PlayerData : BattlerData
{
    public PlayerData()
    {
        type = BattlerType.Player;
    }
}

public class EnemyData : BattlerData
{
    public EnemyData()
    {
        type = BattlerType.Enemy;
    }
}
