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

public class CardData : ScriptableObject
{
    public CardType type;
    public string cardName;
    public Sprite icon;
    public int cost;
    [TextArea] public string description;

    [SerializeReference]
    [SR]
    public List<CardEffectBase> effects;
    //public CardEffect[] onPlayEffects;      // ����ʱЧ��
    //public CardEffect[] onEnterBattlefield; // ����ս��ʱЧ��
    //public CardEffect[] onGraveyard;        // ����س�ʱЧ��
}

[CreateAssetMenu(menuName = "CardData/Monster")]
public class MonsterCardData : CardData
{
    public CardType type = CardType.Monster;
    public int range;
    public int attack;
    public int health;
    public int speed;

    public void Attack()
    {
        Debug.Log($"{cardName} attacks with {attack} damage!");
    }
}
