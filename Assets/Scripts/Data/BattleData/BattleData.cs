using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle Data")]
public class BattleData : ScriptableObject
{
    public string battleName;
    public string description;
    public int maxTurns;
    public List<NonPlayerData> enemies;
    public List<PlayerData> players;
    //public List<EnemyData> enemies
    //public string globalBuff;  // 战斗特有buff之类的
    //public List<EventData> eventList;
}
