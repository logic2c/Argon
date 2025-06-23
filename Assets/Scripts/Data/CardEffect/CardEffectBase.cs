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

public class  DrawCardEffect : CardEffectBase
{
    public int drawAmount;
    public override void Execute()
    {
        
    }
}
