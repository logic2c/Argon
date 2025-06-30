using System.Collections.Generic;
using UnityEngine;

public abstract class Turn
{
    public Battler ActiveBattler { get; set; }

    public Turn(Battler battler)
    {
        ActiveBattler = battler;
    }
    //public abstract void Execute();
}

public class FiveStatesTurn : Turn
{
    public enum TurnStateType
    {
        Start,
        Main1,
        Battle,
        Main2,
        End
    }
    public TurnStateMachine StateMachine { get; private set; }
    public Dictionary<TurnStateType, int> TurnStateCount { get; private set; }

    public FiveStatesTurn(Battler battler) : base(battler)
    {
        StateMachine = new TurnStateMachine();
        StateMachine.ChangeState(new TurnStartState());
        TurnStateCount = new Dictionary<TurnStateType, int>
        {
            { TurnStateType.Start, 1 },
            { TurnStateType.Main1, 1 },
            { TurnStateType.Battle, 1 },
            { TurnStateType.Main2, 1 },
            { TurnStateType.End, 1 }
        };
    }
    public void AddTurnStateCount(TurnStateType stateType, int count)
    {
        if (TurnStateCount.ContainsKey(stateType))
        {
            TurnStateCount[stateType] += count;
        }
        else
        {
            Debug.LogError($"State type {stateType} does not exist in TurnStateCount.");
        }
    }
    public void AddStateCount(TurnStateType stateType, int count)
    {
        if (TurnStateCount.ContainsKey(stateType))
        {
            TurnStateCount[stateType] += count;
        }
        else
        {
            Debug.LogError($"State type {stateType} does not exist in TurnStateCount.");
        }
    }

    public bool CheckCommandValid(Command command)
    {
        if (!Object.ReferenceEquals(ActiveBattler, command.CurrentTurnOwner))  // 发动时机不对
        {
            return false;
        }
        else
        {
            if(StateMachine.CurrentState.CommandCountRestriction.TryGetValue(command.Type, out int restrictionCount))
            {
                if (restrictionCount > 0)
                {
                    return true;
                }
                else
                {
                    Debug.Log($"Command {command.Type} is restricted in the current state: {StateMachine.CurrentState.GetType().Name}");
                    return false;
                }
            }
            else  // 没写就是白名单
            {
                return true;
            }
        }
    }
    public void ReduceCurrentStateCommandCountRestriction(CommandType commandType)
    {
        if (StateMachine.CurrentState.CommandCountRestriction.TryGetValue(commandType, out int count))
        {
            //if (count > 0)
            //{
            //    StateMachine.CurrentState.CommandCountRestriction[commandType] = count - 1;
            //}
            //else
            //{
            //    Debug.LogError($"Command {commandType} count is already zero in the current state: {StateMachine.CurrentState.GetType().Name}");
            //}
            StateMachine.CurrentState.CommandCountRestriction[commandType] = count - 1;
        }
        else
        {
            //Debug.LogError($"Command {commandType} does not exist in the current state's restrictions.");
        }
    }
    
}


public class TurnStateMachine
{
    private TurnBaseState _currentState;
    public TurnBaseState CurrentState { get { return _currentState; } }
    public TurnStateMachine()
    {
        _currentState = null;
        ChangeState(new TurnStartState());
    }

    public void ChangeState(TurnBaseState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
}
public abstract class TurnBaseState
{
    public Dictionary<CommandType, int> CommandCountRestriction { get; private set; } = new Dictionary<CommandType, int>();
    public void AddCommandCountRestriction(CommandType commandType, int count)
    {
        if (CommandCountRestriction.ContainsKey(commandType))
        {
            CommandCountRestriction[commandType] += count;
        }
        else
        {
            CommandCountRestriction.Add(commandType, count);
        }
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}

public class TurnStartState : TurnBaseState
{
    public new Dictionary<CommandType, int> CommandCountRestriction { get; private set; }
    public TurnStartState()
    {
        CommandCountRestriction = new Dictionary<CommandType, int>
        {
            { CommandType.DrawCard, 1 },
        };
    }

    public override void EnterState()
    {
        BattleEventManager.TurnStartStateEvents.OnStateEntered?.Invoke();
    }

    public override void UpdateState(){}

    public override void ExitState()
    {
        BattleEventManager.TurnStartStateEvents.OnStateExited?.Invoke();
    }
}
public class TurnMain1State : TurnBaseState
{
    public new Dictionary<CommandType, int> CommandCountRestriction { get; private set; }

    public override void EnterState()
    {
        BattleEventManager.TurnMain1StateEvents.OnStateEntered?.Invoke();
    }

    public override void ExitState()
    {
        BattleEventManager.TurnMain1StateEvents.OnStateExited?.Invoke();
    }

    public override void UpdateState(){}
}
public class TurnBattleState : TurnBaseState
{
    public new Dictionary<CommandType, int> CommandCountRestriction { get; private set; }

    public override void EnterState()
    {
        BattleEventManager.TurnBattleStateEvents.OnStateEntered?.Invoke();
    }

    public override void ExitState()
    {
        BattleEventManager.TurnBattleStateEvents.OnStateExited?.Invoke();
    }

    public override void UpdateState(){}
}
public class TurnMain2State : TurnBaseState
{
    public new Dictionary<CommandType, int> CommandCountRestriction { get; private set; }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}

public class TurnEndState : TurnBaseState
{
    public new Dictionary<CommandType, int> CommandCountRestriction { get; private set; }

    public override void EnterState()
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
