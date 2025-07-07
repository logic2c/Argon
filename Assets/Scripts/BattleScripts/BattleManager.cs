using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    public BasicBattle BattleModel { get; private set; }


    void Start()
    {
        // Test
        DataManager.Instance.PCData = AssetDatabase.LoadAssetAtPath<PlayerData>("Assets/Data/Battler/TestPC.asset");
        DataManager.Instance.BattleData = AssetDatabase.LoadAssetAtPath<BattleData>("Assets/Data/Battle/TestBattle.asset");

        SetupModel();
        SetupView();
    }

    private void SetupModel()
    {
        BattleData battleData = DataManager.Instance.BattleData;
        PlayerData PCData = DataManager.Instance.PCData;
        if (battleData == null || PCData == null)
        {
            Debug.LogError("Battle data or PC data is not set in DataManager.");
            return;
        }

        BattleModel = BattleFactory.CreateBasicBattleWithExtraBattlerData(battleData, new List<PlayerData> { PCData }, new List<NonPlayerData>());
        // x-对战开始-对战中-对战结束-x
        BattleModel.StartBattle();
    }

    private void SetupView()
    {
    }


    void Update()
    {
    }

    void EndBattle(Battler winner=null, Battler loser=null)
    {
        if (winner == null && loser == null) {
            Debug.Log("No one wins...");
        }
        else
        {

        }
    }

    // Events
    private void OnEnable()
    {
        BattleEventManager.TurnStartStateEvents.OnStateEntered += HandleTurnStartStateEntered;
        BattleEventManager.ViewEvents.OnDrawCardButtonClick += HandleViewDrawCardButtonClick;
        BattleEventManager.BattlerEvents.OnBattlerCardDrawn += HandleBattlerCardDrawn;
        BattleEventManager.BattlerEvents.OnDrawPileEmpty += HandleBattlerDrawPileEmpty;
        BattleEventManager.TurnEvents.OnTurnStateActionLimitChanged += HandleTurnStateActionLimitChanged;
    }
    private void OnDisable()
    {
        BattleEventManager.TurnStartStateEvents.OnStateEntered -= HandleTurnStartStateEntered;
        BattleEventManager.ViewEvents.OnDrawCardButtonClick -= HandleViewDrawCardButtonClick;
        BattleEventManager.BattlerEvents.OnBattlerCardDrawn -= HandleBattlerCardDrawn;
        BattleEventManager.BattlerEvents.OnDrawPileEmpty -= HandleBattlerDrawPileEmpty;
        BattleEventManager.TurnEvents.OnTurnStateActionLimitChanged -= HandleTurnStateActionLimitChanged;


    }
    void HandleTurnStartStateEntered()
    {
        Debug.Log("Turn Start State Entered");
        ViewManager.Instance.OnTurnStartStateEntered();  /////
        if(BattleModel.CurrentTurn.ActiveBattler is Player)
        {
            // view activate draw cards choice ui
            if (BattleModel.CheckActionLimited(ActionType.BattlerDrawCard)){
                ViewManager.Instance.DrawCardButton.interactable = true;
            }
            else { ViewManager.Instance.DrawCardButton.interactable = false; }
            ViewManager.Instance.EndStateButton.interactable = true;
            ViewManager.Instance.EndStateButtonTextComponent.text = "End Start State";

        }
        else if (BattleModel.CurrentTurn.ActiveBattler is NonPlayer)
        {
            NonPlayer enemy = BattleModel.CurrentTurn.ActiveBattler as NonPlayer;
            enemy.DecideStartStateDrawCard();  /////
        }
        else
        {
            Debug.LogError("how could this happen???");
        }
    }
    void HandleViewDrawCardButtonClick()
    {
        Debug.Log("Start State Draw Card");
        // model draw cards
        BattleModel.CurrentBattlerDrawCards(1);
    }
    void HandleBattlerCardDrawn(Battler battler)
    {
        //view part, player and enemy
    }
    void HandleBattlerDrawPileEmpty(Battler battler)
    {
        EndBattle(loser: battler);
    }
    void HandleTurnStateActionLimitChanged(OperationLimiterUtil<ActionType> limiter, ActionType action)
    {
        switch (action)
        {
            case ActionType.BattlerDrawCard:
                if(limiter.CheckValid(action))
                {
                    ViewManager.Instance.EnableDrawCardButton();
                }
                else
                {
                    ViewManager.Instance.DisableDrawCardButton();
                }
                break;
        }
    }
}

public enum CardPosition
{
    Hand,
    Deck,
    Graveyard,
    Field
}
public class  CardPositionBase
{
    public CardPosition type;
}
