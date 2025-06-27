using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle Data")]
public class BattleData : ScriptableObject
{
    public string battleName;
    public string description;
    public int maxTurns;
    public EnemyData enemy;
    //public List<EnemyData> enemies;
    public string eventName; // 事件名称
    public string globalBuff;  // 战斗特有buff之类的

}
