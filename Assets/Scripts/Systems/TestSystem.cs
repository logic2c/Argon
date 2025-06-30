using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class TestSystem : MonoBehaviour
{
    void Start()
    {
        // read from scriptable object assets
        // Test
        DataManager.Instance.PCData = AssetDatabase.LoadAssetAtPath<PlayerData>("Assets/Data/Battler/PC.asset");
        DataManager.Instance.BattleData = AssetDatabase.LoadAssetAtPath<BattleData>("Assets/Data/Battle/TestBattle.asset");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
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

