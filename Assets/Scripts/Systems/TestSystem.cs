using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class TestSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //AudioManager.Instance.Test();

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

