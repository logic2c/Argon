using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EffectConditionBase
{
    public CardPosition from;
    public CardPosition to;
    public string course;

    public abstract bool IsValid();
}

public class NoCardLimitCondition : EffectConditionBase
{
    public Card card;
    public override bool IsValid()
    {
        Debug.Log("NoCardLimitCondition: Validating condition for card: " + card.cardData.cardName);
        // ����������ʵ�ʵ��޿�����������֤�߼�
        return true; // Ĭ�Ϸ���true����ʾ������Ч
    }
}

