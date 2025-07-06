using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "Card Game/Card Database")]
public class CardDatabase : ScriptableObject
{
    public List<CardData> allCards = new List<CardData>();
    public List<MonsterCardData> monsterCards = new List<MonsterCardData>();
    public List<SpellCardData> spellCards = new List<SpellCardData>();
    public List<TrapCardData> trapCards = new List<TrapCardData>();

    public void BuildFromCollector()
    {
        allCards.Clear();
        monsterCards.Clear();
        spellCards.Clear();
        trapCards.Clear();

        allCards.AddRange(CardDataCollector.AllCards);
        monsterCards.AddRange(CardDataCollector.MonsterCards);
        spellCards.AddRange(CardDataCollector.SpellCards);
        trapCards.AddRange(CardDataCollector.TrapCards);

        Debug.Log($"数据库构建完成. 总计: {allCards.Count}张卡牌");
    }
}