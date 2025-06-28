using System.Collections.Generic;
using UnityEngine;

public abstract class Battle
{
    public BattleData Data { get; set; }
    public List<Player> Players { get; private set; }
    public List<Enemy> Enemies { get; private set; }
    public Queue<Turn> Turns { get; private set; }

    public Battle(BattleData data)
    {
        Data = data;
        Players = new List<Player>();
        Enemies = new List<Enemy>();
        Turns = new Queue<Turn>();
    }
    public abstract void StartBattle();
}

public class  BasicBattle : Battle  // 1v1 pve 随机先后手 轮流回合制
{

    public BasicBattle(BattleData data) : base(data) { }

    // StateMachine for Battle
    public Turn CurrentTurn { get; private set; }


    public override void StartBattle()
    {
        // shuffle deck
        foreach (var player in Players)
        {
            player.Deck.Shuffle();
        }
        foreach (var enemy in Enemies)
        {
            enemy.Deck.Shuffle();
        }

        // 先后手
        //if( Random.Range(0, 2) == 0)
        //{
        //    // Player first
        //    Turns.Enqueue(new Turn(Players[0], Enemies[0]));
        //}
        //else
        //{
        //    // Enemy first
        //    Turns.Enqueue(new Turn(Enemies[0], Players[0]));
        //}
        while (Turns.Count > 0)
        {
            CurrentTurn = Turns.Dequeue();
            Debug.Log($"Executing turn for {CurrentTurn.ActiveBattler.BattlerName}");
            CurrentTurn.Execute();
            // add turn
        }
    }

}
