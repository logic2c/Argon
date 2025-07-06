using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public OperationLimiterUtil<TurnStateType> TurnStateLimiter { get; private set; }

    public FiveStatesTurn(Battler battler, TurnBaseState initialState) : base(battler)
    {
        TurnStateLimiter = new OperationLimiterUtil<TurnStateType>(BattleEventManager.TurnEvents.OnTurnStateLimitChanged, 1);
        StateMachine = new TurnStateMachine(initialState);
    }

    public bool CurrentBattlerTryDrawCard()
    {
        var limiter = StateMachine.CurrentState.ActionLimiter;
        // if both action limit and draw pile are valid, then draw a card
        if (limiter.CheckValid(ActionType.BattlerDrawCard) && ActiveBattler.CheckCanDrawCard())
        {
            limiter.TryUse(ActionType.BattlerDrawCard);
            ActiveBattler.TryDrawCard();
            Debug.Log($"{ActiveBattler.BattlerName} drew 1 card.");
            return true;
        }
        else
        {
            Debug.Log($"{ActiveBattler.BattlerName} cannot draw a card due to action limit or empty draw pile.");
            return false;
        }
    }

    public void CurrentBattlerDrawCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (CurrentBattlerTryDrawCard()) { 
                
            }
            else { 
                Debug.Log($"{ActiveBattler.BattlerName} drew {i} cards."); 
                return; 
            }
        }
        Debug.Log($"{ActiveBattler.BattlerName} drew {count} cards.");
    }
}


public class TurnStateMachine
{
    private TurnBaseState _currentState;
    private TurnBaseState _initialState;
    public TurnBaseState CurrentState { get
        {
            if (_currentState == null)
            {
                return _initialState;
            }
            return _currentState;
        } }
    public TurnStateMachine(TurnBaseState initialState)
    {
        _currentState = null;
        _initialState = initialState;
    }
    public void Initialize()
    {
        if (_initialState != null)
        {
            ChangeState(_initialState);
            _initialState = null;  // useless after initialization, gc it
        }
        else
        {
            Debug.LogError("Initial state is not set for the TurnStateMachine.");
        }
    }

    public void ChangeState(TurnBaseState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
}

public enum ActionType
{
    BattlerDrawCard,
    SummonMonster,
}
public abstract class TurnBaseState
{
    public OperationLimiterUtil<ActionType> ActionLimiter { get; set; }
    public TurnBaseState()
    {
        ActionLimiter = new OperationLimiterUtil<ActionType>(BattleEventManager.TurnEvents.OnTurnStateActionLimitChanged, null);
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}

public class TurnStartState : TurnBaseState
{
    public TurnStartState() : base()
    {
        ActionLimiter.SetLimit(ActionType.BattlerDrawCard, 1);
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

    public override void EnterState()
    {
        BattleEventManager.TurnMain2StateEvents.OnStateEntered?.Invoke();
    }

    public override void ExitState()
    {
        BattleEventManager.TurnMain2StateEvents.OnStateExited?.Invoke();
    }

    public override void UpdateState() { }
}

public class TurnEndState : TurnBaseState
{

    public override void EnterState()
    {
        BattleEventManager.TurnEndStateEvents.OnStateEntered?.Invoke();
    }

    public override void ExitState()
    {
        BattleEventManager.TurnEndStateEvents.OnStateExited?.Invoke();
    }

    public override void UpdateState() { }
}
