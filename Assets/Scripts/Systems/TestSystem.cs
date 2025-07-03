using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class TestSystem : MonoBehaviour
{
    void Start()
    {
        // read from scriptable object assets

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("---debug key down---");
            BattleManager.Instance.BattleModel.CurrentTurn.StateMachine.CurrentState.ActionLimiterWithEvent.IncreaseLimit(ActionType.BattlerDrawCard, 1);
        }
        // key 12345 represent 5 TurnStates
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("TurnState: Start");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("TurnState: Draw");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("TurnState: Action");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("TurnState: End");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("TurnState: Wait");
        }
    }

    //private void OnEnable()
    //{
    //    // Subscribe to the event
    //    EventManager.ExampleEvents.OnExampleChanged += HandleExampleChanged;
    //}
    //private void OnDisable()
    //{
    //    // Unsubscribe from the event
    //    EventManager.ExampleEvents.OnExampleChanged -= HandleExampleChanged;
    //}
    //private void HandleExampleChanged(string message)
    //{
    //    Debug.Log($"Event received: {message}");
    //}
}

