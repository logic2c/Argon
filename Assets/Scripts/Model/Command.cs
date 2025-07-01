using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum CommandType
{
    Attack,
    Defend,
    Heal,
    DiscardCard,
    BattlerDrawCard,
    EndState,
    None
}
public abstract class Command
{
    // type things
    public CommandType commandType;
    // #CheckCommandValid() part
    public Battler CurrentTurnOwner { get; set; }
    public Command()
    {
        commandType = CommandType.None;
        CurrentTurnOwner = null;
    }

    // #Execte() part
    bool _isExecuting = false;
    public bool IsExecuting
    {
        get { return _isExecuting; }
        private set { _isExecuting = value; }
    }
    public async void Execute()
    {
        _isExecuting = true;
        await AsyncExecuter();
        _isExecuting = false;
    }
    protected abstract Task AsyncExecuter();


}


public class EndStateCommand : Command
{
    public EndStateCommand() : base() {
        commandType = CommandType.EndState;
    }
    protected override Task AsyncExecuter()
    {
        throw new System.NotImplementedException();
    }
}

public class BattlerDrawCardCommand : Command
{
    public Battler battler;
    public int count;
    public BattlerDrawCardCommand(Battler btl, int cnt) : base()
    {
        commandType = CommandType.BattlerDrawCard;
        battler = btl;
        count = cnt;
    }

    protected override Task AsyncExecuter()
    {
        for (int i = 0; i < count; i++)
        {
            BattleEventManager.BattlerEvents.OnBattlerCardDrawn?.Invoke(battler);

        }
        return Task.CompletedTask;
    }
}