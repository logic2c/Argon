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
    public static ExampleEvents Example;
    public struct ExampleEvents
    {
        public static UnityAction<string> OnExampleChanged;
    }

}
