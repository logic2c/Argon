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
            player.TryDrawCards(5);
        }
        foreach (var enemy in Enemies)
        {
            ShuffleBattlerDrawPile(enemy);
            enemy.TryDrawCards(5);
        }

        // 先后手
        if (Random.Range(0, 2) == 0)
        {
            // Player first
            SwitchToCurrentTurn(TurnFactory.CreateFirstFiveStatesTurn(Players[0]));
        }
        else
        {
            // Enemy first
            SwitchToCurrentTurn(TurnFactory.CreateFirstFiveStatesTurn(Enemies[0]));
        }
    }

    public void NextTurn()
    {
        if (Turns.Count > 0)
        {
            SwitchToCurrentTurn(Turns.Dequeue());
        }
        else
        {
            // append opponent's turn
            if (CurrentTurn.ActiveBattler is Player)
            {
                SwitchToCurrentTurn(TurnFactory.CreateFiveStatesTurn(Enemies[0]));
            }
            else if (CurrentTurn.ActiveBattler is NonPlayer)
            {
                SwitchToCurrentTurn(TurnFactory.CreateFiveStatesTurn(Players[0]));
            }
            else
            {
                Debug.LogError("Current turn's active battler is neither Player nor NonPlayer.");
            }

        }
    }

    void SwitchToCurrentTurn(FiveStatesTurn newTurn)  // 感觉好像应该叫 TurnStart...
    {
        CurrentTurn = newTurn;
        Debug.Log($"Current Turn Changed: {CurrentTurn.ActiveBattler.BattlerName}");
        CurrentTurn.StateMachine.Initialize();  // start first state
    }

    public static void ShuffleBattlerDrawPile(Battler battler)
    {
        battler.DrawPile.Shuffle();
    }

    public bool CheckActionLimited(ActionType type)
    {
        return CurrentTurn.StateMachine.CurrentState.ActionLimiter.CheckValid(type);
    }

    public void CurrentBattlerDrawCards(int count)
    {
        if (CurrentTurn == null)
        {
            Debug.LogError("No current turn to draw cards for.");
            return;
        }
        CurrentTurn.CurrentBattlerDrawCards(count);
    }

}
