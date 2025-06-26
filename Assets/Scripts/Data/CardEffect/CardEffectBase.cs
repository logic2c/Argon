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
        // ����������ʵ�ʵ��˺��߼�
        Debug.Log($"Deal #{damageAmount} damage");
    }
}

public class  DrawCardEffect : CardEffectBase //�鿨Ч��
{
    public int drawAmount;
    public override void Execute()
    {
        
    }
}


public class SearchCardEffect : CardEffectBase //����Ч��
{
    public CardPosition destination;
    public int cardCount;
    public string searchCondition;  // search with filter

    public override void Execute()
    {
        Debug.Log($"Search card to {destination}");
    }
}

public class ActivateCardEffect : CardEffectBase //����Ч��
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

