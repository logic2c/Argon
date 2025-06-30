using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TurnFactory
{
    public static FiveStatesTurn CreateFiveStatesTurn(Battler activeBattler)
    {
        FiveStatesTurn turn = new FiveStatesTurn(activeBattler);  // start with Start state
        return turn;
    }
    public static FiveStatesTurn CreateFirstFiveStatesTurn(Battler activeBattler)
    {
        FiveStatesTurn turn = CreateFiveStatesTurn(activeBattler);
        turn.ReduceCurrentStateCommandCountRestriction(CommandType.DrawCard);  //////
        turn.AddTurnStateCount(FiveStatesTurn.TurnStateType.Battle, -1);
        return turn;
    }
}
