using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapType
{
    Normal,
    Coninuous,
}

[CreateAssetMenu(menuName = "CardData/Trap")]
public class TrapCardData : CardData
{
    public TrapType trapType;

    public int argonRecoveramount;
    public TrapCardData()
    {
        type = CardType.Spell;
    }


}
