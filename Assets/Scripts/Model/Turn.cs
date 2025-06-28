using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turn
{
    public Battler ActiveBattler { get; set; }

    public Turn(Battler battler)
    {
        ActiveBattler = battler;
    }
    public abstract void Execute();
}

public class FiveStatesTurn : Turn
{
    public enum TurnState
    {
        Start,
        Main1,
        Battle,
        Main2,
        End
    }
    public TurnState CurrentState { get; private set; }
    private int BattleStateCount;

    public FiveStatesTurn(Battler battler) : base(battler)
    {
        CurrentState = TurnState.Start;
        BattleStateCount = 1;
    }
    public override void Execute()
    {
        StartState();
        Main1State();
        for (int i = 0; i < BattleStateCount; i++)
        {
            BattleState();
        }
        Main2State();
        EndState();
    }

    private void StartState()
    {
        CurrentState = TurnState.Start;
        // draw cards
        Debug.Log($"{ActiveBattler.BattlerName} Turn Start");
    }
    private void Main1State()
    {
        CurrentState = TurnState.Main1;
        Debug.Log($"{ActiveBattler.BattlerName} Turn Start");
    }
    private void BattleState()
    {
        CurrentState = TurnState.Battle;
        Debug.Log($"{ActiveBattler.BattlerName} Turn Start");
    }
    private void Main2State()
    {
        CurrentState = TurnState.Main2;
        Debug.Log($"{ActiveBattler.BattlerName} Turn Start");
    }
    private void EndState()
    {
        CurrentState = TurnState.End;
        Debug.Log($"{ActiveBattler.BattlerName} Turn Start");
    }


}


//public class  TurnStateMachine
//{

//}

//public interface ITurnState
//{
//    public void EnterState(Turn turn);
//    public void UpdateState(Turn turn);
//    public void ExitState(Turn turn);
//}

//public class  TurnStartState : ITurnState
//{
//    public void EnterState(Turn turn) { }
//    public void UpdateState(Turn turn) { }
//    public void ExitState(Turn turn) { }
//}
//public class TurnMainState1 : ITurnState
//{
//    public void EnterState(Turn turn) { }
//    public void UpdateState(Turn turn) { }
//    public void ExitState(Turn turn) { }
//}
//public class TurnBattleState : ITurnState
//{
//    public void EnterState(Turn turn) { }
//    public void UpdateState(Turn turn) { }
//    public void ExitState(Turn turn) { }
//}
//public class TurnMainState2 : ITurnState
//{
//    public void EnterState(Turn turn) { }
//    public void UpdateState(Turn turn) { }
//    public void ExitState(Turn turn) { }
//}

//public class TurnEndState : ITurnState
//{
//    public void EnterState(Turn turn) { }
//    public void UpdateState(Turn turn) { }
//    public void ExitState(Turn turn) { }
//}
