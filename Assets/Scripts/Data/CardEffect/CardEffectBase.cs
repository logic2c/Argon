using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class  CardEffectBase
{
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
