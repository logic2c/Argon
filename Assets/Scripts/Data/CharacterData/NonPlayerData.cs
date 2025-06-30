using UnityEngine;

[CreateAssetMenu(menuName = "Battler/NonPlayer Data")]
public class NonPlayerData : BattlerData
{
    public NonPlayerData()
    {
        battlerType = BattlerType.Enemy;
    }
}
