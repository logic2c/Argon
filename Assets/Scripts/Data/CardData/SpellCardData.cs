using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpellType
{
    Environment,
    Field,
    Normal,
    Coninuous,
    Equip,
    ArgonRecover,
}

[CreateAssetMenu(menuName = "CardData/Spell")]
public class SpellCardData : CardData
{
    public SpellType spellType;


}
