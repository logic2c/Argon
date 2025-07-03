using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static ExampleEvents Example ;
    public struct ExampleEvents 
    { 
        public static UnityAction<string> OnExampleChanged; 
    }

}
public static class BattleEventManager
{
    public struct CardEvents
    {
        public static UnityAction<Card> OnCardPlayed;
        public static UnityAction<Card> OnCardDrawn;
        public static UnityAction<Card> OnCardDiscarded;
    }
    public struct ViewEvents
    {
        public static UnityAction OnDrawCardButtonClick;
    }
    public struct BattlerEvents
    {
        public static UnityAction<Battler> OnBattlerHealthChanged;
        public static UnityAction<Battler> OnBattlerDied;
        public static UnityAction<Battler> OnBattlerTurnStart;
        public static UnityAction<Battler> OnBattlerTurnEnd;
        public static UnityAction<Battler> OnBattlerCardDrawn;
        public static UnityAction<Battler> OnDrawPileEmpty;
    }
    public struct TurnEvents
    {
        public static UnityAction<OperationLimiterUtil<ActionType>, ActionType> OnTurnStateActionLimitChanged;
    }
    public struct TurnStartStateEvents
    {
        public static UnityAction OnStateEntered;
        public static UnityAction OnStateExited;
    }
    public struct TurnMain1StateEvents {
        public static UnityAction OnStateEntered;
        public static UnityAction OnStateExited;
    }
    public struct TurnBattleStateEvents {
        public static UnityAction OnStateEntered;
        public static UnityAction OnStateExited;
    }
    public struct TurnMain2StateEvents {
        public static UnityAction OnStateEntered;
        public static UnityAction OnStateExited;
    }
    public struct TurnEndStateEvents
    {
        public static UnityAction OnStateEntered;
        public static UnityAction OnStateExited;
    }
}
