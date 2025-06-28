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
        // 这里可以添加实际的无卡限制条件验证逻辑
        return true; // 默认返回true，表示条件有效
    }
}

