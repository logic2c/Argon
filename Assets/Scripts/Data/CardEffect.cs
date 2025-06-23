// SO方案 弃用


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class EffectContext
//{
//    //public CardInstance sourceCard;  // 触发效果的卡牌实例
//    //public Player caster;            // 使用者
//    //public ITargetable target;       // 目标（可抽象）
//    //public Battlefield battlefield;  // 战场状态引用
//    // 可扩展：随机种子、时间戳等
//}


//public interface IEffect
//{
//    // context 包含运行时所需所有数据: 施法者、目标、战场状态等
//    void Execute(EffectContext context);
//}

//public abstract class CardEffect : ScriptableObject, IEffect
//{
//    public abstract void Execute(EffectContext context);
//}


//[CreateAssetMenu(menuName = "Effects/DamageEffect")]
//public class DamageEffect : CardEffect
//{
//    [Header("")]
//    public int damageAmount;
//    public override void Execute(EffectContext context)
//    {
//        //context.target.TakeDamage(damageAmount);
//        Debug.Log($"deal #{damageAmount} damage");
//    }
//}


//[CreateAssetMenu(menuName = "Effects/DrawCardEffect")]
//public class DrawCardEffect : CardEffect
//{
//    public int numCards;
//    public override void Execute(EffectContext context)
//    {
//        //context.caster.DrawCards(numCards);
//    }
//}

