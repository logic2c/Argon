using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlerFactory
{
    public static BattlerFactory Instance { get; } = new BattlerFactory();
    public Battler CreateBattler(BattlerData data)
    {
        Battler battler = new(data);
        return battler;
    }
}

public class PlayerFactory : BattlerFactory
{
    public static new PlayerFactory Instance { get; } = new PlayerFactory();
    public static Player CreatePlayer(PlayerData data)
    {
        return new Player(data);
    }
}

public class  NonPlayerFactory : BattlerFactory
{
    public static new NonPlayerFactory Instance { get; } = new NonPlayerFactory();
    public static NonPlayer CreateNonPlayer(NonPlayerData data)
    {
        return new NonPlayer(data);
    }

}
