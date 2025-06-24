using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle Data")]
public class BattleData : ScriptableObject
{
    public string battleName;
    public string description;
    public int maxTurns;
    public string enemy;  ///////
    public string eventName; // �¼�����
    public string globalBuff;  // ս������buff֮���
}
