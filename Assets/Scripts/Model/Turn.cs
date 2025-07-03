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
    public OperationLimiterUtil<TurnStateType> TurnStateLimiter { get; private set; } = new OperationLimiterUtil<TurnStateType>();

    public FiveStatesTurn(Battler battler, TurnBaseState initialState) : base(battler)
    {
        StateMachine = new TurnStateMachine(initialState);
        TurnStateLimiter.InitializeDefaults(1);
    }

    public bool CurrentBattlerTryDrawCard()
    {
        var limiter = StateMachine.CurrentState.ActionLimiterWithEvent;
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
    private OperationLimiterUtil<ActionType> ActionLimiter { get; set; } = new OperationLimiterUtil<ActionType>();
    public OperationLimiterUtilWithEventDecorator ActionLimiterWithEvent { get; private set; }
    public class OperationLimiterUtilWithEventDecorator
    {
        private readonly OperationLimiterUtil<ActionType> _baseLimiter;
        public OperationLimiterUtilWithEventDecorator(OperationLimiterUtil<ActionType> baseLimiter)
        {
            _baseLimiter = baseLimiter;
        }
        public void InitializeDefaults(int? defaultLimit = null)
        {
            _baseLimiter.InitializeDefaults(defaultLimit);
        }

        public void SetLimit(ActionType actionType, int? limit)
        {
            _baseLimiter.SetLimit(actionType, limit);
            BattleEventManager.TurnEvents.OnTurnStateActionLimitChanged?.Invoke(_baseLimiter, actionType);
        }
        public bool IncreaseLimit(ActionType actionType, int amount)
        {
            bool result = _baseLimiter.IncreaseLimit(actionType, amount);
            if (result)
            {
                BattleEventManager.TurnEvents.OnTurnStateActionLimitChanged?.Invoke(_baseLimiter, actionType);
            }
            return result;
        }
        public bool DecreaseLimit(ActionType actionType, int amount)
        {
            bool result = _baseLimiter.DecreaseLimit(actionType, amount);
            if (result)
            {
                BattleEventManager.TurnEvents.OnTurnStateActionLimitChanged?.Invoke(_baseLimiter, actionType);
            }
            return result;
        }
        public bool CheckValid(ActionType actionType)
        {
            return _baseLimiter.CheckValid(actionType);
        }

        public bool TryUse(ActionType actionType)
        {
            bool result = _baseLimiter.TryUse(actionType);
            if (result)
            {
                BattleEventManager.TurnEvents.OnTurnStateActionLimitChanged?.Invoke(_baseLimiter, actionType);
            }
            return result;
        }
        public int? GetRemainingCount(ActionType actionType)
        {
            return _baseLimiter.GetRemainingCount(actionType);
        }
        public void ResetAll()
        {
            _baseLimiter.ResetAll();
            //BattleEventManager.TurnEvents.OnTurnStateActionLimitReset?.Invoke(_baseLimiter);
        }
    }

    public TurnBaseState()
    {
        ActionLimiter.InitializeDefaults();
        ActionLimiterWithEvent = new OperationLimiterUtilWithEventDecorator(ActionLimiter);
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}

public class TurnStartState : TurnBaseState
{
    public TurnStartState() : base()
    {
        ActionLimiterWithEvent.SetLimit(ActionType.BattlerDrawCard, 1);
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
