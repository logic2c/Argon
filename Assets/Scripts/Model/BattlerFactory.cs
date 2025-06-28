using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerFactory
{
    public static BattlerFactory instance { get; } = new BattlerFactory();
    public Battler CreateBattler(BattlerData data)
    {
        Battler battler = new(data);
        return battler;
    }
}

public class PlayerFactory : BattlerFactory
{
    public static Player CreatePlayer(PlayerData data)
    {
        return new Player(data);
    }
}

public static class  EnemyFactory
{
    public static Enemy CreateEnemy(EnemyData data)
    {
        return new Enemy(data);
    }

}
