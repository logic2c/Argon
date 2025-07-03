using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewManager : Singleton<ViewManager>
{
    //Test
    public Button EndStateButton;
    public Button DrawCardButton;
    public TextMeshProUGUI EndStateButtonTextComponent;

    void Start()
    {
        if(EndStateButton == null || DrawCardButton == null)
        {
            Debug.LogError("View Manager Setup Wrong");
        }
        else
        {
            if (EndStateButtonTextComponent == null) { EndStateButtonTextComponent = EndStateButton.GetComponentInChildren<TextMeshProUGUI>(); }
            EndStateButton.onClick.AddListener(HandleEndStateButtonClick);
            DrawCardButton.onClick.AddListener(HandleDrawCardButtonClick);
            

        }
    }

    void HandleEndStateButtonClick()
    {

    }
    void HandleDrawCardButtonClick()
    {
        BattleEventManager.ViewEvents.OnDrawCardButtonClick?.Invoke();
    }

    public void OnTurnStartStateEntered()
    {
        // ½ö×öÊ¾Àý
        
        
    }

    public void EnableDrawCardButton()
    {
        Debug.Log("enable draw card button");
        DrawCardButton.interactable = true;
    }
    public void DisableDrawCardButton()
    {
        Debug.Log("ban ddraw card button");
        DrawCardButton.interactable = false;
    }

    void Update()
    {
        
    }
}
