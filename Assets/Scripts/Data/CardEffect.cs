// SO���� ����


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class EffectContext
//{
//    //public CardInstance sourceCard;  // ����Ч���Ŀ���ʵ��
//    //public Player caster;            // ʹ����
//    //public ITargetable target;       // Ŀ�꣨�ɳ���
//    //public Battlefield battlefield;  // ս��״̬����
//    // ����չ��������ӡ�ʱ�����
//}


//public interface IEffect
//{
//    // context ��������ʱ������������: ʩ���ߡ�Ŀ�ꡢս��״̬��
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

