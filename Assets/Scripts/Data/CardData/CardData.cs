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

public enum CardGroup
{
    Boyshelpboys,
    Loveorloved,
    None
}

public abstract class CardData : ScriptableObject
{
    protected CardType cardType;
    public CardRace cardRace;
    public string cardName;
    public Sprite icon;
    public int cost;
    [TextArea] public string description;
    public List<CardGroup> group;

    [SerializeReference, SR]
    public List<CardEffectBase> effects;
    //public CardEffect[] onPlayEffects;      // ����ʱЧ��
    //public CardEffect[] onEnterBattlefield; // ����ս��ʱЧ��
    //public CardEffect[] onGraveyard;        // ����س�ʱЧ��


}

