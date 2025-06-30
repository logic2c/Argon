using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum CommandType
{
    Attack,
    Defend,
    Heal,
    DrawCard,
    DiscardCard
}
public abstract class Command
{
    // type things
    public CommandType Type { get; private set; }

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


    // #CheckCommandValid() part
    public Battler CurrentTurnOwner { get; private set; }
}

