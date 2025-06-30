using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleFactory
{
    public static BasicBattle CreateBasicBattle(BattleData data)
    {
        BasicBattle battle = new BasicBattle(data);
        foreach (var playerData in data.players)
        {
            Player player = PlayerFactory.CreatePlayer(playerData);
            battle.Players.Add(player);
        }
        foreach (var enemyData in data.enemies)
        {
            NonPlayer enemy = NonPlayerFactory.CreateNonPlayer(enemyData);
            battle.Enemies.Add(enemy);
        }
        return battle;
    }

    public static BasicBattle CreateBasicBattleWithExtraBattlerData(BattleData data, List<PlayerData> playerData, List<NonPlayerData> enemyData)
    {
        BasicBattle battle = CreateBasicBattle(data);

        foreach (var pData in playerData)
        {
            Player player = PlayerFactory.CreatePlayer(pData);
            battle.Players.Add(player);
        }
        foreach (var eData in enemyData)
        {
            NonPlayer enemy = NonPlayerFactory.CreateNonPlayer(eData);
            battle.Enemies.Add(enemy);
        }

        return battle;
    }
}
