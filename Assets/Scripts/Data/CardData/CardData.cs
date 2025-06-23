using JetBrains.Annotations;
using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Monster,
    Spell,
    Trap
}

public enum CardRace
{
    OldDeus,
    Phantasma,
    Elemental,
    Dragonia,
    Gigant,
    Flugel,
    Elf,
    Dwarf,
    Fairy,
    ExMachina,
    Demonia,
    Dhampir,
    Lunamana,
    Werebeast,
    Siren,
    Immanity,
    others,
}



public abstract class CardData : ScriptableObject
{
    public CardType type;
    public CardRace race;
    public string cardName;
    public Sprite icon;
    public int cost;
    [TextArea] public string description;

    [SerializeReference]
    [SR]
    public List<CardEffectBase> effects;
    //public CardEffect[] onPlayEffects;      // 发动时效果
    //public CardEffect[] onEnterBattlefield; // 进入战场时效果
    //public CardEffect[] onGraveyard;        // 进入坟场时效果


}

