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
        // 这里可以添加实际的伤害逻辑
        Debug.Log($"Deal #{damageAmount} damage");
    }
}
