using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    public BasicBattle battle { get; private set; }


    void Start()
    {
        Setup();

    }

    private void Setup()
    {
        BattleData battleData = DataManager.Instance.BattleData;
        PlayerData PCData = DataManager.Instance.PCData;
        if (battleData == null || PCData == null)
        {
            Debug.LogError("Battle data or PC data is not set in DataManager.");
            return;
        }

        battle = BattleFactory.CreateBasicBattleWithExtraBattlerData(battleData, new List<PlayerData> { PCData }, new List<NonPlayerData>());
        // x-对战开始-对战中-对战结束-x
        battle.StartBattle();
    }

    Queue<Command> commandQueue = new Queue<Command>();
    public void EnqueueCommand(Command command)
    {
        //if (battle.CheckCommandValid(command)){
        //    commandQueue.Enqueue(command);
        //}
        commandQueue.Enqueue(command);
    }
    public Command ActiveCommand { get; private set; }
    void Update()
    {
        if (!ActiveCommand.IsExecuting && commandQueue.Count > 0)
        {
            ActiveCommand = commandQueue.Dequeue();
            if (battle.CheckCommandValid(ActiveCommand))
            {
                battle.ReduceCommandCountRestriction(ActiveCommand);
                ActiveCommand.Execute();
            }
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
