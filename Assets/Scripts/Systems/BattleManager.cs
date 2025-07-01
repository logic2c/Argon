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


    // Command part
    Queue<Command> commandQueue = new Queue<Command>();
    public void EnqueueCommand(Command command)
    {
        //if (battle.CheckCommandValid(command)){
        //    commandQueue.Enqueue(command);
        //}
        command.CurrentTurnOwner = BattleModel.CurrentTurn.ActiveBattler;
        commandQueue.Enqueue(command);
    }
    public Command ActiveCommand { get; private set; }
    void Update()
    {
        if (commandQueue.Count > 0 && !ActiveCommand.IsExecuting)
        {
            ActiveCommand = commandQueue.Dequeue();
            if (BattleModel.CheckCommandValid(ActiveCommand))
            {
                BattleModel.ReduceCommandCountRestriction(ActiveCommand);
                ActiveCommand.Execute();
            }
        }
    }

    // Events
    private void OnEnable()
    {
        BattleEventManager.TurnStartStateEvents.OnStateEntered += HandleTurnStartStateEntered;
    }
    private void OnDisable()
    {
        // Clean up if needed
        commandQueue.Clear();
        ActiveCommand = null;
        // Events
        BattleEventManager.TurnStartStateEvents.OnStateEntered -= HandleTurnStartStateEntered;
    }
    private void HandleTurnStartStateEntered()
    {
        Debug.Log("Turn Start State Entered");
        // view activate draw cards choice ui
        ViewManager.Instance.OnTurnStartStateEntered();
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
