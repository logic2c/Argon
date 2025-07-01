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
        // model draw cards
        //BattleModel.CurrentBattlerDrawCards(1);
        DrawCardButton.interactable = false;
    }

    public void OnTurnStartStateEntered()
    {
        EndStateButton.interactable = true;
        DrawCardButton.interactable = true;
        EndStateButtonTextComponent.text = "End Start State";
        
        
    }

    void Update()
    {
        
    }
}
