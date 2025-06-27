using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleFactory
{
    public static Battle CreateBattle(BattleData data)
    {
        Battle battle = new Battle(data);
        foreach (var playerData in data.players)
        {
            Player player = PlayerFactory.CreatePlayer(playerData);
            battle.Players.Add(player);
        }
        foreach (var enemyData in data.enemies)
        {
            Enemy enemy = EnemyFactory.CreateEnemy(enemyData);
            battle.Enemies.Add(enemy);
        }
        return battle;
    }

    public static Battle CreateExtraBattlerBattle(BattleData data, List<PlayerData> playerData, List<EnemyData> enemyData)
    {
        Battle battle = CreateBattle(data);

        foreach (var pData in playerData)
        {
            Player player = PlayerFactory.CreatePlayer(pData);
            battle.Players.Add(player);
        }
        foreach (var eData in enemyData)
        {
            Enemy enemy = EnemyFactory.CreateEnemy(eData);
            battle.Enemies.Add(enemy);
        }

        return battle;
    }
}
