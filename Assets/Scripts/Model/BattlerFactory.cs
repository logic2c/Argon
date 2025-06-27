using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class PlayerFactory 
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
