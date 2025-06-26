using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EffectCondition
{
    public CardPosition from;
    public CardPosition to;
    public string course;
}



[System.Serializable]
public abstract class  CardEffectBase
{
    public EffectCondition condition;
    public abstract void Execute();
}

public class DamageEffect : CardEffectBase
{
    public int damageAmount;
    public override void Execute()
    {
        // 这里可以添加实际的伤害逻辑
        Debug.Log($"Deal #{damageAmount} damage");
    }
}

public class  DrawCardEffect : CardEffectBase //抽卡效果
{
    public int drawAmount;
    public override void Execute()
    {
        
    }
}


public class SearchCardEffect : CardEffectBase //检索效果
{
    public CardPosition destination;
    public int cardCount;
    public string searchCondition;  // search with filter

    public override void Execute()
    {
        Debug.Log($"Search card to {destination}");
    }
}

public class ActivateCardEffect : CardEffectBase //发动效果
{
    public CardPosition destination;
    public string activateCondition;
    public override void Execute()
    {
        Debug.Log($"Activate card to {destination}");
    }
}

public class SpecialSummonCardEffect : CardEffectBase 
{ 
    public CardPosition source;
    public string specialsummonCondition;
    public override void Execute()
    {
        Debug.Log($"SpecialSummon from {source} card to field");
    }
}

