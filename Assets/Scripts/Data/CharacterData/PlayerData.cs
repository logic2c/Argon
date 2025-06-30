using UnityEngine;

[CreateAssetMenu(menuName = "Battler/Player Data")]
public class PlayerData : BattlerData
{
    public PlayerData()
    {
        battlerType = BattlerType.Player;
    }
} 
