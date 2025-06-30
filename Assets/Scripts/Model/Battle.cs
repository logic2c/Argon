using System.Collections.Generic;
using UnityEngine;

public abstract class Battle
{
    public BattleData Data { get; set; }
    //public Queue<Turn> Turns { get; private set; }

    public Battle(BattleData data)
    {
        Data = data;
        //Turns = new Queue<Turn>();
    }
    public abstract void StartBattle();
}

public class  BasicBattle : Battle  // 1v1 pve 随机先后手 轮流回合制
{
    public List<Player> Players { get; private set; }
    public List<NonPlayer> Enemies { get; private set; }


    public BasicBattle(BattleData data) : base(data) {
        Players = new List<Player>();
        Enemies = new List<NonPlayer>();
        Turns = new Queue<FiveStatesTurn>();
    }

    // StateMachine for Battle
    public Queue<FiveStatesTurn> Turns { get; private set; }
    public FiveStatesTurn CurrentTurn { get; private set; }


    public override void StartBattle()
    {
        // shuffle deck + 起手抽卡
        foreach (var player in Players)
        {
            ShuffleBattlerDrawPile(player);
            player.DrawCards(5);
        }
        foreach (var enemy in Enemies)
        {
            ShuffleBattlerDrawPile(enemy);
            enemy.DrawCards(5);
        }

        // 先后手
        if (Random.Range(0, 2) == 0)
        {
            // Player first
            Turns.Enqueue(TurnFactory.CreateFirstFiveStatesTurn(Players[0]));
        }
        else
        {
            // Enemy first
            Turns.Enqueue(TurnFactory.CreateFirstFiveStatesTurn(Enemies[0]));
        }
    }

    public static void ShuffleBattlerDrawPile(Battler battler)
    {
        battler.DrawPile.Shuffle();
    }
    public bool CheckCommandValid(Command command)
    {
        if (CurrentTurn == null)
        {
            Debug.LogError("No current turn to check command validity against.");
            return false;
        }
        return CurrentTurn.CheckCommandValid(command);
    }
    public void ReduceCommandCountRestriction(Command command)
    {
        if (CurrentTurn == null)
        {
            Debug.LogError("No current turn to reduce command count restriction.");
            return;
        }
        CurrentTurn.ReduceCurrentStateCommandCountRestriction(command.Type);
    }
}
